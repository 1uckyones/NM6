using System;

class Program
{
    static void Main(string[] args)
    {
        int n = 4;
        double[,] a = new double[4, 4]{ { 0.42016, -16937, 0.10087, -0.28570 },
                                        { 0.19439, -0.76571, 0.45605, -0.13218 },
                                        { -0.61729, 0.28952, -0.17253, 0.41974 },
                                        { -0.20038, 0.7832, -0.47011, 0.13625 } };

        double[,] ak = a;

		double[] s = new double[n];
        double[] c = new double[n];

        double[] λ = new double[n];


		Console.WriteLine("Исходная матрица:\n");
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				Console.Write("   " + a[i, j]);
			}

			Console.WriteLine();
		}


		for (int j = 0; j < n; j++)
        {
            s[0] += a[j, j];
        }
        c[0] = s[0];

        for (int i = 1; i < n; i++)
        {
            ak = Multiplication(ak, a);

            for (int j = 0; j < n; j++)
                s[i] += ak[j, j];

            double sum = 0;
            for (int k = 0, l = i - 1; (k < i) && (l > -1); k++, l--)
                sum += c[k] * s[l];

            c[i] = (s[i] - sum) / (i + 1);
        }

		Console.WriteLine("\nЗначения P:\n");
		Console.Write("\t" + 1.0000);
        for (int i = 0; i < n; i++)
        {
            Console.Write(" {0:0.0000}", c[i]);
        }

        Console.ReadKey();
    }

    public static double[,] Multiplication(double[,] a, double[,] b)
    {
        double[,] ak = new double[a.GetLength(0),a.GetLength(1)];
        for(int i = 0; i < a.GetLength(0); i++)
        {
            for (int j = 0; j < a.GetLength(1); j++)
            {
                for (int k = 0; k < a.GetLength(0); k++)
                    ak[i, j] += a[i, k] * b[k, j];
            }
        }     
            
        return ak;
    }

    public static double[,] Subdivision(double[,] a, double c)
    {
        double[,] ak = new double[a.GetLength(0), a.GetLength(0)];

        for (int i = 0; i < a.GetLength(0); i++)
            for (int j = 0; j < a.GetLength(0); j++)
                ak[i, j] = a[i, j];

        for (int i = 0; i < a.GetLength(0); i++)
                ak[i, i] = a[i, i] - c;

        return ak;
    }

    public static double[] ModalLambda(double[,] d)
    {
        double[] lambda = new double[d.GetLength(0)];

        double[,] detz = new double[3, 3];
        double[] c = new double[4];

        for (int k = 0; k < 4; k++)
        {

            for (int i = 0, h = 0; i < 4; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    if (i != k)
                    {
                        detz[h, j - 1] = d[i, j];
                    }
                }

                if (i != k)
                {
                    h++;

                }

                c[k] = Determination(detz);
            }

            lambda[k] = d[k, 0] * c[k];
        }

        double s = 0;
        for (int i = 0; i < 4; i++)
        {
            if (i == 2 | i == 0)
            {
                s += lambda[i];
            }
            else
            {
                s -= lambda[i];
            }
        }

        s = s / c[3];
            
        lambda[2] = -(-1*c[0]) / c[3];
        lambda[1] = -c[1] / c[3];
        lambda[0] = -(-1*c[2]) / c[3];
        lambda[3] = -s; 
            
        return lambda;
    }

    public static double Determination(double[,] determ)
    {
        return determ[0, 0] * determ[1, 1] * determ[2, 2] + determ[0, 2] * 
            determ[1, 0] * determ[2, 1] + determ[0, 1] * determ[1, 2] * 
            determ[2, 0] - determ[0, 2] * determ[1, 1] * determ[2, 0] -
            determ[0, 1] * determ[1, 0] * determ[2, 2] - determ[0, 0] *
            determ[1, 2] * determ[2, 1];
    }

    public static double Normal(double[] x0, double[] x1)
    {
        double sum = 0;
        for (int i = 0; i < x0.Length; i++)
        {
            sum += Math.Pow((x1[i] - x0[i]), 2);
        }

        double normal = Math.Sqrt(sum);

        return normal;
    }
}