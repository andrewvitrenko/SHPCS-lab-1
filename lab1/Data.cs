namespace lab1;

public class Data(bool isLarge, int N)
{
    public static int Read(string message)
    {
        Console.Write(message);

        string input = Console.ReadLine();

        return Convert.ToInt32(input);
    }

    private int[,] ReadMulti(int N)
    {
        int[,] matrix = new int[N, N];

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                matrix[i, j] = Read($"Enter value for {i + 1},{j + 1}: ");
            }
        }
        
        return matrix;
    }

    private int[] ReadLinear(int N)
    {
        int[] vector = new int[N];

        for (int i = 0; i < N; i++)
        {
            vector[i] = Read($"Enter value for {i + 1}: ");
        }

        return vector;
    }

    public void Write(string filename, string prefix, int[] data)
    {
        string output = string.Join(", ", data);
        
        if (isLarge)
        {
            File.WriteAllText(filename, output);
        }
        else
        {
            Console.WriteLine($"{prefix}: {output}");
        }
    }

    public void Write(string filename, string prefix, int data)
    {
        if (isLarge)
        {
            File.WriteAllText(filename, data.ToString());
        }
        else
        {
            Console.WriteLine($"{prefix}: {prefix}");
        }
    }

    private int[,] Multiply(int[,] matrixA, int[,] matrixB)
    {
        int rowsA = matrixA.GetLength(0);
        int colsA = matrixA.GetLength(1);
        int colsB = matrixB.GetLength(1);
        int[,] result = new int[rowsA, colsB];

        for (int i = 0; i < rowsA; i++)
        {
            for (int j = 0; j < colsB; j++)
            {
                int temp = 0;
                for (int k = 0; k < colsA; k++)
                {
                    temp += matrixA[i, k] * matrixB[k, j];
                }

                result[i, j] = temp;
            }
        }

        return result;
    }

    private int[] Multiply(int[] vector, int[,] matrix)
    {
        int[,] matrixB = new int[1, vector.Length];

        for (int i = 0; i < vector.Length; i++)
        {
            matrixB[0, i] = vector[i];
        }

        int[,] mul = Multiply(matrixB, matrix);

        int[] result = new int[mul.GetLength(1)];

        for (int i = 0; i < mul.GetLength(1); i++)
        {
            result[i] = mul[0, i];
        }

        return result;
    }

    private int[] Multiply(int scalar, int[] vector)
    {
        int[] result = new int[vector.Length];

        for (int i = 0; i < vector.Length; i++)
        {
            result[i] = vector[i] * scalar;
        }
        
        return result;
    }

    private int[] Add(int[] vectorA, int[] vectorB)
    {
        int rows = vectorA.Length;
        int[] result = new int[rows];

        for (int i = 0; i < rows; i++)
        {
            result[i] = vectorA[i] + vectorB[i];
        }
        
        return result;
    }

    private int[,] Sub(int[,] matrixA, int[,] matrixB)
    {
        int rows = matrixA.GetLength(0);
        int cols = matrixA.GetLength(1);
        int[,] result = new int[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result[i, j] = matrixA[i, j] - matrixB[i, j];
            }
        }

        return result;
    }

    private int[,] GenerateMatrix()
    {
        if (!isLarge)
        {
            return ReadMulti(N);
        }
        
        int[,] result = new int[N, N];

        Random rnd = new Random();

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                result[i, j] = rnd.Next(-100, 100);
            }
        }

        return result;
    }

    private int[] GenerateVector()
    {
        if (!isLarge)
        {
            return ReadLinear(N);
        }
        
        int[] result = new int[N];

        Random rnd = new Random();

        for (int i = 0; i < N; i++)
        {
            result[i] = rnd.Next(-100, 100);
        }

        return result;
    }

    private int Max(int[] vector)
    {
        return vector.Max();
    }

    private int Max(int[,] matrix)
    {
        int max = matrix[0, 0];

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                max = Math.Max(max, matrix[i, j]);
            }
        }
        
        return max;
    }

    public int[] F1()
    {
        int[] A = GenerateVector();
        int[] B = GenerateVector();
        int[] C = GenerateVector();
        int[] X = GenerateVector();
        int[,] MA = GenerateMatrix();
        int[,] MD = GenerateMatrix();

        int maxA = Max(A);
        int[,] MA_MD = Multiply(MA, MD);
        int[] B_MA_MD = Multiply(B, MA_MD);

        int[] X_B_C = Add(X, Add(B_MA_MD, C));


        return Multiply(maxA, X_B_C);
    }

    public int F2()
    {
        int[,] MH = GenerateMatrix();
        int[,] MK = GenerateMatrix();
        int[,] ML = GenerateMatrix();

        int[,] MH_MK = Multiply(MH, MK);
        int[,] MH_MK_ML = Sub(MH_MK, ML);

        return Max(MH_MK_ML);
    }

    public int[] F3()
    {
        int[] R = GenerateVector();
        int[] V = GenerateVector();

        int[] R_V = Add(R, V);

        int[,] MO = GenerateMatrix();
        int[,] MP = GenerateMatrix();

        int[,] MO_MP = Multiply(MO, MP);

        return Multiply(R_V, MO_MP);
    }
}
