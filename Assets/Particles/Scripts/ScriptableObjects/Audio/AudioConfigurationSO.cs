using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  Particles.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Audio/Configuration")]
    public class AudioConfigurationSO : ScriptableObject
    {
        public AudioManager.AudioConfiguration m_Configuration;
    }
}
