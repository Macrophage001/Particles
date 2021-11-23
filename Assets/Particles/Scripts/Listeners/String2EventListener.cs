using System;
using Actions;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class String2UnityEvent : UnityEvent<string, string> { }

public class String2EventListener : GenericListener<String2EventChannelSO, String2Action, String2UnityEvent, string, string>
{ }
