using System;
using Actions;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class GameObject2UnityEvent : UnityEvent<GameObject, GameObject> { }

public class GameObject2EventListener : GenericListener<GameObject2EventChannelSO, GameObject2Action, GameObject2UnityEvent, GameObject, GameObject>
{ }
