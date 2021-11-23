using System;
using Particles.Scripts.ScriptableObjects.Actions;

namespace Particles.Scripts.Extensions
{
    public static class ReturningActionExtensions
    {
        public static GenericReturningAction<R> Map<T, R>(this GenericReturningAction<T> _returningAction,
            Func<T, GenericReturningAction<R>> _mapper)
            => _mapper(_returningAction.Do());
    }
}
