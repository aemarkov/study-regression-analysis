using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathematics
{
	//Осуществляет регрессионный анализ
	class RegressionAnalisys
	{
		private Func<double, double>[] converters;

		private List<double[]> rawData;
		private List<double[]> linData;
		private double[] y;
		private double[] a;
		private int N;						//Размер выборки
		private int m;                      //ЧИСЛО ПЕРЕМЕННЫХ
		private int M;						//ЧИСЛО КОЭЭФИЦИЕНТОВ

		//Создает объект, задавая функции преобразования
		public RegressionAnalisys(Func<double, double>[] converters)
		{
			this.converters = converters;

			//Конвертеры есть для x1, x2,...,Xm, Y
			//Тут можно запутаться. M используется для индексации массивов 
			//в вычислениях
			m = converters.Length-1;
			M = m + 1;
		}

		/// <summary>
		/// производит регрессионный анали
		/// </summary>
		/// <param name="data">Экспериментальные данные</param>
		/// <returns>Массив коэффициентов</returns>
		public double[] Analysis(List<double[]> data)
		{
			N = data.Count;
			linData = linearise(data, converters);
			a = convertOriginal(SLAUSolver.Solve(calcSLAUMatrix(linData)), new Func<double, double>[] { x => x, x => x, x => x });
			y = calcY(a, linData);
			return a;
		}

		//Остаточная дисперсия
		//sum (Yэi - Yтi)^2(N-M-1)
		public double CalcDispersion()
		{
			double d = 0;
			for(int i = 0; i<N; i++)
			{
				var v = Math.Pow(linData[i][M] - y[i], 2);
				d += v;
			}

			return d / (N - m - 1);
		}

		//Средняя относительная ошибка
		//sum (abs(Yэi-Yтi)/Yэi))/N*100
		public double CalcAverageError()
		{
			double err = 0;
			for(int i = 0; i<N; i++)
			{
				err += Math.Abs((linData[i][M] - y[i]) / linData[i][M]);
			}

			return err / N * 100;
		}

		//F-критерий Фищера
		public double CalcFCriterion()
		{
			double avY;		//среднее
			double avD;     //Дисперсия  среднего
			double ostD;	//Остаточная дисперсия

			//Вычисляем среднее значение Y экспериментального
			avY = 0;
			foreach (var row in linData)
				avY += row[M]*row[M];
			avY /= N;

			//Дисперсия среднего
			avD = 0;
			foreach (var yc in y)
				avD += Math.Pow(yc - avY, 2);
			avD /= (N - 1);

			//Остаточная дисперсия
			ostD = CalcDispersion();

			return avD / ostD;
		}

		//Линеаризует значения
		//Добавляет единичный столбец для дальнейших вычислений
		List<double[]> linearise(List<double[]> data, Func<double, double>[] convertors)
		{
			List<double[]> converted = new List<double[]>();
			foreach (var row in data)
			{
				//Проверка на верное число столбцов
				if (row.Length != M) throw new WrongNumberOfVariablesException();

				converted.Add(new double[row.Length + 1]);
				converted[converted.Count - 1][0] = 1;
				for (int i = 0; i < row.Length; i++)
					converted[converted.Count - 1][i + 1] = convertors[i](row[i]);

			}

			return converted;
		}

		//Вычисление матрицы коэффициентов и свободных членов для СЛАУ
		double[][] calcSLAUMatrix(List<double[]> data)
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

			//int N = rawData.Count;				//Число испытаний
			//int M = rawData[0].Length - 1;      //Число аргументов = число переменных - y(1шт) + 1(a0) = число переменных

			//Создаем матрицу коээфициентов
			double[][] A = new double[M][];
			for (int i = 0; i < M; i++)
				A[i] = new double[M + 1];

			//Расчитываем
			for (int k = 1; k <= M; k++)
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
						A[k - 1][i - 1] += data[j][i - 1] * data[j][k - 1];
					}
				}

				//Расчет свободных членов
				A[k - 1][M] = 0;
				for (int j = 0; j < N; j++)
				{
					A[k - 1][M] += data[j][M] * data[j][k - 1];
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
			for (int i = 0; i < a.Length; i++)
			{
				newA[i] = convertors[i](a[i]);
			}
			return newA;
		}

		//Расчет значений y на основе вычисленных коэффициентов
		double[] calcY(double[] a, List<double[]> data)
		{
			var newY = new double[data.Count];

			for (int j = 0; j < data.Count; j++)
			{
				newY[j] = 0;
				for (int i = 0; i < a.Length; i++)
					newY[j] += data[j][i] * a[i];
			}

			return newY;
		}
	}
}
