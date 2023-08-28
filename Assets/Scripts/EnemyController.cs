using Particles.Packages.BaseParticles.Runtime.Variables;
using Particles.Packages.Core.Runtime;
using UnityEngine;

public class EnemyController : Particle 
{
    [SerializeField] private IntVariable enemyHealth;
    [SerializeField] private FloatVariable enemySpeed;
    [SerializeField] private BoolVariable enemyIsAlive;

    public int EnemyHealth => enemyHealth.Value;
    public float EnemySpeed => enemySpeed.Value;
    public bool EnemyIsAlive => enemyIsAlive.Value;
}
