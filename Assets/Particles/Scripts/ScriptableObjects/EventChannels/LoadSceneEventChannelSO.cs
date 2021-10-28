using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Load Scene Event Channel")]
public class LoadSceneEventChannelSO : ScriptableObject
{
    public UnityAction<string[], bool> OnLoadingRequested;
    public UnityAction<string, bool> OnLoadingRequestedSingle;

    public void RaiseEvent(string[] _sceneNames, bool _showLoadingScreen = true)
    {
        OnLoadingRequested?.Invoke(_sceneNames, _showLoadingScreen);

        if (OnLoadingRequested == null)
        {
            Debug.LogWarning("Multi Scene loading was requested but no SceneLoader responded!");
        }
    }

    public void RaiseEvent(string _sceneName, bool _showLoadingScreen = true)
    {
        OnLoadingRequestedSingle?.Invoke(_sceneName, _showLoadingScreen);
        if (OnLoadingRequestedSingle == null)
        {
            Debug.LogWarning("Single Scene loading was requested but no SceneLoader responded!");
        }
    }
}
