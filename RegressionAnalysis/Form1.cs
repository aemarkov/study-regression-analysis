using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
	}
}
