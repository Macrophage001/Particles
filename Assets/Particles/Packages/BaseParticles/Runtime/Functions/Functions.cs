using Particles.Packages.Core.Runtime.ScriptableObjects.Functions;
using UnityEngine;

namespace Particles.Packages.BaseParticles.Runtime.Functions
{
    public abstract class ShortFunction : ParticleFunction<short> {}
    public abstract class ShortShortFunction : ParticleFunction<short, short> {}
    
    public abstract class IntFunction : ParticleFunction<int> {}
    public abstract class IntIntFunction : ParticleFunction<int, int> {}
    
    public abstract class LongFunction : ParticleFunction<long> {}
    public abstract class LongLongFunction : ParticleFunction<long, long> {}
    
    public abstract class FloatFunction : ParticleFunction<float> {}
    public abstract class FloatFloatFunction : ParticleFunction<float, float> {}
    
    public abstract class DoubleFunction : ParticleFunction<double> {}
    public abstract class DoubleDoubleFunction : ParticleFunction<double, double> {}
    
    public abstract class UShortFunction : ParticleFunction<ushort> {}
    public abstract class UShortUShortFunction : ParticleFunction<ushort, ushort> {}
    
    public abstract class ULongFunction : ParticleFunction<ulong> {}
    public abstract class ULongULongFunction : ParticleFunction<ulong, ulong> {}
    
    public abstract class BoolFunction : ParticleFunction<bool> {}
    public abstract class BoolBoolFunction : ParticleFunction<bool, bool> {}
    
    public abstract class StringFunction : ParticleFunction<string> {}
    public abstract class StringStringFunction : ParticleFunction<string, string> {}
    
    public abstract class Vector2Function : ParticleFunction<Vector2> {}
    
    public abstract class Vector3Function : ParticleFunction<Vector3> {}
    
    public abstract class Vector4Function : ParticleFunction<Vector4> {}
}