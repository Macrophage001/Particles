using UnityEngine;

namespace Particles.Packages.Core.Runtime
{
    public class PoolMasterGO : MonoBehaviour
    {
        public PoolMaster poolMaster;
        public Transform[] poolTransforms;

        private void Awake()
        {
            poolMaster.Create(poolTransforms);
        }
    }
}
