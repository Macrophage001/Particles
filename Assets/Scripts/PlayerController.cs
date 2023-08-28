using Particles.Packages.BaseParticles.Runtime.Constants;
using Particles.Packages.BaseParticles.Runtime.Variables;
using Particles.Packages.Core.Runtime;
using Particles.Packages.Core.Runtime.Attributes;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Player Controller")]
public class PlayerController : Particle
{
    [HorizontalLine(2)]
    [Header("Player Stats")]
    [SerializeField] private IntConstant maxHealth;
    [SerializeField] private IntVariable playerHealth;
    [SerializeField] private FloatVariable playerSpeed;
    [SerializeField] private BoolVariable playerIsAlive;

    public int PlayerHealth => playerHealth.Value;
    public float PlayerSpeed => playerSpeed.Value;
    public bool PlayerIsAlive => playerIsAlive.Value;
}