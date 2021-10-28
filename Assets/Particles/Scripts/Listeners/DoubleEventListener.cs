using System;
using Actions;
using UnityEngine.Events;

[Serializable]
public class DoubleUnityEvent : UnityEvent<double> { }

public class DoubleEventListener : GenericListener<DoubleEventChannelSO, DoubleAction, DoubleUnityEvent, double>
{
}
