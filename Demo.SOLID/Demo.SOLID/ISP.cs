using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.SOLID
{
   public interface IMath
    {
        int Sum(int x, int y);
        int Divide(int x, int y);
        int Multiply(int x, int y);

     

    }

    public interface IAdvMath : IMath
    {
        int Pow(int x, int y);
        int SquareRoot(int x);

        double Tan(double x);
    }

    public class SimpleMath : IMath
    {
        public int Divide(int x, int y)
        {
            throw new NotImplementedException();
        }

        public int Multiply(int x, int y)
        {
            throw new NotImplementedException();
        }

        public int Sum(int x, int y)
        {
            throw new NotImplementedException();
        }
    }

    public class AdvancedMath : IAdvMath
    {
        public int Divide(int x, int y)
        {
            throw new NotImplementedException();
        }

        public int Multiply(int x, int y)
        {
            throw new NotImplementedException();
        }

        public int Pow(int x, int y)
        {
            throw new NotImplementedException();
        }

        public int SquareRoot(int x)
        {
            throw new NotImplementedException();
        }

        public int Sum(int x, int y)
        {
            throw new NotImplementedException();
        }

        public double Tan(double x)
        {
            throw new NotImplementedException();
        }
    }
}
