namespace Particles.Packages.Core.Runtime
{
    public enum DataLoadedFlag
    {
        LOADED,
        FAILED
    }

    public interface ISaveSystem<TData>
    {
        public void PreDataLoaded();
        public void OnDataLoaded(TData data);
        public void PostDataLoaded(DataLoadedFlag _flag) { }
    }
}