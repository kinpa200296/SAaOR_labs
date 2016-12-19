using System;
using System.Collections.Generic;

namespace MathCommon.LinearAlgebra
{
    public class DoubleMatrix : Matrix<double, DoubleVector>
    {
        public DoubleMatrix()
        {
        }

        public DoubleMatrix(int rowCount, int colCount) : base(rowCount, colCount)
        {
        }

        public DoubleMatrix(double[,] data) : base(data)
        {
        }

        public DoubleMatrix(IEnumerable<DoubleVector> data) : base(data)
        {
        }


        protected override DoubleVector CreateEmptyRow()
        {
            return new DoubleVector(ColCount);
        }

        protected override DoubleVector CreateEmptyCol()
        {
            return new DoubleVector(RowCount);
        }


        public static DoubleMatrix One(int size)
        {
            var res = new DoubleMatrix(size, size);
            for (var i = 0; i < size; i++)
            {
                res[i, i] = 1;
            }
            return res;
        }


        #region Operation overloads

        public static DoubleMatrix operator +(DoubleMatrix m1, DoubleMatrix m2)
        {
            CheckSize(m1, m2);
            var matrix = new DoubleMatrix(m1.RowCount, m1.ColCount);
            for (var i = 0; i < matrix.RowCount; i++)
                for (var j = 0; j < matrix.ColCount; j++)
                {
                    matrix[i, j] = m1[i, j] + m2[i, j];
                }
            return matrix;
        }

        public static DoubleMatrix operator -(DoubleMatrix m1, DoubleMatrix m2)
        {
            CheckSize(m1, m2);
            var matrix = new DoubleMatrix(m1.RowCount, m1.ColCount);
            for (var i = 0; i < matrix.RowCount; i++)
                for (var j = 0; j < matrix.ColCount; j++)
                {
                    matrix[i, j] = m1[i, j] - m2[i, j];
                }
            return matrix;
        }

        public static DoubleMatrix operator *(DoubleMatrix m1, DoubleMatrix m2)
        {
            CheckMultiplyDimensions(m1, m2);
            var matrix = new DoubleMatrix(m1.RowCount, m2.ColCount);
            for (var i = 0; i < matrix.RowCount; i++)
                for (var j = 0; j < matrix.ColCount; j++)
                {
                    matrix[i, j] = m1.Row[i]*m2.Col[j];
                }
            return matrix;
        }

        public static DoubleVector operator *(DoubleMatrix m, DoubleVector v)
        {
            CheckMultiplyDimensions(m, v);
            var res = new DoubleVector(m.RowCount);
            for (var i = 0; i < res.Length; i++)
            {
                res[i] = m.Row[i]*v;
            }
            return res;
        }

        public static DoubleVector operator *(DoubleVector v, DoubleMatrix m)
        {
            CheckMultiplyDimensions(v, m);
            var res = new DoubleVector(m.ColCount);
            for (var i = 0; i < res.Length; i++)
            {
                res[i] = v*m.Col[i];
            }
            return res;
        }

        public static DoubleMatrix operator +(DoubleMatrix m, double val)
        {
            var result = new DoubleMatrix(m.RowCount, m.ColCount);
            for (var i = 0; i < result.RowCount; i++)
                for (var j = 0; j < result.ColCount; j++)
                {
                    result[i, j] = m[i, j] + val;
                }
            return result;
        }

        public static DoubleMatrix operator +(double val, DoubleMatrix m)
        {
            return m + val;
        }

        public static DoubleMatrix operator -(DoubleMatrix m, double val)
        {
            var result = new DoubleMatrix(m.RowCount, m.ColCount);
            for (var i = 0; i < result.RowCount; i++)
                for (var j = 0; j < result.ColCount; j++)
                {
                    result[i, j] = m[i, j] - val;
                }
            return result;
        }

        public static DoubleMatrix operator -(double val, DoubleMatrix m)
        {
            var result = new DoubleMatrix(m.RowCount, m.ColCount);
            for (var i = 0; i < result.RowCount; i++)
                for (var j = 0; j < result.ColCount; j++)
                {
                    result[i, j] = val - m[i, j];
                }
            return result;
        }

        public static DoubleMatrix operator *(DoubleMatrix m, double val)
        {
            var result = new DoubleMatrix(m.RowCount, m.ColCount);
            for (var i = 0; i < result.RowCount; i++)
                for (var j = 0; j < result.ColCount; j++)
                {
                    result[i, j] = m[i, j]*val;
                }
            return result;
        }

        public static DoubleMatrix operator *(double val, DoubleMatrix m)
        {
            return m*val;
        }

        public static DoubleMatrix operator /(DoubleMatrix m, double val)
        {
            var result = new DoubleMatrix(m.RowCount, m.ColCount);
            for (var i = 0; i < result.RowCount; i++)
                for (var j = 0; j < result.ColCount; j++)
                {
                    result[i, j] = m[i, j]/val;
                }
            return result;
        }

        #endregion Operation overloads


        private static void CheckSize(DoubleMatrix m1, DoubleMatrix m2)
        {
            if (m1.RowCount != m2.RowCount || m1.ColCount != m2.ColCount)
            {
                throw new ArgumentException("Matrix sizes doesn't match. Cannot continue.");
            }
        }

        private static void CheckMultiplyDimensions(DoubleMatrix m1, DoubleMatrix m2)
        {
            if (m1.ColCount != m2.RowCount)
            {
                throw new ArgumentException("Matrices are inconsistent. Cannot multiply.");
            }
        }

        private static void CheckMultiplyDimensions(DoubleMatrix m, DoubleVector v)
        {
            if (m.ColCount != v.Length)
            {
                throw new ArgumentException("Matrix and vector are inconsistent. Cannot multiply.");
            }
        }

        private static void CheckMultiplyDimensions(DoubleVector v, DoubleMatrix m)
        {
            if (m.RowCount != v.Length)
            {
                throw new ArgumentException("Vector and matrix are inconsistent. Cannot multiply.");
            }
        }
    }
}