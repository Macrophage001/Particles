using System;
using Actions;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class FloatUnityEvent : UnityEvent<float> {}

public class FloatEventListener : GenericListener<FloatEventChannelSO, FloatAction, FloatUnityEvent, float> { }