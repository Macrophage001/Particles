using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnableEventListener : MonoBehaviour
{
    public UnityEvent OnEventRaised;
    public List<VoidAction> m_Actions;
    
    private void OnEnable()
    {
        OnEventRaised?.Invoke();
        for (int i = m_Actions.Count - 1; i >= 0; --i)
        {
            m_Actions[i].Do();
        }
    }
}
