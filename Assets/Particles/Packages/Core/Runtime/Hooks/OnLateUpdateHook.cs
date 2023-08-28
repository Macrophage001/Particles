namespace Particles.Packages.Core.Runtime.Hooks
{
    public class OnLateUpdateHook : VoidHook
    {
        private void LateUpdate()
        {
            OnHook();
        }
    }
}