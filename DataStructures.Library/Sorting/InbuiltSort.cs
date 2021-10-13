using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Library.Sorting
{
    public class InbuiltSort<T> : ISorting<T> where T : IComparable<T>
    {
        public void Sort(IList<T> listToSort)
        {
            ArrayList.Adapter((IList)listToSort).Sort();
        }
    }
}
