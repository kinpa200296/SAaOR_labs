using System;
using System.Collections.Generic;
using System.Linq;

namespace MathCommon.LinearAlgebra
{
    public class IntVector : Vector<int>
    {
        public IntVector()
        {
        }

        public IntVector(int length) : base(length)
        {
        }

        public IntVector(IEnumerable<int> data) : base(data)
        {
        }

        public IntVector(int[] data) : base(data)
        {
        }


        public static IntVector One(int size, int index)
        {
            return new IntVector(size) { [index] = 1 };
        }


        #region Operation overloads

        public static IntVector operator +(IntVector v1, IntVector v2)
        {
            CheckLength(v1, v2);
            var v = new IntVector(v1.Length);
            for (var i = 0; i < v.Length; i++)
            {
                v[i] = v1[i] + v2[i];
            }
            return v;
        }

        public static IntVector operator -(IntVector v1, IntVector v2)
        {
            CheckLength(v1, v2);
            var v = new IntVector(v1.Length);
            for (var i = 0; i < v.Length; i++)
            {
                v[i] = v1[i] - v2[i];
            }
            return v;
        }

        public static int operator *(IntVector v1, IntVector v2)
        {
            CheckLength(v1, v2);
            return v1.Select((t, i) => t*v2[i]).Sum();
        }

        public static IntVector operator +(IntVector v, int val)
        {
            return new IntVector(v.Select(t => t + val));
        }

        public static IntVector operator -(IntVector v, int val)
        {
            return new IntVector(v.Select(t => t - val));
        }

        public static IntVector operator *(IntVector v, int val)
        {
            return new IntVector(v.Select(t => t*val));
        }
        public static IntVector operator +(int val, IntVector v)
        {
            return new IntVector(v.Select(t => val + t));
        }

        public static IntVector operator -(int val, IntVector v)
        {
            return new IntVector(v.Select(t => val - t));
        }

        public static IntVector operator *(int val, IntVector v)
        {
            return new IntVector(v.Select(t => val*t));
        }

        public static IntVector operator /(IntVector v, int val)
        {
            return new IntVector(v.Select(t => t/val));
        }

        #endregion Operation overloads


        private static void CheckLength(IntVector v1, IntVector v2)
        {
            if (v1?.Length != v2?.Length)
            {
                throw new ArgumentException("Vector sizes do not match!!!");
            }
        }
    }
}