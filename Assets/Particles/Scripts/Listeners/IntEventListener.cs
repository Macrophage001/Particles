using System;
using System.Collections.Generic;
using Actions;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class IntUnityEvent : UnityEvent<int> { }

public class IntEventListener : GenericListener<IntEventChannelSO, IntAction, IntUnityEvent, int> { }
