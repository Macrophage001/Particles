using UnityEngine;

namespace Particles.Packages.Core.Runtime
{
    public abstract class DataLoadable<T> : ScriptableObject
    {
        public abstract bool LoadData(T _data);
    }
}