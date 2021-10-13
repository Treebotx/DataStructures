using System.Collections.Generic;

namespace DataStructures.Library.Sorting
{
    public interface ISorting<T>
    {
        public void Sort(IList<T> listToSort);
    }
}
