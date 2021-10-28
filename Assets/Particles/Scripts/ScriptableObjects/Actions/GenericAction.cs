using UnityEngine;


/// <summary>
/// Will perform an action on the passed in parameter when called.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class GenericAction<T> : ScriptableObject
{
    public abstract void Do(T _obj);
}

public abstract class GenericAction<T0, T1> : ScriptableObject
{
    public abstract void Do(T0 _obj0, T1 _obj1);
}

public abstract class GenericReturningAction<TIn> : ScriptableObject
{
    public abstract TIn Do();
}

public abstract class GenericReturningAction<TIn, T0> : ScriptableObject
{
    public abstract TIn Do(T0 _obj0);
}

public abstract class GenericReturningAction<TIn, T0, T1> : ScriptableObject
{
    public abstract TIn Do(T0 _obj0, T1 _obj1);
}
