using System;
using System.Collections.Generic;
using System.Linq;

namespace Particles.Packages.Core.Runtime.Extensions
{
    public static class ArrayExtension
    {
        public static List<(int, int)> Neighbors<T>(this T[][] _grid, int _x, int _y, bool _diagonals = false)
        {
            List<(int, int)> _points;
            if (_diagonals)
            {
                _points = new List<(int, int)>()
                {
                    (_x+1, _y),
                    (_x, _y+1),
                    (_x-1, _y),
                    (_x, _y-1),
                    (_x+1, _y-1),
                    (_x+1, _y+1),
                    (_x-1, _y-1),
                    (_x-1, _y+1),
                };
            }
            _points = new List<(int, int)>()
            {
                (_x-1, _y),
                (_x+1, _y),
                (_x, _y+1),
                (_x, _y-1),
            };

            return _points
                .Where((_point, _i) => _grid.WithinBounds(_point))
                .ToList();
        }
        public static List<(int, int)> Neighbors<T>(this T[,] _grid, int _x, int _y, bool _diagonals = false)
        {
            List<(int, int)> _points;
            if (_diagonals)
            {
                _points = new List<(int, int)>()
                {
                    (_x+1, _y),
                    (_x, _y+1),
                    (_x-1, _y),
                    (_x, _y-1),
                    (_x+1, _y-1),
                    (_x+1, _y+1),
                    (_x-1, _y-1),
                    (_x-1, _y+1),
                };
            }
            else
            {
                _points = new List<(int, int)>()
                {
                    (_x-1, _y),
                    (_x+1, _y),
                    (_x, _y+1),
                    (_x, _y-1),
                };
            }

            return _points
                .Where((_point, _i) => _grid.WithinBounds(_point))
                .ToList();
        }

        public static bool WithinBounds<T>(this T[][] _grid, (int, int) _tuple)
        {
            return _grid.WithinBounds(_tuple.Item2, _tuple.Item1);
        }
        public static bool WithinBounds<T>(this T[][] _grid, int _x, int _y)
        {
            var _width = _grid.Length;
            var _height = _grid[0].Length;
            return (_x >= 0 && _x < _width && _y >= 0 && _y < _height);
        }
        public static bool WithinBounds<T>(this T[,] _grid, (int, int) _tuple)
        {
            return _grid.WithinBounds(_tuple.Item2, _tuple.Item1);
        }
        public static bool WithinBounds<T>(this T[,] _grid, int _x, int _y)
        {
            var _width = _grid.GetLength(1);
            var _height = _grid.GetLength(0);
            return (_x >= 0 && _x < _width && _y >= 0 && _y < _height);
        }

        public static HashSet<(int, int)> FloodFill<T>(this T[][] _grid, Action<(int, int), T> _action, int _startX = 0, int _startY = 0)
        {
            HashSet<(int, int)> _visited = new HashSet<(int, int)>();
            Stack<(int, int)> _toVisit = new Stack<(int, int)>();
            _toVisit.Push((_startX, _startY));

            while (_toVisit.Any())
            {
                var (x, y) = _toVisit.Pop();
                var _neighbors = Neighbors(_grid, x, y);

                foreach (var _point in _neighbors)
                {
                    if (_visited.Contains(_point))
                        continue;

                    _action(_point, _grid[_point.Item2][_point.Item1]);

                    _toVisit.Push(_point);
                    _visited.Add(_point);
                }
            }
            return _visited;
        }

        public static HashSet<(int, int)> FloodFillWhen<T>
            (this T[][] _grid, Func<T[][],
            (int, int), List<(int, int)>> _pred,
            int _startX = 0, int _startY = 0)
        {
            HashSet<(int, int)> _visited = new HashSet<(int, int)>();
            Stack<(int, int)> _toVisit = new Stack<(int, int)>();
            _toVisit.Push((_startX, _startY));

            while (_toVisit.Any())
            {
                var _next = _toVisit.Pop();
                var _neighbors = _pred(_grid, _next);

                foreach (var _point in _neighbors)
                {
                    if (_visited.Contains(_point))
                        continue;

                    _toVisit.Push(_point);
                    _visited.Add(_point);
                }
            }
            return _visited;
        }

        public static T[,] To2D<T>(this T[][] _source)
        {
            try
            {
                int _firstDim = _source.Length;
                int _secondDim = _source.GroupBy(_row => _row.Length).Single().Key; // throws InvalidOperationException if source is not rectangular

                var _result = new T[_firstDim, _secondDim];
                for (int _i = 0; _i < _firstDim; ++_i)
                    for (int _j = 0; _j < _secondDim; ++_j)
                        _result[_i, _j] = _source[_i][_j];

                return _result;
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("The given jagged array is not rectangular.");
            }
        }

        public static T[][] ToJagged<T>(this T[,] _source)
        {
            try
            {
                int _firstDim = _source.Length;
                int _secondDim = _source.GetLength(1);

                var _result = new T[_firstDim][];
                for (int _y = 0; _y < _firstDim; _y++)
                {
                    for (int _x = 0; _x < _secondDim; _x++)
                    {
                        _result[_y][_x] = _source[_y, _x];
                    }
                }

                return _result;
            }
            catch (InvalidOperationException _e)
            {
                throw new InvalidOperationException($"The given 2D array is not rectangular. {_e}");
            }
        }

