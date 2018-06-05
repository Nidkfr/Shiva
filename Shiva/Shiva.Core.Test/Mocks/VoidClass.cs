using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shiva.Mocks
{
    public abstract class AbstractVoidClass
    {
        public object Value
        {
            get;
            set;
        }
    }

    public abstract class AbstractVoidClass<T>: AbstractVoidClass
    {
       new public T Value
        {
            get
            {
                return (T)base.Value;
            }
            set
            {
                base.Value = value;
            }
        }
    }

    public class VoidClass1<T> : AbstractVoidClass<T>
    {
        public VoidClass1(Shiva.Core.Services.ILogger logm)
        {
        }
    }

    public class VoidClass2<T> : AbstractVoidClass<T>
    {
    }
}
