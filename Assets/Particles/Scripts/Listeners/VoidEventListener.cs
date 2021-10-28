using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class VoidEventListener : MonoBehaviour
{
    [FormerlySerializedAs("_channel")] [SerializeField] private VoidEventChannelSO m_Channel;
    public UnityEvent OnEventRaised;
    public List<VoidAction> m_Actions;
    
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

    private void Response()
    {
        OnEventRaised?.Invoke();
    }
}