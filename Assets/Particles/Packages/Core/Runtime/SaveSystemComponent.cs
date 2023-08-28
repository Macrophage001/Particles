using System;
using Particles.Packages.Core.Runtime.Managers;

namespace Particles.Packages.Core.Runtime
{
    public class SaveSystemComponent : DataManager<SaveData>
    {
        private const string SAVE_FILE = "save.bin";
    
        private void Awake()
        {
            Load(SAVE_FILE, PreDataLoaded, OnDataLoaded, PostDataLoaded);
        }
        private void OnApplicationQuit()
        {
            Save();
            ResetData();
        }

        public void PreDataLoaded() { }
        public void OnDataLoaded(SaveData data)
        {
        
        }
        public void PostDataLoaded(DataLoadedFlag flag) { }

        public override void Save()
        {
            SavingSystem.Save(SAVE_FILE, this);
        }
        public override void ResetData()
        {
        }
        public override SaveData CreateData()
        {
            return null;
        }
    }

    [Serializable]
    public class SaveData
    {
    }
}