using Particles.Packages.Core.Runtime.ScriptableObjects.Actions;
using UnityEngine;

namespace Particles.Packages.BaseParticles.Runtime.Actions
{
    public abstract class ShortAction : ParticleAction<short> {}
    public abstract class ShortShortAction : ParticleAction<short, short> {}
    
    public abstract class IntAction : ParticleAction<int> {}
    public abstract class IntIntAction : ParticleAction<int, int> {}
    
    public abstract class LongAction : ParticleAction<long> {}
    public abstract class LongLongAction : ParticleAction<long, long> {}
    
    public abstract class FloatAction : ParticleAction<float> {}
    public abstract class FloatFloatAction : ParticleAction<float, float> {}
    
    public abstract class DoubleAction : ParticleAction<double> {}
    public abstract class DoubleDoubleAction : ParticleAction<double, double> {}
    
    public abstract class UShortAction : ParticleAction<ushort> {}
    public abstract class UShortUShortAction : ParticleAction<ushort, ushort> {}
    
    public abstract class ULongAction : ParticleAction<ulong> {}
    public abstract class ULongULongAction : ParticleAction<ulong, ulong> {}
    
    public abstract class BoolAction : ParticleAction<bool> {}
    public abstract class BoolBoolAction : ParticleAction<bool, bool> {}
    
    public abstract class StringAction : ParticleAction<string> {}
    
    public abstract class Vector2Action : ParticleAction<Vector2> {}
    
    public abstract class Vector3Action : ParticleAction<Vector3> {}
    
    public abstract class Vector4Action : ParticleAction<Vector4> {}
}