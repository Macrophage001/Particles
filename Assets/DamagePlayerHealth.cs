using Particles.Packages.BaseParticles.Runtime.Variables;
using UnityEngine;

public class DamagePlayerHealth : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private IntVariable playerHealth;
    
    public void DamageHealth()
    {
        playerHealth.Value -= damageAmount;
    }
}
