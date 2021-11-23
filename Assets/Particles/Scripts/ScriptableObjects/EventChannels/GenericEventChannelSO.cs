using UnityEngine;
using UnityEngine.Events;

abstract public class GenericEventChannelSO<T> : ScriptableObject
{
    [Tooltip("Used by Editor to test values.")]
    public T m_TestValue;
    
    public UnityAction<T> OnEventRaised;

    public void RaiseEvent(T _obj)
    {
        OnEventRaised?.Invoke(_obj);
    }
}

abstract public class GenericEventChannelSO<T0, T1> : ScriptableObject
{
    [Tooltip("Used by Editor to test values.")]
    public T0 m_TestValue0;
    public T0 m_TestValue1;

    public UnityAction<T0, T1> OnEventRaised;

    public void RaiseEvent(T0 _obj0, T1 _obj1)
    {
        //Debug.Log($"{name} Event Raised with parameters: {_obj0.ToString()}, {_obj1.ToString()}");
        OnEventRaised?.Invoke(_obj0, _obj1);
    }
}
