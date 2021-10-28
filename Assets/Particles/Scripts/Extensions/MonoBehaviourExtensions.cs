using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Particles.Extensions
{
    public interface Indexable<TElement>
    {
        public TElement GetAt(int _index);
        public int Count();
    }

    public struct TransformIndexable : Indexable<Transform>
    {
        private Transform _transform;

        public TransformIndexable(Transform _transform) => this._transform = _transform;
        
        public Transform GetAt(int _index)
        {
            return _transform.GetChild(_index);
        }

        public int Count() => _transform.childCount;
    }
    
    public struct Indexer<TIndexable, TElement>
        where TIndexable: Indexable<TElement>
    {
        private TIndexable _indexable;

        public Indexer(TIndexable _indexable) => this._indexable = _indexable;
        public TElement this[int _index] => _indexable.GetAt(_index);
        public TElement this[int _index0, int _index1] => _indexable.GetAt(_index1 * _indexable.Count() + _index0);

        public int Count => _indexable.Count();
    }

    public struct DynamicTransformArrayEnumerator : IEnumerator<Transform>
    {
        private Transform _transform;
        private int _index;

        public DynamicTransformArrayEnumerator(
            Transform _transform,
            int _index)
        {
            this._transform = _transform;
            this._index = _index;
        }
        public bool MoveNext()
        {
            _index++;
            return _index < _transform.childCount;
        }

        public void Reset()
        {
            _index = -1;
        }

        public Transform Current
        {
            get => _transform.GetChild(_index);
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            // Not used.
        }
    }

    public struct DynamicTransform : CollectionExtensions.ICollection<Transform, DynamicTransformArrayEnumerator, DynamicTransform>
    {
        private Transform _transform;
        private int _length;

        public DynamicTransform(Transform _transform)
        {
            this._transform = _transform;
            _length = _transform.childCount;
        }

        public Transform this[int _index]
        {
            get
            {
                if (_index >= _length)
                {
                    throw new IndexOutOfRangeException($"{_index} >= {_length}");
                }

                return _transform.GetChild(_index);
            }

            set
            {
                if (_index >= _length)
                {
                    throw new IndexOutOfRangeException($"{_index} >= {_length}");
                }

                value.parent = _transform;
            }
        }
        
        public DynamicTransformArrayEnumerator GetEnumerator()
        {
            return new DynamicTransformArrayEnumerator(_transform, -1);
        }

        public DynamicTransform Allocate()
        {
            // Not used.
            return this;
        }

        public void Add(Transform _element)
        {
            _element.parent = _transform;
        }
    }

    public static class MonoBehaviourExtensions
    {
        #region GameObject Extensions
        public static bool TryGetComponentInParent(this GameObject _gameObject, Type _type, out Component _component)
        {
            GameObject _parentObject = _gameObject.transform.parent.gameObject;
            return _parentObject.TryGetComponent(_type, out _component);
        }
        #endregion

        #region Transform Extensions

        public static Transform[] GetChildren(this Transform _transform)
        {
            Transform[] _childTransforms = new Transform[_transform.childCount];
            for (int i = _transform.childCount - 1; i >= 0; --i)
            {
                _childTransforms[i] = _transform.GetChild(i);
            }

            return _childTransforms;
        }

        public static void SetChildrenActive(this Transform _transform, bool _active)
        {
            foreach (Transform _t in _transform)
            {
                _t.gameObject.SetActive(_active);
            }
        }

        #endregion
    
    }
}
