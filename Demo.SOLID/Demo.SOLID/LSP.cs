using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.SOLID
{
    public interface IArea
    {
        int GetArea();
    }
    public class Rectangle : IArea
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public int GetArea() => Width * Height;
    }

    public class Square : IArea //: Rectangle
    {
        public int EdgeLength { get; set; }

        public int GetArea() => EdgeLength * EdgeLength;

    }



    public class Sample
    {
        public void AreaFunction(IArea area)
        {
        
        }
    }
}
