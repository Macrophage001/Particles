using System;
using System.Collections.Generic;
using Particles.Packages.BaseParticles.Runtime.Events;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Particles.Packages.Core.Runtime
{
    [CreateAssetMenu(menuName = "Pool Master")]
    public class PoolMaster : ScriptableObject
    {
        [Serializable]
        public class Pool
        {
            public string m_Tag;
            public string m_ParentName;
            [HideInInspector] public Transform m_Parent;
            public GameObject m_Prefab;
            public int m_Size;
        }

        [SerializeField] private VoidEvent PoolsGeneratedEventChannel;
        [SerializeField] private StringEvent PoolGeneratedEventChannel;

        private Dictionary<string, Queue<GameObject>> m_PoolDictionary;
        public List<Pool> m_PoolList;

        private bool Exists(string tag, out Queue<GameObject> queue)
        {
            return m_PoolDictionary.TryGetValue(tag, out queue);
        }

        public void Create(Transform[] parents)
        {
            m_PoolDictionary = new Dictionary<string, Queue<GameObject>>(parents.Length);

            foreach (var pool in m_PoolList)
            {
                var queue = new Queue<GameObject>(pool.m_Size);

                for (var i = 0; i < pool.m_Size; i++)
                {
                    pool.m_Prefab.SetActive(false);
                    var obj = Instantiate(pool.m_Prefab, pool.m_Parent, true);
                    obj.SetActive(false);
                    obj.name = pool.m_Prefab.name;
                    foreach (var parent in parents)
                    {
                        if (!parent.name.Equals(pool.m_ParentName)) continue;
                        pool.m_Parent = parent;
                        break;
                    }
                    queue.Enqueue(obj);
                }

                m_PoolDictionary.Add(pool.m_Tag, queue);
                PoolGeneratedEventChannel.RaiseEvent(pool.m_ParentName);
            }
            PoolsGeneratedEventChannel.RaiseEvent();
        }

        public void TryGet(string tag, Action<GameObject> onGet)
        {
            if (Exists(tag, out var queue))
            {
                var gameObject = queue.Dequeue();

                onGet?.Invoke(gameObject);
                ShouldExpandPool(tag, gameObject);
                queue.Enqueue(gameObject);
            }
            else
            {
                Debug.LogError("Pool with tag '" + tag + "' does not exist");
            }
        }

        public void TryGetWithComponent<T>(string tag, Action<GameObject, T> onGet)
        {
            if (Exists(tag, out var queue))
            {
                var gameObject = queue.Dequeue();
                if (!gameObject.TryGetComponent<T>(out var component))
                    Debug.LogError($"Pool with tag '{tag}' does not have component '{typeof(T)}'");
                onGet?.Invoke(gameObject, component);
                ShouldExpandPool(tag, gameObject);
                queue.Enqueue(gameObject);
            }
            else
            {
                Debug.LogError("Pool with tag '" + tag + "' does not exist");
            }
        }

        public void TryGetWithComponent<T0, T1>(string tag, Action<GameObject, T0, T1> onGet)
        {
            if (Exists(tag, out var queue))
            {
                var gameObject = queue.Dequeue();
                if (!gameObject.TryGetComponent(out T0 c0))
                    Debug.LogError($"Pool with tag '{tag}' does not have component '{typeof(T0)}'");
                if (!gameObject.TryGetComponent(out T1 c1))
                    Debug.LogError($"Pool with tag '{tag}' does not have component '{typeof(T1)}'");
                onGet?.Invoke(gameObject, c0, c1);
                ShouldExpandPool(tag, gameObject);
                queue.Enqueue(gameObject);
            }
            else
            {
                Debug.LogError("Pool with tag '" + tag + "' does not exist");
            }
        }
    
        public void TryGetWithComponent<T0, T1, T2>(string tag, Action<GameObject, T0, T1, T2> onGet)
        {
            if (Exists(tag, out var queue))
            {
                var gameObject = queue.Dequeue();
                if (!gameObject.TryGetComponent(out T0 c0)) Debug.LogError($"Pool with tag '{tag}' does not have component '{typeof(T0)}'");
                if (!gameObject.TryGetComponent(out T1 c1)) Debug.LogError($"Pool with tag '{tag}' does not have component '{typeof(T1)}'");
                if (!gameObject.TryGetComponent(out T2 c2)) Debug.LogError($"Pool with tag '{tag}' does not have component '{typeof(T2)}'");
                onGet?.Invoke(gameObject, c0, c1, c2);
                ShouldExpandPool(tag, gameObject);
                queue.Enqueue(gameObject);
            }
            else
            {
                Debug.LogError("Pool with tag '" + tag + "' does not exist");
            }
        }

        public bool TryGet(string tag, out GameObject obj)
        {
            if (Exists(tag, out var queue))
            {
                obj = queue.Dequeue();
                ShouldExpandPool(tag, obj);
                queue.Enqueue(obj);
                return true;
            }
            Debug.LogError("Pool with tag '" + tag + "' does not exist");
            obj = null;
            return false;
        }
    
        public bool TryGetWithComponent<T>(string tag, out GameObject obj, out T component)
        {
            obj = default;
            component = default;
            if (Exists(tag, out var queue))
            {
                obj = queue.Dequeue();
                if (!obj.TryGetComponent<T>(out component)) Debug.LogError($"Pool with tag '{tag}' does not have component '{typeof(T)}'");
                ShouldExpandPool(tag, obj);
                queue.Enqueue(obj);
                return true;
            }
            Debug.LogError("Pool with tag '" + tag + "' does not exist");
            return false;
        }
    
        public bool TryGetWithComponent<T0, T1>(string tag, out GameObject obj, out T0 c0, out T1 c1)
        {
            obj = default;
            c0 = default;
            c1 = default;
            if (Exists(tag, out var queue))
            {
                obj = queue.Dequeue();
                if (!obj.TryGetComponent(out c0)) Debug.LogError($"Pool with tag '{tag}' does not have component '{typeof(T0)}'");
                if (!obj.TryGetComponent(out c1)) Debug.LogError($"Pool with tag '{tag}' does not have component '{typeof(T1)}'");
                ShouldExpandPool(tag, obj);
                queue.Enqueue(obj);
                return true;
            }
            Debug.LogError("Pool with tag '" + tag + "' does not exist");
            return false;
        }
    
        public bool TryGetWithComponent<T0, T1, T2>(string tag, out GameObject obj, out T0 c0, out T1 c1, out T2 c2)
        {
            obj = default;
            c0 = default;
            c1 = default;
            c2 = default;
            if (Exists(tag, out var queue))
            {
                obj = queue.Dequeue();
                if (!obj.TryGetComponent(out c0)) Debug.LogError($"Pool with tag '{tag}' does not have component '{typeof(T0)}'");
                if (!obj.TryGetComponent(out c1)) Debug.LogError($"Pool with tag '{tag}' does not have component '{typeof(T1)}'");
                if (!obj.TryGetComponent(out c2)) Debug.LogError($"Pool with tag '{tag}' does not have component '{typeof(T2)}'");
                ShouldExpandPool(tag, obj);
                queue.Enqueue(obj);
                return true;
            }
            Debug.LogError("Pool with tag '" + tag + "' does not exist");
            return false;
        }

        public void GetParent(string tag, Action<Transform> onGet)
        {
            if (!m_PoolDictionary.ContainsKey(tag))
            {
                Debug.LogError("Pool with tag '" + tag + "' does not exist");
            }

            var parent = m_PoolDictionary[tag].Peek().transform.parent;
            onGet?.Invoke(parent);
        }

        public void ShouldExpandPool(string tag, GameObject reference)
        {
            if (m_PoolDictionary[tag].Count != 0) return;

            var newGameObject = Instantiate(reference, reference.transform.parent);
            newGameObject.name = reference.name;
            m_PoolDictionary[tag].Enqueue(newGameObject);
        }
    }
}
