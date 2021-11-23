using System;
using UnityEngine;

namespace Particles.Scripts.ScriptableObjects.Actions
{
    /// <summary>
    /// Will perform an action on the passed in parameter when called.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class GenericAction<T> : ScriptableObject
    {
        public abstract void Do(T _obj);
    }

    /// <summary>
    /// Will perform an action on the passed in parameters when called.
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    public abstract class GenericAction<T0, T1> : ScriptableObject
    {
        public abstract void Do(T0 _obj0, T1 _obj1);
    }

    /// <summary>
    /// Will perform an action on the passed in parameter when called and return a value.
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    public abstract class GenericReturningAction<TIn> : ScriptableObject
    {
        public abstract TIn Do();
    }

    /// <summary>
    /// Will perform an action on the passed in parameter when called and return a value.
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="T0"></typeparam>
    public abstract class GenericReturningAction<TIn, T0> : ScriptableObject
    {
        public abstract TIn Do(T0 _obj0);
    }

    /// <summary>
    /// Will perform an action on the passed in parameter when called and return a value.
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    public abstract class GenericReturningAction<TIn, T0, T1> : ScriptableObject
    {
        public abstract TIn Do(T0 _obj0, T1 _obj1);
    }
}