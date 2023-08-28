using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Particles.Packages.Core.Runtime.Managers;
using UnityEngine;
using static Particles.Packages.Core.Runtime.Helpers.Utils;

namespace Particles.Packages.Core.Runtime
{
    public static class SavingSystem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"> The Data class type to use.</typeparam>
        /// <param name="_filepath"></param>
        /// <returns></returns>
        public static T LoadDataOfType<T>(string _filepath)
        {
            string _path = Application.persistentDataPath + "/" + _filepath;
            T _data = default;
            if (File.Exists(_path))
            {
                return Using(new FileStream(_path, FileMode.Open), _stream =>
                {
                    BinaryFormatter _binaryFormatter = new BinaryFormatter();
                    try
                    {
                        _data = (T)_binaryFormatter.Deserialize(_stream);
                    }
                    catch (InvalidCastException _e)
                    {
                        Debug.LogWarning(_e.ToString());
                    }
                    return _data;
                });
            }
            return _data;
        }

        public static void Save<T>(string _fileName, DataManager<T> _manager)
        {
            string _fullPath = Application.persistentDataPath + "/" + _fileName;
            using (var _stream = new FileStream(_fullPath, FileMode.Create))
            {
                BinaryFormatter _binaryFormatter = new BinaryFormatter();
                T _data = _manager.CreateData();
                _binaryFormatter.Serialize(_stream, _data);
            }
        }
    }
}
