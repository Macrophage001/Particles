namespace Particles.Packages.Core.Runtime.Hooks
{
    public class OnEnableHook : VoidHook 
    {
        private void OnEnable()
        {
            OnHook();
        }
    }
}