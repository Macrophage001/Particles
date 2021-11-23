using System;
using System.Collections.Generic;
using Particles.Extensions;
using ScriptableObjects;
using UnityEngine;

namespace Particles.Scripts.Extensions
{
    public static class PoolMasterExtensions
    {
        /// <summary>
        /// Dequeues a GameObject and provides the requested Components to the Action that's passed in for use.
        /// </summary>
        /// <param name="_poolMaster"></param>
        /// <param name="_tag"></param>
        /// <param name="onGet"></param>
        /// <typeparam name="T0"></typeparam>
        /// <returns></returns>
        public static bool TryGetWithComponent<T0>(this PoolMaster _poolMaster, string _tag, Action<GameObject, T0> onGet)
            where T0 : Component
        {
            if (_poolMaster.Exists(_tag, out Queue<GameObject> _queue))
            {
                GameObject _gameObject = _queue.Dequeue();

                if (_gameObject.TryGetComponent(typeof(T0), out Component _component0))
                {
                    onGet?.Invoke(_gameObject, (T0) _component0);
                }
                else
                {
                    Debug.LogWarning(
                        $"Requested Component(s) could not be found. Please make sure that the GameObjects have the requested Component(s).");
                }
                _poolMaster.ShouldExpandPool(_tag, _gameObject);
                _queue.Enqueue(_gameObject);
                return true;
            }
            else
            {
                Debug.LogWarning("Pool with tag '" + _tag + "' does not exist");
                return false;
            }
        }
        
        public static bool TryGetWithComponents<T0, T1>(this PoolMaster _poolMaster, string _tag, Action<GameObject, T0, T1> onGet)
            where T0 : Component
            where T1 : Component
        {
            if (_poolMaster.Exists(_tag, out Queue<GameObject> _queue))
            {
                GameObject _gameObject = _queue.Dequeue();

                if (_gameObject.TryGetComponent(typeof(T0), out Component _component0) &&
                    _gameObject.TryGetComponent(typeof(T1), out Component _component1))
                {
                    onGet?.Invoke(_gameObject, (T0) _component0, (T1) _component1);
                }
                else
                {
                    Debug.LogWarning(
                        $"Requested Component(s) could not be found. Please make sure that the GameObjects have the requested Component(s).");
                }
                _poolMaster.ShouldExpandPool(_tag, _gameObject);
                _queue.Enqueue(_gameObject);
                return true;
            }
            else
            {
                Debug.LogWarning("Pool with tag '" + _tag + "' does not exist");
                return false;
            }
        }
        
        public static bool TryGetWithComponents<T0, T1, T2>(this PoolMaster _poolMaster, string _tag, Action<GameObject, T0, T1, T2> onGet)
            where T0 : Component
            where T1 : Component
            where T2 : Component
        {
            if (_poolMaster.Exists(_tag, out Queue<GameObject> _queue))
            {
                GameObject _gameObject = _queue.Dequeue();

                if (_gameObject.TryGetComponent(typeof(T0), out Component _component0) &&
                    _gameObject.TryGetComponent(typeof(T1), out Component _component1) &&
                    _gameObject.TryGetComponent(typeof(T2), out Component _component2))
                {
                    onGet?.Invoke(_gameObject, (T0) _component0, (T1) _component1, (T2) _component2);
                }
                else
                {
                    Debug.LogWarning(
                        $"Requested Component(s) could not be found. Please make sure that the GameObjects have the requested Component(s).");
                }
                _poolMaster.ShouldExpandPool(_tag, _gameObject);
                _queue.Enqueue(_gameObject);
                return true;
            }
            else
            {
                Debug.LogWarning("Pool with tag '" + _tag + "' does not exist");
                return false;
            }
        }
        
        public static bool TryGetWithComponents<T0, T1, T2, T3>(this PoolMaster _poolMaster, string _tag, Action<GameObject, T0, T1, T2, T3> onGet)
            where T0 : Component
            where T1 : Component
            where T2 : Component
            where T3 : Component
        {
            if (_poolMaster.Exists(_tag, out Queue<GameObject> _queue))
            {
                GameObject _gameObject = _queue.Dequeue();

                if (_gameObject.TryGetComponent(typeof(T0), out Component _component0) &&
                    _gameObject.TryGetComponent(typeof(T1), out Component _component1) &&
                    _gameObject.TryGetComponent(typeof(T2), out Component _component2) &&
                    _gameObject.TryGetComponent(typeof(T3), out Component _component3))
                {
                    onGet?.Invoke(_gameObject, (T0) _component0, (T1) _component1, (T2) _component2, (T3) _component3);
                }
                else
                {
                    Debug.LogWarning(
                        $"Requested Component(s) could not be found. Please make sure that the GameObjects have the requested Component(s).");
                }
                _poolMaster.ShouldExpandPool(_tag, _gameObject);
                _queue.Enqueue(_gameObject);
                return true;
            }
            else
            {
                Debug.LogWarning("Pool with tag '" + _tag + "' does not exist");
                return false;
            }
        }
        
        public static bool TryGetWithComponents<T0, T1, T2, T3, T4>(this PoolMaster _poolMaster, string _tag, Action<GameObject, T0, T1, T2, T3, T4> onGet)
            where T0 : Component
            where T1 : Component
            where T2 : Component
            where T3 : Component
            where T4 : Component
        {
            if (_poolMaster.Exists(_tag, out Queue<GameObject> _queue))
            {
                GameObject _gameObject = _queue.Dequeue();

                if (_gameObject.TryGetComponent(typeof(T0), out Component _component0) &&
                    _gameObject.TryGetComponent(typeof(T1), out Component _component1) &&
                    _gameObject.TryGetComponent(typeof(T2), out Component _component2) &&
                    _gameObject.TryGetComponent(typeof(T2), out Component _component3) &&
                    _gameObject.TryGetComponent(typeof(T2), out Component _component4))
                {
                    onGet?.Invoke(_gameObject, (T0) _component0, (T1) _component1, (T2) _component2, (T3) _component3, (T4) _component4);
                }
                else
                {
                    Debug.LogWarning(
                        $"Requested Component(s) could not be found. Please make sure that the GameObjects have the requested Component(s).");
                }
                _poolMaster.ShouldExpandPool(_tag, _gameObject);
                _queue.Enqueue(_gameObject);
                return true;
            }
            else
            {
                Debug.LogWarning("Pool with tag '" + _tag + "' does not exist");
                return false;
            }
        }

        /// <summary>
        /// Retrieves all components of type <typeparam name="T"></typeparam>> within a given pool.
        /// </summary>
        /// <param name="_poolMaster"></param>
        /// <param name="_tag"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> TryGetComponents<T>(this PoolMaster _poolMaster, string _tag) 
            where T : Component
        {
            if (_poolMaster.Exists(_tag, out Queue<GameObject> _queue))
            {
                var _count = _queue.Count;
                for (int _i = 0; _i < _count; _i++)
                {
                    T _component = default;
                    _poolMaster.TryGetWithComponent<T>(_tag, (_o, _c) =>
                    {
                        _component = _c;
                    });
                    yield return _component;
                }
            }
            else
            {
                Debug.LogWarning("Pool with tag '" + _tag + "' does not exist");
            }
        }
    }
}
