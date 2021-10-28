using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class AudioUnityEvent : UnityEvent<AudioManager.AudioConfiguration> { }

public class AudioEventListener : GenericListener<AudioEventChannelSO, AudioAction, AudioUnityEvent, AudioManager.AudioConfiguration> { }
