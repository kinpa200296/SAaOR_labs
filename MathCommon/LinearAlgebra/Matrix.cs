using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MathCommon.Core;

namespace MathCommon.LinearAlgebra
{
    public abstract class Matrix<T, TVector> : IEnumerable<TVector> where TVector : Vector<T>
    {
        protected T[,] Data = new T[0, 0];


        public T this[int rowIndex, int colIndex]
        {
            get
            {
                CheckIndexes(rowIndex, colIndex);
                return Data[rowIndex, colIndex];
            }
            set
            {
                CheckIndexes(rowIndex, colIndex);
                Data[rowIndex, colIndex] = value;
            }
        }

        public IndexedProperty<int, TVector> Row { get; }

        public IndexedProperty<int, TVector> Col { get; }

        public TVector this[int rowNumber]
        {
            get { return Col[rowNumber]; }
            set { Col[rowNumber] = value; }
        }

        public int RowCount => Data.GetLength(0);

        public int ColCount => Data.GetLength(1);


        protected Matrix()
        {
            Row = new IndexedProperty<int, TVector>(GetRow, SetRow);
            Col = new IndexedProperty<int, TVector>(GetCol, SetCol);
        }

        protected Matrix(int rowCount, int colCount) : this()
        {
            Data = new T[rowCount, colCount];
        }

        protected Matrix(T[,] data) : this()
        {
            Data = new T[data.GetLength(0), data.GetLength(1)];
            for (var i = 0; i < data.GetLength(0); i++)
                for (var j = 0; j < data.GetLength(1); j++)
                {
                    Data[i, j] = data[i, j];
                }
        }

        protected Matrix(IEnumerable<TVector> data) : this()
        {
            var vectors = data.ToArray();
            Data = new T[vectors.Length, vectors[0].Length];
            for (var i = 0; i < vectors.Length; i++)
            {
                this[i] = vectors[i];
            }
        }


        public IEnumerator<TVector> GetEnumerator()
        {
            return new ColMatrixEnumerator<T, TVector>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        protected abstract TVector CreateEmptyRow();

        protected abstract TVector CreateEmptyCol();


        protected TVector GetRow(int rowIndex)
        {
            CheckRowIndex(rowIndex);
            var vector = CreateEmptyRow();
            for (var i = 0; i < ColCount; i++)
            {
                vector[i] = Data[rowIndex, i];
            }
            return vector;
        }

        protected TVector GetCol(int colIndex)
        {
            CheckColIndex(colIndex);
            var vector = CreateEmptyCol();
            for (var i = 0; i < RowCount; i++)
            {
                vector[i] = Data[i, colIndex];
            }
            return vector;
        }

        protected void SetRow(int rowIndex, Vector<T> v)
        {
            CheckRowIndex(rowIndex);
            CheckFitCol(v);
            for (var i = 0; i < ColCount; i++)
            {
                Data[rowIndex, i] = v[i];
            }
        }

        protected void SetCol(int colIndex, Vector<T> v)
        {
            CheckColIndex(colIndex);
            CheckFitRow(v);
            for (var i = 0; i < RowCount; i++)
            {
                Data[i, colIndex] = v[i];
            }
        }

        protected void SetRow(int rowIndex, TVector v)
        {
            SetRow(rowIndex, v as Vector<T>);
        }

        protected void SetCol(int colIndex, TVector v)
        {
            SetCol(colIndex, v as Vector<T>);
        }


        private void CheckRowIndex(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= RowCount)
            {
                throw new IndexOutOfRangeException("Matrix row index out of range");
            }
        }

        private void CheckColIndex(int colIndex)
        {
            if (colIndex < 0 || colIndex >= ColCount)
            {
                throw new IndexOutOfRangeException("Matrix column index out of range");
            }
        }

        private void CheckIndexes(int rowIndex, int colIndex)
        {
            CheckRowIndex(rowIndex);
            CheckColIndex(colIndex);
        }

        private void CheckFitRow(Vector<T> v)
        {
            if (v.Length != RowCount)
            {
                throw new ArgumentException("Matrix row count and vector length do not match");
            }
        }

        private void CheckFitCol(Vector<T> v)
        {
            if (v.Length != ColCount)
            {
                throw new ArgumentException("Matrix column count and vector length do not match");
            }
        }
    }


    internal class ColMatrixEnumerator<T, TVector> : IEnumerator<TVector> where TVector : Vector<T>
    {
        private readonly Matrix<T, TVector> _matrix;
        private int _index = -1;

        public ColMatrixEnumerator(Matrix<T, TVector> matrix)
        {
            _matrix = matrix;
        }

        public void Dispose() { }

        public bool MoveNext()
        {
            _index++;
            return _index < _matrix.ColCount;
        }

        public void Reset()
        {
            _index = -1;
        }

        public TVector Current => _matrix.Col[_index];

        object IEnumerator.Current => Current;
    }


    internal class RowMatrixEnumerator<T, TVector> : IEnumerator<TVector> where TVector : Vector<T>
    {
        private readonly Matrix<T, TVector> _matrix;
        private int _index = -1;

        public RowMatrixEnumerator(Matrix<T, TVector> matrix)
        {
            _matrix = matrix;
        }

        public void Dispose() { }

        public bool MoveNext()
        {
            _index++;
            return _index < _matrix.RowCount;
        }

        public void Reset()
        {
            _index = -1;
        }

        public TVector Current => _matrix.Row[_index];

        object IEnumerator.Current => Current;
    }
}