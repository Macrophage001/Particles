using System;
using Particles.Scripts.ScriptableObjects.Variables;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/DateTime")]
public class DateTimeVariable : Variable<System.DateTime>
{
    public override bool Equals(DateTime _value, DateTime _other)
    {
        return _value.Equals(_other);
    }
}
