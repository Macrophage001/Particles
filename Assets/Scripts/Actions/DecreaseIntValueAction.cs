using Particles.Packages.BaseParticles.Runtime.Variables;
using Particles.Packages.Core.Runtime.ScriptableObjects.Actions;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Actions/Decrease Int Value")]
public class DecreaseIntValueAction : ParticleAction
{
    [SerializeField] private IntVariable intVariable;
    [SerializeField] private int value;
    
    public override void Do()
    {
        intVariable.Value -= value;
    }
}
