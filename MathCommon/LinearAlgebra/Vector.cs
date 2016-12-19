using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MathCommon.LinearAlgebra
{
    public abstract class Vector<T> : IEnumerable<T>
    {
        protected T[] Data = new T[0];


        public int Length => Data.Length;
        
        public T this[int index]
        {
            get
            {
                CheckIndex(index);
                return Data[index];
            }
            set
            {
                CheckIndex(index);
                Data[index] = value;
            }
        }


        protected Vector()
        {
        }

        protected Vector(int length)
        {
            Data = new T[length];
        }

        protected Vector(IEnumerable<T> data)
        {
            Data = data.ToArray();
        }

        protected Vector(T[] data)
        {
            Data = new T[data.Length];
            data.CopyTo(Data, 0);
        }


        public IEnumerator<T> GetEnumerator()
        {
            return new VectorEnumerator<T>(Data);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        private void CheckIndex(int index)
        {
            if (index < 0 || index >= Length)
            {
                throw new IndexOutOfRangeException("Vector index is out of range");
            }
        }
    }


    internal class VectorEnumerator<T> : IEnumerator<T>
    {
        private readonly T[] _data;
        private int _index = -1;


        public T Current => _data[_index];

        object IEnumerator.Current => Current;


        public VectorEnumerator(T[] data)
        {
            _data = new T[data.Length];
            data.CopyTo(_data, 0);
        }


        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            _index++;
            return _index < _data.Length;
        }

        public void Reset()
        {
            _index = -1;
        }
    }
}