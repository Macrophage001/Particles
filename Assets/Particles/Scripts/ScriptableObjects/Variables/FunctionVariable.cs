using UnityEngine;

public abstract class FunctionVariable<T> : ScriptableObject
{
    public virtual T Func()
    {
        return default(T);
    }
}
