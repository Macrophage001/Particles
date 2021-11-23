using System;
using System.Runtime.InteropServices;

public struct Left<L>
{
    internal static readonly Left<L> Default = new Left<L>();
        
    public L Value { get; }

    public Left(L _value)
        => Value = _value;
}
public struct Right<R>
{
    internal static readonly Right<R> Default = new Right<R>();
        
    public R Value { get; }

    public Right(R _value)
        => Value = _value;
}

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public struct Either<L, R>
{
    private bool m_HasSucceeded;
        
    public Left<L> Left { get; }
    public Right<R> Right { get; }

    internal Either(R _right)
    {
        Left = new Left<L>();
        Right = new Right<R>(_right);
        m_HasSucceeded = true;
    }
    internal Either(L _left)
    {
        Right = new Right<R>();
        Left = new Left<L>(_left);
        m_HasSucceeded = false;
    }
        
    public static implicit operator Either<L, R>(Left<L> _left)
        => new Either<L, R>(_left.Value);
    public static implicit operator Either<L, R>(Right<R> _right)
        => new Either<L, R>(_right.Value);
    public static implicit operator Either<L, R>(L _left)
        => new Either<L, R>(_left);
    public static implicit operator Either<L, R>(R _right)
        => new Either<L, R>(_right);

    public TR Match<TR>(Func<L, TR> _left, Func<R, TR> _right)
        => m_HasSucceeded ? _right(Right.Value) : _left(Left.Value);
}