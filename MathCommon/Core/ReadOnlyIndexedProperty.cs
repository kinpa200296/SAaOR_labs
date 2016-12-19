using System;

namespace MathCommon.Core
{
    public class ReadOnlyIndexedProperty<TIndex, TValue>
    {
        private readonly Func<TIndex, TValue> _getFunc;


        public TValue this[TIndex i] => _getFunc(i);


        public ReadOnlyIndexedProperty(Func<TIndex, TValue> getFunc)
        {
            _getFunc = getFunc;
        }
    }
}