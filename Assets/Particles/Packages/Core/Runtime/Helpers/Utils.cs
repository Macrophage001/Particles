using System;

namespace Particles.Packages.Core.Runtime.Helpers
{
    public static class Utils
    {
        public static TData Using<TDisposable, TData>(TDisposable disposable, Func<TDisposable, TData> func)
            where TDisposable : IDisposable
        {
            using (disposable) return func(disposable);
        }

        public static void Using<TDisposable>(TDisposable disposable, Action<TDisposable> action)
            where TDisposable : IDisposable
        {
            using (disposable) action(disposable);
        }

    }
}