        public static bool Any<T>(this T[,] _array, Func<T, bool> _func)
        {
            for (int _y = 0; _y < _array.GetLength(0); _y++)
            {
                for (int _x = 0; _x < _array.GetLength(1); _x++)
                {
                    if (_func(_array[_y, _x]))
                        return true;
                }
            }
            return false;
        }
        public static bool Any<T>(this T[][] _array, Func<T, bool> _func)
        {
            for (int _y = 0; _y < _array.GetLength(0); _y++)
            {
                for (int _x = 0; _x < _array[_y].Length; _x++)
                {
                    if (_func(_array[_y][_x]))
                        return true;
                }
            }
            return false;
        }

        public static bool All<T>(this T[,] _array, Func<T, bool> _func) => _array.Count(_func) == _array.GetLength(0) * _array.GetLength(1);
        public static bool All<T>(this T[][] _array, Func<T, bool> _func) => _array.Count(_func) == _array.GetLength(0) * _array.GetLength(1);

        public static T First<T>(this T[,] _array, Func<T, bool> _func) => _array.Where((_t, _x, _y) => _func(_t)).First();
        public static T First<T>(this T[][] _array, Func<T, bool> _func) => _array.Where((_t, _x, _y) => _func(_t)).First();

        public static int Count<T>(this T[,] _array, Func<T, bool> _func)
        {
            int _count = 0;
            for (int _y = 0; _y < _array.GetLength(0); _y++)
            {
                for (int _x = 0; _x < _array.GetLength(1); _x++)
                {
                    if (_func(_array[_y, _x]))
                        _count++;
                }
            }
            return _count;
        }
        public static int Count<T>(this T[][] _array, Func<T, bool> _func)
        {
            int _count = 0;
            for (int _y = 0; _y < _array.GetLength(0); _y++)
            {
                for (int _x = 0; _x < _array[_y].Length; _x++)
                {
                    if (_func(_array[_y][_x]))
                        _count++;
                }
            }
            return _count;
        }
        public static IEnumerable<T> Where<T>(this T[,] _array, Func<T, int, int, bool> _func)
        {
            for (int _y = 0; _y < _array.GetLength(0); _y++)
            {
                for (int _x = 0; _x < _array.GetLength(1); _x++)
                {
                    if (_func(_array[_y, _x], _x, _y))
                        yield return _array[_y, _x];
                }
            }
        }
        public static IEnumerable<T> Where<T>(this T[][] _array, Func<T, int, int, bool> _func)
        {
            for (int _y = 0; _y < _array.GetLength(0); _y++)
            {
                for (int _x = 0; _x < _array[_y].Length; _x++)
                {
                    if (_func(_array[_y][_x], _x, _y))
                        yield return _array[_y][_x];
                }
            }
        }

        public static IEnumerable<R> Select<T, R>(this T[,] _array, Func<T, R> _func)
        {
            for (int _y = 0; _y < _array.GetLength(0); _y++)
            {
                for (int _x = 0; _x < _array.GetLength(1); _x++)
                {
                    yield return _func(_array[_y, _x]);
                }
            }
        }
        public static IEnumerable<R> Select<T, R>(this T[][] _array, Func<T, R> _func)
        {
            for (int _y = 0; _y < _array.GetLength(0); _y++)
            {
                for (int _x = 0; _x < _array[_y].Length; _x++)
                {
                    yield return _func(_array[_y][_x]);
                }
            }
        }
        // Stackoverflow coming in clutch.
        // https://stackoverflow.com/questions/16604985/find-elements-in-a-list-that-together-add-up-to-a-target-number?noredirect=1&lq=1
        public static IEnumerable<IEnumerable<T>> SubSetsOf<T>(this IEnumerable<T> _source)
        {
            // Deal with the case of an empty source (simply return an enumerable containing a single, empty enumerable)
            if (!_source.Any())
                return Enumerable.Repeat(Enumerable.Empty<T>(), 1);

            // Grab the first element off of the list
            var _element = _source.Take(1);

            // Recurse, to get all subsets of the source, ignoring the first item
            var _haveNots = SubSetsOf(_source.Skip(1));

            // Get all those subsets and add the element we removed to them
            var _haves = _haveNots.Select(set => _element.Concat(set));

            // Finally combine the subsets that didn't include the first item, with those that did.
            return _haves.Concat(_haveNots);
        }

        public static IEnumerable<T> DequeueChunk<T>(this Queue<T> _queue, int _count)
        {
            for (int _i = 0; _i < _count; _i++)
                yield return _queue.Dequeue();
        }
        public static IEnumerable<T> PopChunk<T>(this Stack<T> _queue, int _count)
        {
            for (int _i = 0; _i < _count; _i++)
                yield return _queue.Pop();
        }

        public static void Deconstruct<K, V>(this KeyValuePair<K, V> _pair, out K _key, out V _value)
        {
            _key = _pair.Key;
            _value = _pair.Value;
        }

        public static void ForEach<T>(this List<T> _list, Action<T, int> _action)
        {
            for (int _i = 0; _i < _list.Count; _i++) _action(_list[_i], _i);
        }
    }
}
