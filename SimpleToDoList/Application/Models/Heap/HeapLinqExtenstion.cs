using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SimpleToDoList.Application.Models.Heap
{
    public static class HeapLinqExtenstion
    {

        public static Heap<T> ToHeap<T>(this IQueryable<T> source, Expression<Func<T, T, int>> comparer)
        {

            var comparerDelegate = comparer.Compile();
            var comparerObject = Comparer<T>.Create((x, y) => comparerDelegate(x, y));

            // Changed the AsEnumerable method to AsQueryable method
            return source.AsQueryable().ToHeap(comparerObject);
        }

        public static Heap<T> ToHeap<T>(this IEnumerable<T> source, IComparer<T> comparer)
        {
            var heap = new Heap<T>(comparer);

            foreach (var item in source)
            {
                heap.Add(item);
            }
            return heap;
        }
    }
}
