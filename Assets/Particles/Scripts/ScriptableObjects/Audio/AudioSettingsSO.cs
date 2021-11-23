using System.Collections;
using System.Collections.Generic;
using Particles.Scripts.ScriptableObjects.Audio;
using UnityEngine;

namespace Particles.Scripts.ScriptableObjects.Audio
{
    [CreateAssetMenu(menuName = "Audio/Audio Settings")]
    public class AudioSettingsSO : ScriptableObject
    {
        public AudioConfigurationSO[] m_AudioConfigurations;

        public float DEFAULT_VOLUME = 1f;
        public float DEFAULT_PITCH = 0f;
        public bool DEFAULT_LOOP = false;
        
        public void SetVolume(float _volume)
        {
            foreach (var _audioConfiguration in m_AudioConfigurations)
            {
                _audioConfiguration.m_Configuration._volume = _volume;
            }
        }

        public void SetPitch(float _pitch)
        {
            foreach (var _audioConfiguration in m_AudioConfigurations)
            {
                _audioConfiguration.m_Configuration._pitch = _pitch;
            }
        }

        public void SetLoop(bool _loop)
        {
            foreach (var _audioConfiguration in m_AudioConfigurations)
            {
                _audioConfiguration.m_Configuration._loop = _loop;
            }
        }

        public void LoadDefaultVolumes()
        {
            foreach (var _audioConfiguration in m_AudioConfigurations)
            {
                _audioConfiguration.m_Configuration._volume = _audioConfiguration.DEFAULT_VOLUME;
            }
        }

        public void LoadDefaultPitch()
        {
            foreach (var _audioConfiguration in m_AudioConfigurations)
            {
                _audioConfiguration.m_Configuration._pitch = _audioConfiguration.DEFAULT_PITCH;
            }
        }
    }
}

