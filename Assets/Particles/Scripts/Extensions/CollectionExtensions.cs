using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace Particles.Extensions
{
    /// <summary>
    /// Source: https://www.jacksondunstan.com/articles/5516
    /// </summary>
    public static class CollectionExtensions
    {
        #region Interfaces
        public interface IIndexedCollection<T>
        {
            int Length { get; }
            T this[int index] { get; set; }
        }
        
        public interface IForwardIterator<T, TForwardIterator> :
            IEquatable<TForwardIterator>
            where TForwardIterator : IForwardIterator<T, TForwardIterator>
        {
            T Value { get; set; }
            void Next();
        }
        
        public interface IMapper<TFrom, TTo>
            where TFrom : struct
            where TTo : struct
        {
            TTo Map(in TFrom _item);
        }
        
        public interface IFilterPredicate<T>
            where T : struct
        {
            bool Check(in T _item);
        }
        
        public interface ISelector<T>
        {
            T Select(T _val);
        }
        
        public interface IPredicate<T>
        {
            bool Check(T _val);
        }
        
        public interface ICollection<T, TEnumerator, TCollection>
            where TEnumerator : IEnumerator<T>
        {
            TEnumerator GetEnumerator();
            TCollection Allocate();
            void Add(T _element);
        }
        
        #endregion

        public static TCollection Where<T, TEnumerator, TPredicate, TCollection>(
            this TCollection _collection, TPredicate _predicate)
            where TPredicate : IPredicate<T>
            where TEnumerator : IEnumerator<T>
            where TCollection : ICollection<T, TEnumerator, TCollection>
        {
            TCollection _results = _collection.Allocate();
            TEnumerator _enumerator = _collection.GetEnumerator();
            while (_enumerator.MoveNext())
            {
                T _value = _enumerator.Current;
                if (_predicate.Check(_value))
                {
                    _results.Add(_value);
                }
            }
            _enumerator.Dispose();
            return _results;
        }

        public static TCollection Select<T, TEnumerator, TSelector, TCollection>(
            this TCollection _collection, TSelector _selector)
            where TEnumerator : IEnumerator<T>
            where TSelector : ISelector<T>
            where TCollection : ICollection<T, TEnumerator, TCollection>
        {
            TCollection _results = _collection.Allocate();
            TEnumerator _enumerator = _collection.GetEnumerator();
            while (_enumerator.MoveNext())
            {
                T _value = _enumerator.Current;
                T _selected = _selector.Select(_value);
                _results.Add(_selected);
            }
            _enumerator.Dispose();
            return _results;
        }

        public static int Length<T, TCollection, TEnumerator>(
            this TCollection _collection)
            where T : struct
            where TEnumerator : IEnumerator<T>
            where TCollection : ICollection<T, TEnumerator, TCollection>
        {
            int _length = 0;
            IEnumerator _enumerator = _collection.GetEnumerator();
            while (_enumerator.MoveNext())
            {
                _length++;
            }

            return _length;
        }
        
        public static int Count<T, TCollection, TEnumerator>(
            this TCollection _collection)
            where T : class
            where TEnumerator : IEnumerator<T>
            where TCollection : ICollection<T, TEnumerator, TCollection>
        {
            int _length = 0;
            IEnumerator _enumerator = _collection.GetEnumerator();
            while (_enumerator.MoveNext())
            {
                _length++;
            }

            return _length;
        }
        
        public static int Length<T, TEnumerator>(
            this TEnumerator _enumerator)
            where T : struct
            where TEnumerator : IEnumerator<T>
        {
            int _length = 0;
            while (_enumerator.MoveNext())
            {
                _length++;
            }

            return _length;
        }

        public static int IndexOf<T>(this NativeArray<T> _array, in T _item)
            where T : struct, IEquatable<T>
        {
            for (int i = _array.Length - 1; i >= 0; --i)
            {
                if (_array[i].Equals(_item))
                    return i;
            }

            return -1;
        }

        public static NativeArray<T> Filter<T, TFilterPredicate>(
            this NativeArray<T> _input,
            NativeArray<T> _output,
            in TFilterPredicate _predicate,
            out int _numFiltered)
            where T : struct
            where TFilterPredicate : IFilterPredicate<T>
        {
            int _destIndex = 0;
            for (int i = _input.Length - 1; i >= 0; --i)
            {
                T _cur = _input[i];
                if (_predicate.Check(_cur))
                {
                    _output[_destIndex] = _cur;
                    _destIndex++;
                }
            }
            _numFiltered = _destIndex;
            return _output;
        }

        public static NativeArray<TTo> Map<TFrom, TTo, TMapper>(
            this NativeArray<TFrom> _input,
            NativeArray<TTo> _output,
            in TMapper _mapper)
            where TFrom : struct
            where TTo : struct
            where TMapper : IMapper<TFrom, TTo>
        {
            for (int i = _input.Length - 1; i >= 0; --i)
            {
                _output[i] = _mapper.Map(_input[i]);
            }

            return _output;
        }
        
        public static TIndexedCollectionTo Map<TIndexedCollectionFrom, TIndexedCollectionTo, TFrom, TTo, TMapper>(
            this TIndexedCollectionFrom _input,
            TIndexedCollectionTo _output,
            in TMapper _mapper)
            where TIndexedCollectionFrom : IIndexedCollection<TFrom>
            where TIndexedCollectionTo : IIndexedCollection<TTo>
            where TFrom : struct
            where TTo : struct
            where TMapper : IMapper<TFrom, TTo>
        {
            for (int i = _input.Length - 1; i >= 0; --i)
            {
                _output[i] = _mapper.Map(_input[i]);
            }

            return _output;
        }

        public static TForwardIteratorTo Map<TForwardIteratorFrom, TForwardIteratorTo, TFrom, TTo, TMapper>(
            this TForwardIteratorFrom _inputStart,
            TForwardIteratorFrom _inputEnd,
            TForwardIteratorTo _output,
            in TMapper _mapper)
            where TForwardIteratorFrom : IForwardIterator<TFrom, TForwardIteratorFrom>
            where TForwardIteratorTo : IForwardIterator<TTo, TForwardIteratorTo>
            where TFrom : struct
            where TTo : struct
            where TMapper : IMapper<TFrom, TTo>
        {
            for (; !_inputStart.Equals(_inputEnd); _inputStart.Next(), _output.Next())
            {
                _output.Value = _mapper.Map(_inputStart.Value);
            }

            return _output;
        }

        #region Wrappers
        
        public struct NativeArrayIndexedCollection<T> : IIndexedCollection<T>
            where T : struct
        {
            public NativeArray<T> Array;
            public int Length => Array.Length;

            public T this[int _index]
            {
                get => Array[_index];
                set => Array[_index] = value;
            }

            public NativeArrayIndexedCollection(NativeArray<T> _array)
            {
                Array = _array;
            }

            public static implicit operator NativeArrayIndexedCollection<T>(
                NativeArray<T> _array)
            {
                return new NativeArrayIndexedCollection<T>(_array);
            }

            public static implicit operator NativeArray<T>(
                NativeArrayIndexedCollection<T> _collection)
            {
                return _collection.Array;
            }
        }

        public struct NativeArrayIterator<T> : IForwardIterator<T, NativeArrayIterator<T>>
            where T : struct
        {
            public NativeArray<T> Array;
            public int Index;

            public NativeArrayIterator(NativeArray<T> _array, int _index)
            {
                Array = _array;
                Index = _index;
            }

            public bool Equals(NativeArrayIterator<T> other) =>
                Array.Equals(other.Array) && Index == other.Index;

            public T Value
            {
                get => Array[Index]; 
                set => Array[Index] = value;
            }

            public void Next()
            {
                Index++;
            }
        }

        public struct DynamicNativeArrayEnumerator<T> : IEnumerator<T>
            where T : struct
        {
            private DynamicNativeArray<T> _array;
            private int _index;

            public DynamicNativeArrayEnumerator(
                DynamicNativeArray<T> _array,
                int _index)
            {
                this._array = _array;
                this._index = _index;
            }

            public void Reset()
            {
                _index = -1;
            }

            public T Current
            {
                get => _array[_index];
            }

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                _index++;
                return _index < _array.Length;
            }

            public void Dispose()
            {
            }
        }
        
        public struct DynamicNativeArray<T> : ICollection<T, DynamicNativeArrayEnumerator<T>, DynamicNativeArray<T>>
            where T : struct
        {
            private NativeArray<T> _array;
            private int _length;
            private Allocator _allocator;

            public int Length => _length;
            
            public DynamicNativeArray(int _capacity, Allocator _allocator)
            {
                _array = new NativeArray<T>(_capacity, _allocator);
                _length = 0;
                this._allocator = _allocator;
            }

            public T this[int _index]
            {
                get
                {
                    if (_index >= _length)
                    {
                        throw new IndexOutOfRangeException($"{_index} >= {_length}");
                    }

                    return _array[_index];
                }
                set
                {
                    if (_index >= _length)
                    {
                        throw new IndexOutOfRangeException($"{_index} >= {_length}");
                    }

                    _array[_index] = value;
                }
            }

            public DynamicNativeArrayEnumerator<T> GetEnumerator()
            {
                return new DynamicNativeArrayEnumerator<T>(this, -1);
            }

            public DynamicNativeArray<T> Allocate()
            {
                return new DynamicNativeArray<T>(4, _allocator);
            }

            public void Add(T _element)
            {
                if (_length == _array.Length)
                {
                    NativeArray<T> _newArray = new NativeArray<T>(
                        _array.Length * 2,
                        _allocator);
                    NativeArray<T>.Copy(_array, _newArray, _array.Length);
                    _array = _newArray;
                }

                _array[_length] = _element;
                _length++;
            }
        }
        
        #endregion
    }

    public static class NativeArrayForwardIteratorExtensions
    {
        public static CollectionExtensions.NativeArrayIterator<T> Begin<T>(
            this NativeArray<T> _array)
            where T : struct
        {
            return new CollectionExtensions.NativeArrayIterator<T>(_array, 0);
        }

        public static CollectionExtensions.NativeArrayIterator<T> End<T>(
            this NativeArray<T> _array)
            where T : struct
        {
            return new CollectionExtensions.NativeArrayIterator<T>(_array, _array.Length);
        }
    }
}

