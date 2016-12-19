using System;
using System.Collections.Generic;

namespace MathCommon.LinearAlgebra
{
    public class IntMatrix : Matrix<int, IntVector>
    {
        public IntMatrix()
        {
        }

        public IntMatrix(int rowCount, int colCount) : base(rowCount, colCount)
        {
        }

        public IntMatrix(int[,] data) : base(data)
        {
        }

        public IntMatrix(IEnumerable<IntVector> data) : base(data)
        {
        }


        protected override IntVector CreateEmptyRow()
        {
            return new IntVector(ColCount);
        }

        protected override IntVector CreateEmptyCol()
        {
            return new IntVector(RowCount);
        }


        public static IntMatrix One(int size)
        {
            var res = new IntMatrix(size, size);
            for (var i = 0; i < size; i++)
            {
                res[i, i] = 1;
            }
            return res;
        }


        #region Operation overloads

        public static IntMatrix operator +(IntMatrix m1, IntMatrix m2)
        {
            CheckSize(m1, m2);
            var matrix = new IntMatrix(m1.RowCount, m1.ColCount);
            for (var i = 0; i < matrix.RowCount; i++)
                for (var j = 0; j < matrix.ColCount; j++)
                {
                    matrix[i, j] = m1[i, j] + m2[i, j];
                }
            return matrix;
        }

        public static IntMatrix operator -(IntMatrix m1, IntMatrix m2)
        {
            CheckSize(m1, m2);
            var matrix = new IntMatrix(m1.RowCount, m1.ColCount);
            for (var i = 0; i < matrix.RowCount; i++)
                for (var j = 0; j < matrix.ColCount; j++)
                {
                    matrix[i, j] = m1[i, j] - m2[i, j];
                }
            return matrix;
        }

        public static IntMatrix operator *(IntMatrix m1, IntMatrix m2)
        {
            CheckMultiplyDimensions(m1, m2);
            var matrix = new IntMatrix(m1.RowCount, m2.ColCount);
            for (var i = 0; i < matrix.RowCount; i++)
                for (var j = 0; j < matrix.ColCount; j++)
                {
                    matrix[i, j] = m1.Row[i]*m2.Col[j];
                }
            return matrix;
        }

        public static IntVector operator *(IntMatrix m, IntVector v)
        {
            CheckMultiplyDimensions(m, v);
            var res = new IntVector(m.RowCount);
            for (var i = 0; i < res.Length; i++)
            {
                res[i] = m.Row[i]*v;
            }
            return res;
        }

        public static IntVector operator *(IntVector v, IntMatrix m)
        {
            CheckMultiplyDimensions(v, m);
            var res = new IntVector(m.ColCount);
            for (var i = 0; i < res.Length; i++)
            {
                res[i] = v*m.Col[i];
            }
            return res;
        }

        public static IntMatrix operator +(IntMatrix m, int val)
        {
            var result = new IntMatrix(m.RowCount, m.ColCount);
            for (var i = 0; i < result.RowCount; i++)
                for (var j = 0; j < result.ColCount; j++)
                {
                    result[i, j] = m[i, j] + val;
                }
            return result;
        }

        public static IntMatrix operator +(int val, IntMatrix m)
        {
            return m + val;
        }

        public static IntMatrix operator -(IntMatrix m, int val)
        {
            var result = new IntMatrix(m.RowCount, m.ColCount);
            for (var i = 0; i < result.RowCount; i++)
                for (var j = 0; j < result.ColCount; j++)
                {
                    result[i, j] = m[i, j] - val;
                }
            return result;
        }

        public static IntMatrix operator -(int val, IntMatrix m)
        {
            var result = new IntMatrix(m.RowCount, m.ColCount);
            for (var i = 0; i < result.RowCount; i++)
                for (var j = 0; j < result.ColCount; j++)
                {
                    result[i, j] = val - m[i, j];
                }
            return result;
        }

        public static IntMatrix operator *(IntMatrix m, int val)
        {
            var result = new IntMatrix(m.RowCount, m.ColCount);
            for (var i = 0; i < result.RowCount; i++)
                for (var j = 0; j < result.ColCount; j++)
                {
                    result[i, j] = m[i, j]*val;
                }
            return result;
        }

        public static IntMatrix operator *(int val, IntMatrix m)
        {
            return m * val;
        }

        public static IntMatrix operator /(IntMatrix m, int val)
        {
            var result = new IntMatrix(m.RowCount, m.ColCount);
            for (var i = 0; i < result.RowCount; i++)
                for (var j = 0; j < result.ColCount; j++)
                {
                    result[i, j] = m[i, j]/val;
                }
            return result;
        }

        #endregion Operation overloads


        private static void CheckSize(IntMatrix m1, IntMatrix m2)
        {
            if (m1.RowCount != m2.RowCount || m1.ColCount != m2.ColCount)
            {
                throw new ArgumentException("Matrix sizes doesn't match. Cannot continue.");
            }
        }

        private static void CheckMultiplyDimensions(IntMatrix m1, IntMatrix m2)
        {
            if (m1.ColCount != m2.RowCount)
            {
                throw new ArgumentException("Matrices are inconsistent. Cannot multiply.");
            }
        }

        private static void CheckMultiplyDimensions(IntMatrix m, IntVector v)
        {
            if (m.ColCount != v.Length)
            {
                throw new ArgumentException("Matrix and vector are inconsistent. Cannot multiply.");
            }
        }

        private static void CheckMultiplyDimensions(IntVector v, IntMatrix m)
        {
            if (m.RowCount != v.Length)
            {
                throw new ArgumentException("Vector and matrix are inconsistent. Cannot multiply.");
            }
        }
    }
}