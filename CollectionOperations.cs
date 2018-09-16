using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eonae.CollectionExtensions
{
    public static class CollectionOperations
    {
        private static Random rnd = new Random();

        public static (T Element, IEnumerable<T> NewCollection) RandomElementExclude<T>(this IEnumerable<T> list)
        {
            var element = list.RandomElement();
            var temp = list.ToList();
            temp.Remove(element);
            return (element, temp);

        }
        public static T RandomElement<T>(this IEnumerable<T> list)
        {
            int count = list.Count();
            return list.ToArray()[rnd.Next(count)];
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list)
        {
            throw new NotImplementedException();
        }

        /// 1. Перемешивание коллекции
    }
}
