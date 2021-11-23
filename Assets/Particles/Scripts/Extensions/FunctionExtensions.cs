using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Particles.Scripts.Extensions;
using Particles.Scripts.Functions;
using static Particles.Scripts.Functions.Constants;

public static class EitherExtension
{
    public static Either<L, R> Flatten<L, R, RR>(this Either<L, RR> _either, Func<RR, Either<L, R>> _binder)
        => _either.Match(_l => Left<L, R>(_l), _r => _binder(_r));

    public static Either<L, RR> Map<L, R, RR>(this Either<L, R> _either, Func<L, R, RR> _mapper)
        => _either.Match(_l => new Either<L, RR>(_l), _r => new Either<L, RR>(_mapper(_either.Left.Value, _r)));

    public static Either<L, R> Where<L, R>(this Either<L, R> _either, Func<R, bool> _pred)
        => _either.Match(_l => new Either<L, R>(_l), _r => _pred(_r) ? _either : new Either<L, R>(_either.Left.Value));
}

public static class OptionalExtension
{
    public static Optional<int> Parse(this int _int, string _string) => Optional(int.Parse(_string));

    public static Optional<R> Flatten<T, R>(this Optional<T> _optional, Func<T, Optional<R>> _binder)
        => _optional.Match(() => Constants.None, _binder);

    public static IEnumerable<R> Flatten<T, R>(this IEnumerable<T> _enumerable, Func<T, Optional<R>> _func)
        => _enumerable.Flatten(_arg => _func(_arg).AsEnumerable());

    public static IEnumerable<R> Flatten<T, R>(this Optional<T> _optional, Func<T, IEnumerable<R>> _func)
        => _optional.AsEnumerable().Flatten(_func);

    public static Optional<R> Map<T, R>(this Optional<T> _optional, Func<T, R> _mapper)
        => _optional.Match(() => Constants.None, _arg => Constants.Optional(_mapper(_arg)));

    public static Optional<T> Where<T>(this Optional<T> _optional, Func<T, bool> _pred)
        => _optional.Match(() => Constants.None, _arg => _pred(_arg) ? _optional : Constants.None);

    public static IEnumerable<T> Return<T>(params T[] _params)
        => _params.ToList();

    public static IEnumerable<R> Flatten<T, R>(this IEnumerable<T> _enumerable, Func<T, IEnumerable<R>> _f)
    {
        foreach (var _t in _enumerable)
        foreach (var _r in _f(_t))
            yield return _r;
    }

    public static Optional<R> Apply<T, R>(this Optional<Func<T, R>> _optionalFunc, Optional<T> _optional)
        => _optionalFunc.Match(
            () => Constants.None,
            _f => _optional.Match(
                () => Constants.None,
                _t => Constants.Optional(_f(_t))));

    public static Optional<Func<T2, R>> Apply<T1, T2, R>(this Optional<Func<T1, T2, R>> _optionalFunc,
        Optional<T1> _optional)
        => Apply(_optionalFunc.Map(_func => _func.Curry()), _optional);

    public static Optional<Func<T2, Func<T3, R>>> Apply<T1, T2, T3, R>(this Optional<Func<T1, T2, T3, R>> _optionalFunc,
        Optional<T1> _optional)
        => Apply(_optionalFunc.Map(_func => _func.Curry()), _optional);

    public static Optional<T> Lookup<T>(this IEnumerable<T> _enumerable, Predicate<T> _predicate)
    {
        foreach (var _element in _enumerable)
        {
            if (_predicate(_element))
            {
                return _element;
            }
        }

        return Constants.None;
    }
}

public static class FuncExtension
{
    public static Func<T2, R> Apply<T1, T2, R>(this Func<T1, T2, R> _f, T1 _t1)
        => _arg => _f(_t1, _arg);

    public static Func<T2, T3, R> Apply<T1, T2, T3, R>(this Func<T1, T2, T3, R> _f, T1 _t1)
        => (_arg1, _arg2) => _f(_t1, _arg1, _arg2);

    // [(T1, T2) -> R] -> [T1 -> (T2 -> R)]
    public static Func<T1, Func<T2, R>> Curry<T1, T2, R>(this Func<T1, T2, R> _func)
        => _t1 => _t2 => _func(_t1, _t2);

    // [(T1, T2, T3) -> R] -> [(T1 -> (T2 -> (T3 -> R)))]
    public static Func<T1, Func<T2, Func<T3, R>>> Curry<T1, T2, T3, R>(this Func<T1, T2, T3, R> _func)
        => _t1 => _t2 => _t3 => _func(_t1, _t2, _t3);
    
    public static Func<R> Map<T, R>(this Func<T> _f, Func<T, R> _g)
        => () => _g(_f());
    
    public static Func<R> Map<T, R>(T _t, Func<T, R> _g)
        => () => _g(_t);
    public static Func<R> Bind<T, R>(this Func<T> _f, Func<T, Func<R>> _g)
        => () => _g(_f())();
}

public static class TupleExtension
{
    public static R Map<T0, T1, R>(this (T0, T1) _t, Func<(T0, T1), R> _g)
        => _g(_t);
    public static R Map<T0, T1, T2, R>(this (T0, T1, T2) _t, Func<(T0, T1, T2), R> _g)
        => _g(_t);
    

    public static Func<R> Bind<T0, T1, R>(this (T0, T1) _tuple, Func<(T0, T1), R> _g)
        => () => _g(_tuple);

    public static Func<R> Bind<T0, T1, T2, R>(this (T0, T1, T2) _tuple, Func<(T0, T1, T2), R> _g)
        => () => _g(_tuple);
}

public static class TryExtension
{
    public static Either<Exception, T> Run<T>([NotNull] this Utils.Try<T> _t)
    {
        try { return _t(); }
        catch (Exception _e) { return _e; }
    }
}