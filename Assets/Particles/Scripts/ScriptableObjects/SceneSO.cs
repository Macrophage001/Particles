using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Scene Data")]
public class SceneSO : ScriptableObject
{
    [Header("Scene Information")] 
    public string m_SceneName;
    public string m_ShortDescription;
}
