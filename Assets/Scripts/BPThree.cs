using UnityEngine;

public class BPThree
{
    public int input_num;// 输入层结点数
    public int hide_num; // 隐层结点数
    public int output_num; // 输出层结点数
    public float learnRateMax;
    public float learnRateMin;
    // 学习率
    public Matrix hide_weight;// 隐层权值
    public Matrix output_weight;// 输出层权值
    public Matrix hide_theta;// 隐层阈值
    public Matrix output_theta;// 输出层阈值
    public float count;//当前迭代次数
    public float totalTime;//总迭代次数
    public static float MaxWeight = 1.0f;

    public BPThree(int input_num, int hide_num, int output_num)
    {
        // TODO Auto-generated constructor stub
        this.input_num = input_num;
        this.hide_num = hide_num;
        this.output_num = output_num;

        learnRateMax = 10;
        learnRateMin = 0.1f;

        float[,] hideW = new float[input_num, hide_num];
        for (int i = 0; i < input_num; i++)
        {
            for (int j = 0; j < hide_num; j++)
            {
                hideW[i, j] = Random.Range(-MaxWeight, MaxWeight);
            }
        }
        hide_weight = new Matrix(hideW);
        float[,] hideT = new float[1, hide_num];
        for (int i = 0; i < hide_num; i++)
        {
            hideT[0, i] = Random.Range(-MaxWeight, MaxWeight);
        }
        hide_theta = new Matrix(hideT);

        float[,] outputW = new float[hide_num, output_num];
        for (int i = 0; i < hide_num; i++)
        {
            for (int j = 0; j < output_num; j++)
            {
                outputW[i, j] = Random.Range(-MaxWeight, MaxWeight);
            }
        }
        output_weight = new Matrix(outputW);
        float[,] outputT = new float[1, output_num];
        for (int i = 0; i < output_num; i++)
        {
            outputT[0, i] = Random.Range(-MaxWeight, MaxWeight);
        }
        output_theta = new Matrix(outputT);
    }
    public BPThree mtimes(float scale,float maxValue)
    {
        BPThree tmp = this;
        tmp.hide_weight = tmp.hide_weight.mtimes(scale, maxValue);
        tmp.output_weight = tmp.output_weight.mtimes(scale, maxValue);
        return tmp;
    }
    public BPThree plus(BPThree other, float maxValue)
    {
        BPThree tmp = this;
        tmp.hide_weight = tmp.hide_weight.plus(other.hide_weight, maxValue);
        tmp.output_weight = tmp.output_weight.plus(other.output_weight, maxValue);
        return tmp;
    }
    public BPThree minus(BPThree other, float maxValue)
    {
        BPThree tmp = this;
        tmp.hide_weight = tmp.hide_weight.minus(other.hide_weight, maxValue);
        tmp.output_weight = tmp.output_weight.minus(other.output_weight, maxValue);
        return tmp;
    }
    public BPThree mtimes(float scale)
    {
        BPThree tmp = this;
        tmp.hide_weight = tmp.hide_weight.mtimes(scale);
        tmp.output_weight = tmp.output_weight.mtimes(scale);
        return tmp;
    }
    public BPThree plus(BPThree other)
    {
        BPThree tmp = this;
        tmp.hide_weight = tmp.hide_weight.plus(other.hide_weight);
        tmp.output_weight = tmp.output_weight.plus(other.output_weight);
        return tmp;
    }
    public BPThree minus(BPThree other)
    {
        BPThree tmp = this;
        tmp.hide_weight = tmp.hide_weight.minus(other.hide_weight);
        tmp.output_weight = tmp.output_weight.minus(other.output_weight);
        return tmp;
    }
    // 计算实际输出
    public Matrix calculateOutput(Matrix input, Matrix weight, Matrix theta)
    {
        Matrix tMatrix = input.mtimes(weight).minus(theta);
        for (int i = 0; i < tMatrix.getSize(0); i++)
        {
            for (int j = 0; j < tMatrix.getSize(1); j++)
            {
                tMatrix.set(sigmoid(tMatrix.get(i, j)), i, j);
            }
        }
        return tMatrix;
    }

