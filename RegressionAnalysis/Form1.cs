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
			
			
		}

		//Читает данные из файла
		//Осуществляет необходимые замены переменных,
		//приводит данные к употребимому виду
		double[][] readData(string filename, Func<double, double>[] convertors)
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

			return list.ToArray();
		}

		//Парсинг строки в массив вместе с преобразованием
		double[] parseString(string str, Func<double, double>[] convertors)
		{
			var strs = str.Split(';');
			if (strs.Length != convertors.Length) throw new WrongNumberOfVariablesException();
			double[] values = new double[strs.Length];

			for(int i= 0; i<strs.Length; i++)
				values[i] = convertors[i](Double.Parse(strs[i]));

			return values;
		}

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
			return  Math.Log(y);
		}
	}
}
