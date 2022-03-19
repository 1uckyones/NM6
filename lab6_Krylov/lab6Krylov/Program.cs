using System;

class Program
{
	static void Main(string[] args)
	{
		double[,] A = { { 0.42016, -16937, 0.10087, -0.28570 }, 
						{ 0.19439, -0.76571, 0.45605, -0.13218 }, 
						{ -0.61729, 0.28952, -0.17253, 0.41974 }, 
						{ -0.20038, 0.7832, -0.47011, 0.13625 } };
		double[,] y = new double[4, 5];


		y[0, 0] = 1;
		Console.WriteLine("Исходная матрица:\n");
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				Console.Write("   " + A[i, j]);
			}

			Console.WriteLine();
		}


		Console.WriteLine("\nВекторы\n");
		for (int k = 0; k < 4; k++)
		{
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					y[i, k + 1] += A[i, j] * y[j, k];
				}
			}
		}
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				Console.Write("\t{0:0.0000}", y[i, j]);
			}

			Console.WriteLine();
		}

		Console.WriteLine("\nЗначения P:");

		for (int i = 0; i < 4; i++)
		{
			double tmp;
			tmp = y[i, i];
			for (int j = 4; j >= i; j--)
            {
				y[i, j] /= tmp;
			}

			for (int j = i + 1; j < 4; j++)
			{
				tmp = y[j, i];
				for (int k = 4; k >= i; k--)
                {
					y[j, k] -= tmp * y[i, k];
				}
			}
		}

		double[] xx = new double[4];
		double[] p = new double[5];

		xx[3] = y[3, 4];
		for (int i = 2; i >= 0; i--)
		{
			xx[i] = y[i, 4];
			for (int j = i + 1; j < 4; j++)
			{
				xx[i] -= y[i, j] * xx[j];
			}
					
		}

		for (int i = 4; i > 0; i--)
		{
			p[i] = xx[4 - i];
		}

		p[0] = 1;

		for (int i = 0; i < 5; i++)
		{
			Console.Write("   {0:0.0000}", p[i] + ((i != 4) ? ", " : ""));
		}
				
		Console.ReadKey();
	}
}