    // 计算误差
    public float calculateError(Matrix outputs, Matrix answers)
    {
        float error = 0;
        for (int i = 0; i < output_num; i++)
        {
            error += 0.5f * Mathf.Pow((answers.get(0, i) - outputs.get(0, i)), 2);
        }
        return error;
    }

    // 更新权值和阈值
    public void updateWeight(Matrix inputs1, Matrix inputs2, Matrix outputs, Matrix answers)
    {
        for (int i = 0; i < output_weight.getSize(0); i++)
        {
            for (int j = 0; j < output_weight.getSize(1); j++)
            {
                float tmp = output_weight.get(i, j);
                tmp += (learnRateMin + (learnRateMax - learnRateMin) * (count / totalTime))
                        * (answers.get(0, j) - outputs.get(0, j))
                        * outputs.get(0, j) * (1 - outputs.get(0, j)) *
                        inputs2.get(0, i);
                output_weight.set(tmp, i, j);
            }
        }
        for (int i = 0; i < output_theta.getSize(0); i++)
        {
            for (int j = 0; j < output_theta.getSize(1); j++)
            {
                float tmp = output_theta.get(i, j);
                tmp += (learnRateMin + (learnRateMax - learnRateMin) * (count / totalTime))
                        * (answers.get(0, j) - outputs.get(0, j))
                        * outputs.get(0, j) * (1 - outputs.get(0, j)) * -1;
                output_theta.set(tmp, i, j);
            }
        }
        for (int i = 0; i < hide_weight.getSize(0); i++)
        {
            for (int j = 0; j < hide_weight.getSize(1); j++)
            {
                float tmp = hide_weight.get(i, j);
                float d = 0;
                for (int k = 0; k < output_weight.getSize(1); k++)
                {
                    d += output_weight.get(j, k) * (answers.get(0, k) -
                            outputs.get(0, k)) * outputs.get(0, k) *
                            (1 - outputs.get(0, k));
                }
                tmp += (learnRateMin + (learnRateMax - learnRateMin) * (count / totalTime))
                        * d * inputs2.get(0, j) * (1 - inputs2.get(0, j))
                        * inputs1.get(0, i);
                hide_weight.set(tmp, i, j);
            }
        }
        for (int i = 0; i < hide_theta.getSize(0); i++)
        {
            for (int j = 0; j < hide_theta.getSize(1); j++)
            {
                float tmp = hide_theta.get(i, j);
                float d = 0;
                for (int k = 0; k < output_weight.getSize(1); k++)
                {
                    d += output_weight.get(j, k) * (answers.get(0, k) -
                            outputs.get(0, k)) * outputs.get(0, k) *
                            (1 - outputs.get(0, k));
                }
                tmp += (learnRateMin + (learnRateMax - learnRateMin) * (count / totalTime))
                        * d * inputs2.get(0, j) * (1 - inputs2.get(0, j)) * -1;
                hide_theta.set(tmp, i, j);
            }
        }
    }

    // 训练数据集
    public void trainData(Matrix inputs, Matrix answers, int time)
    {
        totalTime = time;
        for (count = 0; count < totalTime; count++)
        {
            for (int j = 0; j < inputs.getSize(0); j++)
            {
                Matrix output1 = calculateOutput(inputs.getRowList()[j], hide_weight, hide_theta);
                Matrix output2 = calculateOutput(output1, output_weight, output_theta);
                float error = calculateError(output2, answers.getRowList()[j]);
                if (error < 0.000001)
                    break;
                updateWeight(inputs.getRowList()[j], output1, output2, answers.getRowList()[j]);
            }
        }
    }
    // 测试新的数据
    public Matrix testData(Matrix inputs)
    {
        Matrix output1 = calculateOutput(inputs, hide_weight, hide_theta);
        Matrix output2 = calculateOutput(output1, output_weight, output_theta);
        return output2;
    }
    // 激励函数
    public float sigmoid(float src)
    {
        return 1.0f / (1.0f + Mathf.Exp(-src));
    }
}
