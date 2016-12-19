using System;

namespace MathCommon.Core
{
    public class WriteOnlyIndexedProperty<TIndex, TValue>
    {
        private readonly Action<TIndex, TValue> _setAction;


        public TValue this[TIndex i]
        {
            set { _setAction(i, value); }
        }


        public WriteOnlyIndexedProperty(Action<TIndex, TValue> setAction)
        {
            _setAction = setAction;
        }
    }
}