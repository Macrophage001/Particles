using System.Collections;
using System.Collections.Generic;
using Particles.Scripts.ScriptableObjects.Actions;
using UnityEngine;

public abstract class GenericActionQueue<TAction, TType> : ScriptableObject
    where TAction : GenericAction<TType>
{
    public enum QueueStatus
    {
        Idle = 0,
        Pending = 1,
        Finished = 2
    }

    public QueueStatus m_Status;
    
    public List<TAction> m_ActionsList = new List<TAction>();
    private Queue<TAction> m_QueuedActions = new Queue<TAction>();

    public void Init()
    {
        m_QueuedActions = new Queue<TAction>(m_ActionsList);
        m_Status = m_QueuedActions.Count > 0 ? QueueStatus.Pending : QueueStatus.Finished;
    }
    
    /// <summary>
    /// Adds an action straight to the queue.
    /// </summary>
    /// <param name="_action"></param>
    public void AddToQueue(TAction _action)
    {
        m_QueuedActions.Enqueue(_action);
    }
    
    private void UpdateQueueStatus()
    {
        m_Status = m_QueuedActions.Count > 0 ? QueueStatus.Pending : QueueStatus.Finished;
    }
    
    public void Do(TType _type)
    {
        UpdateQueueStatus();
        while (m_Status == QueueStatus.Pending)
        {
            if (m_QueuedActions.Count <= 0)
            {
                m_Status = QueueStatus.Finished;
                break;
            }
            var _action = (GenericAction<TType>) m_QueuedActions.Dequeue();
            _action.Do(_type);
        }

        m_ActionsList.Clear();
    }

    public int Count => m_QueuedActions.Count;
}

public abstract class GenericActionQueue<TAction> : ScriptableObject
    where TAction : VoidAction
{
    public enum QueueStatus
    {
        Idle = 0,
        Pending = 1,
        Finished = 2
    }

    public QueueStatus m_Status;
    
    public List<TAction> m_ActionsList = new List<TAction>();
    private Queue<TAction> m_QueuedActions = new Queue<TAction>();

    public void Init()
    {
        m_QueuedActions = new Queue<TAction>(m_ActionsList);
        UpdateQueueStatus();
    }

    /// <summary>
    /// Adds an action straight to the queue.
    /// </summary>
    /// <param name="_action"></param>
    public void AddToQueue(TAction _action)
    {
        Debug.Log($"Adding Action: {_action.name}, to ActionQueue: {name}");
        m_QueuedActions.Enqueue(_action);
    }

    private void UpdateQueueStatus()
    {
        m_Status = m_QueuedActions.Count > 0 ? QueueStatus.Pending : QueueStatus.Finished;
    }
    
    public void Do()
    {
        UpdateQueueStatus();
        while (m_Status == QueueStatus.Pending)
        {
            if (m_QueuedActions.Count <= 0)
            {
                m_Status = QueueStatus.Finished;
                break;
            }
            var _action = (VoidAction) m_QueuedActions.Dequeue();
            _action.Do();
        }

        m_ActionsList.Clear();
    }

    public int Count => m_QueuedActions.Count;
}
