using System;
using UnityEngine;

namespace Particles.Scripts.Extensions
{
    public interface IIndexable<out TElement>
    {
        public TElement GetAt(int _index);
        public int Count();
    }
    
    /// <summary>
    /// Literally just a wrapper for two of the Transform component's functions.
    /// </summary>
    public struct TransformIndexable : IIndexable<Transform>
    {
        private Transform _transform;

        public TransformIndexable(Transform _transform) => this._transform = _transform;
        
        public Transform GetAt(int _index)
        {
            return _transform.GetChild(_index);
        }

        public int Count() => _transform.childCount;
    }
    
    

    /// <summary>
    /// Allows the user to apply 1D and 2D array-based indexing to an object that didn't have it previously, like Transforms.
    /// </summary>
    /// <typeparam name="TIndexable"></typeparam>
    /// <typeparam name="TElement"></typeparam>
    public struct Indexer<TIndexable, TElement>
        where TIndexable: IIndexable<TElement>
    {
        private TIndexable _indexable;
        
        public Indexer(TIndexable _indexable) => this._indexable = _indexable;
        public TElement this[int _index] => _indexable.GetAt(_index);
        public TElement this[int _index0, int _index1] => _indexable.GetAt(_index1 * _indexable.Count() + _index0);

        public int Count => _indexable.Count();
    }
    
    
    
    
    public static class MonoBehaviourExtensions
    {
        #region GameObject Extensions
        public static bool TryGetComponentInParent<T>(this GameObject _gameObject, Action<T> _action)
            where T : Component
        {
            GameObject _parentObject = _gameObject.transform.parent.gameObject;
            if (_parentObject.TryGetComponent(out T _component))
            {
                _action(_component);
                return true;
            }
            return false;
        }

        public static GameObject Map(this GameObject _gameObject, Func<GameObject, GameObject> _func)
            => _func(_gameObject);

        public static void ForComponent<T>(this GameObject _gameObject, Action<T> _action)
            where T : Component
        {
            if (_gameObject.TryGetComponent(out T _component))
            {
                _action?.Invoke(_component);
            }
        }

        public static T ForComponent<TComponent, T>(this GameObject _gameObject, Func<TComponent, T> _func)
            where TComponent : Component
        {
            if (_gameObject.TryGetComponent(out TComponent _component))
                return _func(_component);
            return default;
        }
        
        public static void ForComponents<T0, T1>(this GameObject _gameObject, Action<T0, T1> _action)
            where T0 : Component
            where T1 : Component
        {
            if (_gameObject.TryGetComponent(out T0 _component0) &&
                _gameObject.TryGetComponent(out T1 _component1))
            {
                _action?.Invoke(_component0, _component1);
            }
        }
        public static void ForComponents<T0, T1, T2>(this GameObject _gameObject, Action<T0, T1, T2> _action)
            where T0 : Component
            where T1 : Component
            where T2 : Component
        {
            if (_gameObject.TryGetComponent(out T0 _component0) &&
                _gameObject.TryGetComponent(out T1 _component1) &&
                _gameObject.TryGetComponent(out T2 _component2))
            {
                _action?.Invoke(_component0, _component1, _component2);
            }
        }

        public static bool HasComponent<T>(this GameObject _gameObject)
        {
            return _gameObject.TryGetComponent(typeof(T), out Component _component);
        }
        
        #endregion

        #region Transform Extensions

        public static Transform[] GetChildren(this Transform _transform)
        {
            Transform[] _childTransforms = new Transform[_transform.childCount];
            for (int _i = _transform.childCount - 1; _i >= 0; --_i)
            {
                _childTransforms[_i] = _transform.GetChild(_i);
            }

            return _childTransforms;
        }

        public static void SetChildrenActive(this Transform _transform, bool _active)
        {
            if (_transform.childCount > 0)
            {
                foreach (Transform _t in _transform)
                {
                    _t.gameObject.SetActive(_active);
                }
            }
            else
            {
                Debug.LogWarning($"{_transform.name} is Empty");
            }
        }

        #endregion
    
    }
}
