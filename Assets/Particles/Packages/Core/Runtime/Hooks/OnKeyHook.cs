﻿using Particles.Packages.Core.Runtime.Input;
using UnityEngine;

namespace Particles.Packages.Core.Runtime.Hooks
{
    public class OnKeyHook : VoidHook
    {
        [SerializeField] private ParticleKey particleKey;
        private void Update()
        {
            if (particleKey.IsKey())
            {
                OnHook();
            }
        }
    }
}