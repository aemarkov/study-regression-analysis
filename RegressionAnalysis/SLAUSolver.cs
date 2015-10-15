using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

namespace Mathematics
{
	static class SLAUSolver
	{
		//Решение СЛАУ
		public static double[] Solve(double[][] matrix)
		{
			return calcMatrix(makeTriangle(matrix));
		}

		//Приведение матрицы к верхнему треугольному виду
		private static double[][] makeTriangle(double[][] matrix)
		{
			for (int i = 0; i < matrix.Length - 1; i++)
			{
				//Меняем местами строки, чтобы в текущей строке
				//i-ый элемент был максимальный
				exange(matrix, i);
		
				//Делим текущую строку, чтобы ее i-ый элемент был 1
				matrix[i] = divideRow(matrix, i);

				//Домножаем и вычитаем текущую строку из каждой последующей
				for(int j=i+1; j<matrix.Length; j++)
					SubstractRow(matrix, i, j);

			}
			return matrix;
		}

		//Выполняет обратный ход метода Гаусса - расчет значений
		//по полученной верхней треугольной матрице
		public static double[] calcMatrix(double[][] matrix)
		{
			//printMatrix(matrix);

			int lastIndex = matrix.Length-1;
			double[] x = new double[matrix.Length];

			//Идем от последнего элемента и расчитываем
			//Расчет последнего элемента Xn = Bn /  An
			x[lastIndex] = matrix[lastIndex][lastIndex + 1] / matrix[lastIndex][lastIndex];
			//Debug.WriteLine(x[lastIndex]);

			//Расчет остальных значений
			for(int i=lastIndex - 1; i>=0; i--)
			{
				for (int j = i+1; j <= lastIndex; j++)
					matrix[i][lastIndex + 1] -= matrix[i][j]*x[j];
				x[i] = matrix[i][lastIndex + 1];// / matrix[i][i];
				//Debug.WriteLine(x[i]);
			}


			return x;
		}

		//Меняет местами две строки
		public static void exange(double[][]matrix, int row)
		{
			int index = findMaxRow(matrix, row);
			var temp = matrix[row];
			matrix[row] = matrix[index];
			matrix[index] = temp;
		}

		//Поиск индекса наибольшей строки
		public static int findMaxRow(double[][] matrix, int row)
		{
			double max = matrix[row][row];
			int maxRow = row;
			for(int i = row+1; i<matrix.Length; i++)
			{
				if(matrix[i][row]>max)
				{
					max = matrix[i][row];
					maxRow = i;
				}
			}

			return maxRow;
		}

		//Делит строку матрицы на ее первый элемент, чтобы
		//первый элемент был 1
		public static double[] divideRow(double[][] matrix, int row)
		{
			double a = matrix[row][row];
			if (a == 0) throw new DivideByZeroException();

			return matrix[row].Select(x => x / a).ToArray();
		}

		//Вычитает из очередной строки домноженную текущию
		public static void SubstractRow(double[][] matrix, int currentRow, int row)
		{
			double k = matrix[row][currentRow];
			for(int i = currentRow; i<matrix[row].Length; i++)
			{
				matrix[row][i] -= matrix[currentRow][i] * k;
			}
		}

		//Печатает матрицу
		private static void printMatrix(double[][] matrix)
		{
			foreach(var row in matrix)
			{
				foreach(var x in row)
					Debug.Write(x.ToString("N4")+"\t");
				Debug.WriteLine("");
            }
		}
	}
}
