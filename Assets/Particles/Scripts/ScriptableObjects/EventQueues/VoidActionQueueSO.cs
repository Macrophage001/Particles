using System.Collections.Generic;
using UnityEngine;

public abstract class VoidActionQueueSO<T> : ScriptableObject where T : VoidAction
{
    public enum QueueStatus
    {
        Idle = 0,
        Pending = 1,
        Finished = 2
    }

    public QueueStatus m_Status;
    
    public List<T> m_ActionsList;
    private Queue<T> m_QueuedActions;

    public void Init()
    {
        m_QueuedActions = new Queue<T>(m_ActionsList);
        m_Status = m_QueuedActions.Count > 0 ? QueueStatus.Pending : QueueStatus.Finished;
    }
    
    public void Do()
    {
        // if (m_Status == QueueStatus.Finished)
        // {
        //     Debug.LogWarning("ActionQueue finished but no actions were taken. Make sure there are actions in the Action List before calling Do.");
        //     return;
        // }

        while (m_Status == QueueStatus.Pending)
        {
            if (m_QueuedActions.Count <= 0)
            {
                m_Status = QueueStatus.Finished;
                break;
            }
            var _action = m_QueuedActions.Dequeue();
            _action.Do();
        }

        m_ActionsList.Clear();
    }
}
