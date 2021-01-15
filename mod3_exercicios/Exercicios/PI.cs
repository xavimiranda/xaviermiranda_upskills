namespace Exercicios
{
    class PI
    {
        // i:           0 1 2 3 4  5  6 ... 
        // denominador: 1 3 5 7 9 11 13 ...
        // i*2:         0 2 4 6 8 10 12 ...
        // (i*2)+1      1 3 5 7 9 11 13 ...
        public static double AsDouble(int precision)
        {
            double pi = 0;
            for (int i = 0; i < precision; i++)
            {
                double cur = 1d / ((2 * i) + 1);
                if (i % 2 == 0)
                    pi += cur;
                else
                    pi -= cur;
            }
            return pi * 4;
        }

        public static double[] AsArray(int precision)
        {
            double[] arr = new double[precision];
            for (int i = 0; i < precision; i++)
            {
                arr[i] = AsDouble(i);
            }
            return arr;
        }
    }
}
