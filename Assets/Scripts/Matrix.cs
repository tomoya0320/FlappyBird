using System.Collections.Generic;


public class Matrix
{
    private float[,] data;
    private int width;
    private int height;
    public Matrix(float[,] data)
    {
        this.data = data;
        height = data.GetLength(0);
        width = data.GetLength(1);
    }
    public Matrix mtimes(Matrix other)
    {
        float[,] tmp = new float[height, other.width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < other.width; j++)
            {
                tmp[i, j] = 0;
                for (int m = 0; m < width; m++)
                {
                    tmp[i, j] += data[i, m] * other.data[m, j];
                }
            }
        }
        return new Matrix(tmp);
    }
    public Matrix mtimes(float other)
    {
        float[,] tmp = new float[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                tmp[i, j] = data[i, j] * other;
            }
        }
        return new Matrix(tmp);
    }
    public Matrix mtimes(float other,float maxValue)
    {
        float[,] tmp = new float[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                tmp[i, j] = data[i, j] * other;
                if (tmp[i, j] > maxValue)
                    tmp[i, j] = maxValue;
                if (tmp[i, j] < -maxValue)
                    tmp[i, j] = -maxValue;
            }
        }
        return new Matrix(tmp);
    }
    public Matrix minus(Matrix other)
    {
        float[,] tmp = new float[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                tmp[i, j] = data[i, j] - other.data[i, j];
            }
        }
        return new Matrix(tmp);
    }
    public Matrix minus(Matrix other,float maxValue)
    {
        float[,] tmp = new float[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                tmp[i, j] = data[i, j] - other.data[i, j];
                if (tmp[i, j] > maxValue)
                    tmp[i, j] = maxValue;
                if (tmp[i, j] < -maxValue)
                    tmp[i, j] = -maxValue;
            }
        }
        return new Matrix(tmp);
    }
    public Matrix plus(Matrix other)
    {
        float[,] tmp = new float[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                tmp[i, j] = data[i, j] + other.data[i, j];
            }
        }
        return new Matrix(tmp);
    }
    public Matrix plus(Matrix other,float maxValue)
    {
        float[,] tmp = new float[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                tmp[i, j] = data[i, j] + other.data[i, j];
                if (tmp[i, j] > maxValue)
                    tmp[i, j] = maxValue;
                if (tmp[i, j] < -maxValue)
                    tmp[i, j] = -maxValue;
            }
        }
        return new Matrix(tmp);
    }
    public int getSize(int d)
    {
        if (d == 0)
        {
            return height;
        }
        else if (d == 1)
        {
            return width;
        }

        return 0;
    }
    public float get(int i, int j)
    {
        return data[i, j];
    }
    public void set(float d, int i, int j)
    {
        data[i, j] = d;
    }
    public List<Matrix> getRowList()
    {
        List<Matrix> matrixList = new List<Matrix>();
        for (int i = 0; i < height; i++)
        {
            float[,] tmp = new float[1, width];
            for (int j = 0; j < width; j++)
            {
                tmp[0, j] = data[i, j];
            }
            matrixList.Add(new Matrix(tmp));
        }
        return matrixList;
    }
}