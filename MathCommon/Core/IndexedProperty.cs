using System;

namespace MathCommon.Core
{
    public class IndexedProperty<TIndex, TValue>
    {
        private readonly Func<TIndex, TValue> _getFunc;
        private readonly Action<TIndex, TValue> _setAction;


        public TValue this[TIndex i]
        {
            get { return _getFunc(i); }
            set { _setAction(i, value); }
        }


        public IndexedProperty(Func<TIndex, TValue> getFunc, Action<TIndex, TValue> setAction)
        {
            _getFunc = getFunc;
            _setAction = setAction;
        }
    }
}