namespace Particles.Packages.Core.Runtime.Hooks
{
    public class OnDisableHook : VoidHook 
    {
        private void OnDisable()
        {
            OnHook();
        }
    }
}