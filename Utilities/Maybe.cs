using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Maybe<T>
    {
        public readonly bool HasValue;
        public readonly T Value;

        private Maybe(T value)
        {
            this.Value = value;
            this.HasValue = true;
        }

        private Maybe()
        {
            this.HasValue = false;
        }

        public static Maybe<T> Some(T value)
        {
            return new Maybe<T>(value);
        }

        public static Maybe<T> None()
        {
            return new Maybe<T>();
        }
    }
}
