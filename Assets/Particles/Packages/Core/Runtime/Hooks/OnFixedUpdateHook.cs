namespace Particles.Packages.Core.Runtime.Hooks
{
    public class OnFixedUpdateHook : VoidHook
    {
        private void OnFixedUpdate()
        {
            OnHook();
        }
    }
}