using System;
using Actions;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class GameObjectUnityEvent : UnityEvent<GameObject> {}

public class GameObjectEventListener : GenericListener<GameObjectEventChannelSO, GameObjectAction, GameObjectUnityEvent, GameObject> { }