using System;
using UnityEngine;

namespace Particles.Packages.Core.Runtime.Extensions
{
    public static class MonoBehaviourExtensions
    {
        #region GameObject Extensions
        public static bool TryGetComponentInParent<T>(this GameObject _gameObject, Action<T> _action)
            where T : Component
        {
            GameObject parentObject = _gameObject.transform.parent.gameObject;
            if (!parentObject.TryGetComponent(out T component)) return false;
            _action(component);
            return true;
        }
        
        public static bool TryGetComponents<T0, T1>(this GameObject _gameObject, out T0 _component0, out T1 _component1)
            where T0 : Component
            where T1 : Component
        {
            _component0 = default;
            _component1 = default;
            return _gameObject.TryGetComponent(out _component0) && 
                   _gameObject.TryGetComponent(out _component1);
        }
        
        public static bool TryGetComponents<T0, T1, T2>(this GameObject _gameObject, out T0 _component0, out T1 _component1, out T2 _component2)
            where T0 : Component
            where T1 : Component
        {
            _component0 = default;
            _component1 = default;
            _component2 = default;
            return _gameObject.TryGetComponent(out _component0) &&
                   _gameObject.TryGetComponent(out _component1) &&
                   _gameObject.TryGetComponent(out _component2);
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
        public static Transform GetHighestAncestor(this Transform _transform) => _transform.parent == null ? _transform : _transform.parent.GetHighestAncestor();

        public static Transform GetAncestor(this Transform _transform, int _depth)
        {
            if (_depth == 0) return _transform;
            return _transform.parent == null ? null : _transform.parent.GetAncestor(_depth - 1);
        }

        #endregion
    }
}
