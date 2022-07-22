using System;

namespace Plantjes.Utilities.Attributes
{
    class ClearableAttribute<T> : Attribute
    {
        public T Value { get; set; }

        public ClearableAttribute(T value = default)
        {
            Value = value;
        }
    }
}