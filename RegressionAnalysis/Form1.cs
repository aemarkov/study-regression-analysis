using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;	

using Mathematics;

namespace RegressionAnalysis
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

			double[][] matrix = new double[][]
			{
				new double[]{2,44,5,9},
				new double[]{4,5,51,6},
				new double[]{76,43,6,74}
			};
			var a = SLAUSolver.Solve(matrix);
		}

		//Чтение данных и расчет
		private void btnOpen_Click(object sender, EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			if (dlg.ShowDialog() != DialogResult.OK)
				return;

			var data = readData(dlg.FileName, new Func<double, double>[] { x => x, x2Lin, yLin });
			var a = convertOriginal(SLAUSolver.Solve(calcSLAUMatrix(data)), new Func<double, double>[] { x => x, x => x, x => x });
			
		}


		#region PARSING

		//Читает данные из файла
		//Осуществляет необходимые замены переменных,
		//приводит данные к употребимому виду
		List<double[]> readData(string filename, Func<double, double>[] convertors)
		{
			//Создаем список, куда будем складывать значения
			var list = new List<double[]>();

			using (var reader = new StreamReader(filename))
			{
				//По заголовоку определяем число переменных
				string line = reader.ReadLine();
				if (line == null) throw new IOException("Файл пуст");

				int numberOfVar = line.Count(x => x == ';')+1;
				if (numberOfVar != convertors.Length) throw new WrongNumberOfVariablesException();

				//Парсим остальные строки
				while ((line = reader.ReadLine()) != null)
					list.Add(parseString(line, convertors));
			}

			return list;
		}

		//Парсинг строки в массив вместе с преобразованием
		double[] parseString(string str, Func<double, double>[] convertors)
		{
			var strs = str.Split(';');
			if (strs.Length != convertors.Length) throw new WrongNumberOfVariablesException();
			double[] values = new double[strs.Length+1];
			values[0] = 1;	//Граязнохак для дальнейших вычислений

			for(int i= 0; i<strs.Length; i++)
				values[i+1] = convertors[i](Double.Parse(strs[i]));

			return values;
		}

		#endregion

		//Функции для линеаризации значений
		double x2Lin(double x2)
		{
			if (x2 <= 0)
				throw new LogException(x2);
			return  Math.Log(x2);
		}

		double yLin(double y)
		{
			if (y <= 0)
				throw new LogException(y);
			return Math.Log(y);
		}


		//Вычисление матрицы коэффициентов и свободных членов для СЛАУ
		double[][] calcSLAUMatrix(List<double[]> rawData)
		{

			/*	Расчет коэффициентов:
				N - число испытаний
				М - число параметров
	
				Матрица для M=3

				| N			 sum (Xi1)		sum (Xi2)     |
				| sum (xi1)	 sum (xi1*xi1)	sum (xi2*xi1) |
				| sum (xi2)	 sum (xi1*xi2)	sum (xi2*xi2) |

				Можно видеть, что в первой строке и в первом
				столбце отсутсвует домножение на Xik. Чтобы 
				не писать для этих строк отдельный код, введем
				ФИКТИВНЫЙ х в массив исходных данных, равный 1.

				Было:     Стало:
				X11 X12   1 X11 X12
				X21 X22   1 X21 X22
				...       ...    
				Xn1 Xn2   1 Xn1 Xn2
	          

				Перепишем матрицу:
        
					1               2               3
				1 | sum (xi0*xi0) 	sum (Xi1*xi0)	sum (Xi2*xi0) |
				2 | sum (xi0*xi1)	sum (xi1*xi1)	sum (xi2*xi1) |
				3 | sum (xi0*xi2)	sum (xi1*xi2)	sum (xi2*xi2) |

				Аналогично для свободных членов

				1 | sum (yi*xi0) |
				2 | sum (yi*xi1) |
				3 | sum (yi(xi2) |
			*/

			int N = rawData.Count;              //Число испытаний
			int M = rawData[0].Length-1;        //Число аргументов = число переменных - y(1шт) + 1(a0) = число переменных

			//Создаем матрицу коээфициентов
			double[][] A = new double[M][];
			for (int i = 0; i < M; i++)
				A[i] = new double[M+1];

			//Расчитываем
			for (int k = 1; k <= M; k ++)
			{
				//k-номер строки
				//Расчет K-й строки

				//Расчет коэффициентов
				//Из-за сдвига - нумерация строк с 1, а не 0
				for (int i = 1; i <= M; i++)
				{
					//i - номер элемента в строке (номер столбца)
					//Расчет элемента A[k,i]
					A[k - 1][i - 1] = 0;
					for (int j = 0; j < N; j++)
					{
						//j - номер эксперимента
						A[k - 1][i - 1] += rawData[j][i - 1] * rawData[j][k - 1];
					}
				}

				//Расчет свободных членов
				A[k-1][M] = 0;
				for(int j=0; j<N; j++)
				{
					A[k - 1][M] += rawData[j][M] * rawData[j][k-1];
				}
			}

			return A;
		}


		//Преобразует полученные коээфициенты обратно в исходные
		//после замены переменной
		double[] convertOriginal(double[] a, Func<double, double>[] convertors)
		{
			if (a.Length != convertors.Length) throw new WrongNumberOfVariablesException();

			var newA = new double[a.Length];
			for(int i =0; i<a.Length; i++)
			{
				newA[i] = convertors[i](a[i]);
			}
			return newA;
		}
	}
}
