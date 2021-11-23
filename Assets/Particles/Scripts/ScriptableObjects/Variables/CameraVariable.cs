using Particles.Scripts.ScriptableObjects.Variables;
using UnityEngine;


[CreateAssetMenu(menuName = "Variables/Camera", fileName ="Camera")]
[System.Serializable]
public class CameraVariable : Variable<Camera>
{
    public override bool Equals(Camera _value, Camera _other)
    {
        return _value.Equals(_other);
    }
}
