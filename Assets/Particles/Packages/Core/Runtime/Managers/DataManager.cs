using System;
using UnityEngine;

namespace Particles.Packages.Core.Runtime.Managers
{
    public abstract class DataManager<TData> : MonoBehaviour
    {
        [HideInInspector] public DataLoadedFlag m_DataLoadedFlag;

        protected TData data;
    
        public abstract void Save();

        public void Load(
            string _filePath,
            Action _preDataLoaded,
            Action<TData> _onDataLoaded,
            Action<DataLoadedFlag> _postDataLoaded)
        {
            Debug.Log($"Loading Data from: {_filePath}");

            _preDataLoaded?.Invoke();

            data = Load(_filePath);
            if (data != null)
            {
                _onDataLoaded?.Invoke(data);
                m_DataLoadedFlag = DataLoadedFlag.LOADED;
            }
            else
            {
                m_DataLoadedFlag = DataLoadedFlag.FAILED;
            }


            _postDataLoaded?.Invoke(m_DataLoadedFlag);
        }

        private TData Load(string _filePath)
        {
            return SavingSystem.LoadDataOfType<TData>(_filePath);
        }
        public abstract void ResetData();
        public abstract TData CreateData();
    }
}
