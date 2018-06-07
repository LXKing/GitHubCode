using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Collections.Generic
{
    /// <summary>
    /// Enumerable可枚举的扩展方法
    /// </summary>
    public static class IEnumerableEx
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> instance, Action<T> action)
        {
            using (IEnumerator<T> e1 = instance.GetEnumerator())
            {
                while (e1.MoveNext())
                {
                    action(e1.Current);
                    yield return e1.Current;
                }
            }
            //try
            //{
                
            //    //foreach (T local in instance)
            //    //{
            //    //    action(local);
            //    //}
            //    //return instance;
            //}
            //catch (Exception ex)
            //{
            //    COMMON.Logs.Log.WriteException("异常",ex);
            //    throw ex;
            //}
        }
        /// <summary>
        /// 转成字符串，使用固定分割分进行分割
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="splitChars"></param>
        /// <returns></returns>
        public static string SplitByChars(this IEnumerable<string> instance,string splitChars)
        {
            return string.Join(splitChars, instance.Select(x => x.ToString()));
        }
        /// <summary>
        /// 两个集合对应索引的元素拼接(集合1:a1,a2 集合2:b1,b2  结果:a1,b1,a2,b2)
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> Alternate<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            using (IEnumerator<TSource> e1 = first.GetEnumerator())
            using (IEnumerator<TSource> e2 = second.GetEnumerator())
                while (e1.MoveNext() && e2.MoveNext())
                {
                    yield return e1.Current;
                    yield return e2.Current;
                }
        }
        /// <summary>
        /// 在一个集合后面追加一个集合
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> Append<TSource>(this IEnumerable<TSource> source, TSource element)
        {
            using (IEnumerator<TSource> e1 = source.GetEnumerator())
            {
                while (e1.MoveNext())
                    yield return e1.Current;
            }
            yield return element;
        }
        /// <summary>
        /// 在一个集合前面追加一个集合
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> Prepend<TSource>(this IEnumerable<TSource> source, TSource element)
        {
            yield return element;

            using (IEnumerator<TSource> e1 = source.GetEnumerator())
                while (e1.MoveNext())
                    yield return e1.Current;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {

            HashSet<TKey> seenKeys = new HashSet<TKey>();

            foreach (TSource element in source)
            {

                if (seenKeys.Add(keySelector(element)))
                {

                    yield return element;

                }

            }

        }
    }
}
