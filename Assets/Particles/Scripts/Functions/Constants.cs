namespace Particles.Scripts.Functions
{
    public static class Constants
    {
        public static None None => None.Default;
        public static Optional<T> Optional<T>(T _value)
            => new Optional<T>(_value);
        
        public static Either<L, R> Left<L, R>(L _value)
            => new Either<L, R>(_value);
        public static Either<L, R> Right<L, R>(R _value)
            => new Either<L, R>(_value);
    }
}