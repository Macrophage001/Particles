using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using Cysharp.Text;

namespace Particles.Scripts.Extensions
{
    public static class Utils
    {
        public delegate Either<Exception, T> Try<T>();
        
        private static Utf16ValueStringBuilder m_ZStringBuilder = ZString.CreateStringBuilder();
        private static StringBuilder m_StringBuilder = new StringBuilder();

        public static string ToStringF<T0, T1>(Func<(T0, T1, string)> _dataFormatter)
        {
            m_StringBuilder.Clear();
            return _dataFormatter.Map(_tuple =>
                m_StringBuilder.AppendFormat(_tuple.Item3, _tuple.Item1, _tuple.Item2).ToString())();
        }

        public static string ToStringF<TIn, T0, T1>(TIn _in, Func<TIn, (T0, T1, string)> _dataFormatter)
        {
            m_ZStringBuilder.Clear();
            return _dataFormatter(_in).Map(_tuple =>
            {
                m_ZStringBuilder.AppendFormat(_tuple.Item3, _tuple.Item1, _tuple.Item2);
                return m_ZStringBuilder.ToString();
            });
        }
        
        public static TData Using<TDisposable, TData>(TDisposable _disposable, Func<TDisposable, TData> _func)
            where TDisposable : IDisposable
        {
            using (_disposable) return _func(_disposable);
        }

        public static TData TryUsing<TDisposable, TData>(
            TDisposable _disposable,
            Func<bool> _pred,
            Func<TDisposable, TData> _true,
            Func<TData> _false)
            where TDisposable : IDisposable
        {
            if (_pred())
            {
                return Using(_disposable, _true);
            }

            return _false();
        }

        public static void Using<TDisposable>(TDisposable _disposable, Action<TDisposable> _action)
            where TDisposable : IDisposable
        {
            using (_disposable) _action(_disposable);
        }

        public static long Time<T>(Func<T> _func)
        {
            Stopwatch _ = Stopwatch.StartNew();
            _func();
            return _.ElapsedMilliseconds;
        }
    }

    public static class Helper
    {
        /// <summary>
        /// Returns the number of digits in the double.
        /// </summary>
        /// <param name="_d"></param>
        /// <param name="_i"></param>
        /// <param name="_filter">To calculate order of magnitude based off specific number</param>
        /// <returns></returns>
        public static int Length(this double _d, Func<int> _filter=null) => (int) Math.Floor(Math.Log10(_d) / _filter());

        /// <summary>
        /// Shrinks double, bringing decimal point forward (to the left)
        /// </summary>
        /// <param name="_value"></param>
        /// <param name="_numOfPlaces"></param>
        /// <returns></returns>
        public static double Shrink(double _value, int _numOfPlaces)
        {
            return _value / Math.Pow(10, _numOfPlaces);
        }
    }
}