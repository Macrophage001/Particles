using UnityEngine;

namespace Particles.Packages.Core.Runtime.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Scene Data")]
    public class SceneSO : ScriptableObject
    {
        [Header("Scene Information")] 
        public string m_SceneName;
        public string m_ShortDescription;
    }
}
