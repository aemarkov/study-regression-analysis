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

			try
			{
				var ra = new RegressionAnalisys(new Func<double, double>[] { x => x, x2Lin, yLin }, f);
				var rawData = readData(dlg.FileName, 2);
				var a = ra.Analysis(rawData);

				//Расчет характеристик
				double dispersion = ra.CalcDispersion();
				double avError = ra.CalcAverageError();
				double fCrit = ra.CalcFCriterion();

				//Вывод характеристик
				txtDispertion.Text = dispersion.ToString("0.###E+000");
				txtAvErr.Text = avError.ToString("0.###E+000");
				txtFCrit.Text = fCrit.ToString("0.###E+000");

				//Вывод коэффициентов
				gridA.Columns.Clear();
				gridA.Columns.Add("name", "Коэффициент");
				gridA.Columns.Add("value", "Значение");

				for (int i = 0; i < a.Length; i++)
				{
					gridA.Rows.Add("a" + i, a[i].ToString("N3"));
				}

			}
			catch (IOException exp)
			{
				MessageBox.Show(exp.Message, "Ошибка отрытия файла", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			catch (LogException exp)
			{
				MessageBox.Show(exp.Message, "Ошибка вычисления", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			catch (WrongNumberOfVariablesException exp)
			{
				MessageBox.Show(exp.Message, "Ошибка данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		

		#region FUNCTIO_SPECIFIC

		//Функции для линеаризации значений
		double x2Lin(double x2)
		{
			if (x2 <= 0)
				throw new LogException(x2);
			return Math.Log(x2);
		}

		double yLin(double y)
		{
			if (y <= 0)
				throw new LogException(y);
			return Math.Log(y);
		}

		//Функция, с которой работаем
		double f(double[] a, double[] x)
		{
			if ((a.Length != 3) && (x.Length != 2)) throw new WrongNumberOfVariablesException();
			return Math.Pow(x[1], a[2]) * Math.Exp(a[0] + a[1] * x[0]);
		}

		#endregion

		#region PARSING

		//Читает данные из файла
		List<double[]> readData(string filename, int numberOfX)
		{
			//Создаем список, куда будем складывать значения
			var list = new List<double[]>();

			using (var reader = new StreamReader(filename))
			{
				string line = reader.ReadLine();
				if (line == null) throw new IOException("Файл пуст");

				//Парсим остальные строки
				while ((line = reader.ReadLine()) != null)
					list.Add(parseString(line, numberOfX));
			}

			return list;
		}

		//Парсинг строки в массив вместе с преобразованием
		double[] parseString(string str, int numberOfX)
		{
			var strs = str.Split(';');
			if (strs.Length != numberOfX+1) throw new WrongNumberOfVariablesException();
			double[] values = new double[strs.Length];

			for (int i = 0; i < strs.Length; i++)
				values[i] = double.Parse(strs[i]);

			return values;
		}

		#endregion

	}
}
