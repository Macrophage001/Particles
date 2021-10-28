using System;
using System.Collections.Generic;
using Actions;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class BoolUnityEvent : UnityEvent<bool> { }

public class BoolEventListener : GenericListener<BoolEventChannelSO, BoolAction, BoolUnityEvent, bool> { }
