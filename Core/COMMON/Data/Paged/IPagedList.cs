using System.Collections;
using System.Collections.Generic;

namespace System.Data
{
    public interface IPagedList<T> : IPage, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
    {

    }
}
