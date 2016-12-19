using System;
using System.Collections.Generic;
using System.Linq;

namespace MathCommon.LinearAlgebra
{
    public class DoubleVector : Vector<double>
    {
        public DoubleVector()
        {
        }

        public DoubleVector(int length) : base(length)
        {
        }

        public DoubleVector(IEnumerable<double> data) : base(data)
        {
        }

        public DoubleVector(double[] data) : base(data)
        {
        }


        public static DoubleVector One(int size, int index)
        {
            return new DoubleVector(size) {[index] = 1};
        }


        #region Operation overloads

        public static DoubleVector operator +(DoubleVector v1, DoubleVector v2)
        {
            CheckLength(v1, v2);
            var v = new DoubleVector(v1.Length);
            for (var i = 0; i < v.Length; i++)
            {
                v[i] = v1[i] + v2[i];
            }
            return v;
        }

        public static DoubleVector operator -(DoubleVector v1, DoubleVector v2)
        {
            CheckLength(v1, v2);
            var v = new DoubleVector(v1.Length);
            for (var i = 0; i < v.Length; i++)
            {
                v[i] = v1[i] - v2[i];
            }
            return v;
        }

        public static double operator *(DoubleVector v1, DoubleVector v2)
        {
            CheckLength(v1, v2);
            return v1.Select((t, i) => t*v2[i]).Sum();
        }

        public static DoubleVector operator +(DoubleVector v, double val)
        {
            return new DoubleVector(v.Select(t => t + val));
        }

        public static DoubleVector operator -(DoubleVector v, double val)
        {
            return new DoubleVector(v.Select(t => t - val));
        }

        public static DoubleVector operator *(DoubleVector v, double val)
        {
            return new DoubleVector(v.Select(t => t*val));
        }
        public static DoubleVector operator +(double val, DoubleVector v)
        {
            return new DoubleVector(v.Select(t => val + t));
        }

        public static DoubleVector operator -(double val, DoubleVector v)
        {
            return new DoubleVector(v.Select(t => val - t));
        }

        public static DoubleVector operator *(double val, DoubleVector v)
        {
            return new DoubleVector(v.Select(t => val*t));
        }

        public static DoubleVector operator /(DoubleVector v, double val)
        {
            return new DoubleVector(v.Select(t => t/val));
        }

        #endregion Operation overloads


        private static void CheckLength(DoubleVector v1, DoubleVector v2)
        {
            if (v1?.Length != v2?.Length)
            {
                throw new ArgumentException("Vector sizes do not match!!!");
            }
        }
    }
}