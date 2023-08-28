namespace Particles.Packages.Core.Runtime.Hooks
{
    public class OnDestroyHook : VoidHook
    {
        private void OnDestroy()
        {
            OnHook();
        }
    }
}