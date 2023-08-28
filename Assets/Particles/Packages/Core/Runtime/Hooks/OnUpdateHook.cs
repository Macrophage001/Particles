namespace Particles.Packages.Core.Runtime.Hooks
{
    public class OnUpdateHook : VoidHook 
    {
        private void Update()
        {
            OnHook();
        }
    }
}