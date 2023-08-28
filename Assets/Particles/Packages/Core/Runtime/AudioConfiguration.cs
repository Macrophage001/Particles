using System;
using UnityEngine;

namespace Particles.Packages.Core.Runtime
{
    [Serializable]
    public struct AudioConfiguration
    {
        public string _soundName;
        public bool _loop;
        public float _volume;
        public float _pitch;

        public Vector3 _sourcePosition;
    }
}