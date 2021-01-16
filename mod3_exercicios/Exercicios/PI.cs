namespace Exercicios
{
    class PI
    {
        // i:           0 1 2 3 4  5  6 ... 
        // denominador: 1 3 5 7 9 11 13 ...
        // i*2:         0 2 4 6 8 10 12 ...
        // (i*2)+1      1 3 5 7 9 11 13 ...
        public static double AsDoubleGL(int precision)
        {
            double pi = 0;
            for (int i = 0; i < precision; i++)
            {
                double portion = 4d / ((2 * i) + 1);
                if (i % 2 == 0)
                    pi += portion;
                else
                    pi -= portion;
            }
            return pi;
        }

        public static double AsDoubleN(int precision)
        {
            double pi = 3;
            for (int i = 0; i < precision; i++)
            {
                int x = (i + 1) * 2;
                double portion = 4d / (x * (x+1) * (x+2));
                if (i % 2 == 0)
                    pi += portion;
                else
                    pi -= portion;
            }
            return pi;
        }

        public static double[] AsArray(int precision)
        {
            double[] arr = new double[precision];
            for (int i = 0; i < precision; i++)
            {
                if (precision % 2 == 0 && (i == precision / 2 || i == precision / 2 - 1))
                    continue;
                else if (precision % 2 != 0 && i == precision / 2)
                    continue;
                else
                    arr[i] = AsDoubleGL(i+1);      
            }
            return arr;
        }
    }
}
