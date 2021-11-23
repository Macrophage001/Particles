using System.Collections.Generic;
using Particles.Scripts.ScriptableObjects.Actions;
using UnityEngine;
using UnityEngine.Events;

public abstract class GenericListener<
    TEventChannel,
    TAction,
    TUnityEvent,
    TType> : MonoBehaviour
    where TEventChannel : GenericEventChannelSO<TType>
    where TAction : GenericAction<TType>
    where TUnityEvent : UnityEvent<TType>
{
    [SerializeField] private TEventChannel m_Channel;

    public TUnityEvent OnEventRaised;

    public List<TAction> m_Actions;

    private void OnEnable()
    {
        if (m_Channel != null)
        {
            m_Channel.OnEventRaised += Response;

            for (int i = m_Actions.Count - 1; i >= 0; --i)
            {
                m_Channel.OnEventRaised += m_Actions[i].Do;
            }
        }
    }
    private void OnDisable()
    {
        if (m_Channel != null)
        {
            m_Channel.OnEventRaised -= Response;

            for (int i = m_Actions.Count - 1; i >= 0; --i)
            {
                m_Channel.OnEventRaised -= m_Actions[i].Do;
            }
        }
    }

    private void Response(TType _value)
    {
        OnEventRaised?.Invoke(_value);
    }
}

public abstract class GenericListener<
    TEventChannel,
    TAction,
    TUnityEvent,
    TType0, TType1> : MonoBehaviour
    where TEventChannel : GenericEventChannelSO<TType0, TType1>
    where TAction : GenericAction<TType0, TType1>
    where TUnityEvent : UnityEvent<TType0, TType1>
{
    [SerializeField] private TEventChannel m_Channel;

    public TUnityEvent OnEventRaised;

    public List<TAction> m_Actions;

    private void OnEnable()
    {
        if (m_Channel != null)
        {
            m_Channel.OnEventRaised += Response;

            for (int i = m_Actions.Count - 1; i >= 0; --i)
            {
                m_Channel.OnEventRaised += m_Actions[i].Do;
            }
        }
    }
    private void OnDisable()
    {
        if (m_Channel != null)
        {
            m_Channel.OnEventRaised -= Response;

            for (int i = m_Actions.Count - 1; i >= 0; --i)
            {
                m_Channel.OnEventRaised -= m_Actions[i].Do;
            }
        }
    }

    private void Response(TType0 _value0, TType1 _value1)
    {
        OnEventRaised?.Invoke(_value0, _value1);
    }
}
