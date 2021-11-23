using UnityEngine;

namespace  Particles.Scripts.ScriptableObjects.Audio
{
    [CreateAssetMenu(menuName = "Audio/Configuration")]
    public class AudioConfigurationSO : ScriptableObject
    {
        public AudioManager.AudioConfiguration m_Configuration;

        public float DEFAULT_VOLUME = 1f;
        public float DEFAULT_PITCH = 0f;

        public void Load(AudioData _audioData)
        {
            m_Configuration._volume = _audioData._volume;
            m_Configuration._pitch = _audioData._pitch;
            m_Configuration._loop = _audioData._loop;
        }
    }
}
