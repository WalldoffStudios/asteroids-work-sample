using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using VContainer.Unity;

namespace Asteroids.Utilities
{
    public class SaveManager : ISaveManager, IInitializable, IDisposable
    {
        private const string SaveFileName = "saveData.json";

        private readonly Dictionary<string, object> _saveDataDictionary = new Dictionary<string, object>();

        public void Initialize()
        {
            Load();
            Application.quitting += Save;
        }

        public void Dispose()
        {
            Application.quitting -= Save;
        }

        public void SetData<T>(string key, T value)
        {
            _saveDataDictionary[key] = value;
        }

        public T GetData<T>(string key, T defaultValue = default)
        {
            if (_saveDataDictionary.TryGetValue(key, out object value))
            {
                if (value is T typedValue)
                {
                    return typedValue;
                }
                else
                {
                    Debug.LogWarning($"Type mismatch for key: {key}. Expected {typeof(T)}, found {value.GetType()}.");
                    return defaultValue;
                }
            }
            return defaultValue;
        }

        public void Save()
        {
            SaveDataWrapper wrapper = new SaveDataWrapper();

            foreach (var kvp in _saveDataDictionary)
            {
                if (kvp.Value is int intValue)
                {
                    wrapper.IntSaveDataList.Add(new IntSaveData { Key = kvp.Key, Value = intValue });
                }
                else if (kvp.Value is float floatValue)
                {
                    wrapper.FloatSaveDataList.Add(new FloatSaveData { Key = kvp.Key, Value = floatValue });
                }
                else if (kvp.Value is string stringValue)
                {
                    wrapper.StringSaveDataList.Add(new StringSaveData { Key = kvp.Key, Value = stringValue });
                }
                else
                {
                    Debug.LogWarning($"Unsupported data type for key: {kvp.Key}");
                }
            }

            string json = JsonUtility.ToJson(wrapper, true);
            Debug.Log($"Saving JSON: {json}");
            File.WriteAllText(GetSaveFilePath(), json);
            Debug.Log("Game data saved.");
        }

        public void Load()
        {
            string path = GetSaveFilePath();
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                Debug.Log($"Loading JSON: {json}");
                SaveDataWrapper wrapper = JsonUtility.FromJson<SaveDataWrapper>(json);

                if (wrapper == null)
                {
                    Debug.LogWarning("Failed to deserialize save data. Wrapper is null.");
                    return;
                }

                _saveDataDictionary.Clear();

                if (wrapper.IntSaveDataList != null)
                {
                    foreach (var data in wrapper.IntSaveDataList)
                    {
                        _saveDataDictionary[data.Key] = data.Value;
                    }
                }

                if (wrapper.FloatSaveDataList != null)
                {
                    foreach (var data in wrapper.FloatSaveDataList)
                    {
                        _saveDataDictionary[data.Key] = data.Value;
                    }
                }

                if (wrapper.StringSaveDataList != null)
                {
                    foreach (var data in wrapper.StringSaveDataList)
                    {
                        _saveDataDictionary[data.Key] = data.Value;
                    }
                }

                Debug.Log("Game data loaded.");
            }
            else
            {
                Debug.Log("No save file found. Starting fresh.");
            }
        }


        private string GetSaveFilePath()
        {
            return Path.Combine(Application.persistentDataPath, SaveFileName);
        }

        [Serializable]
        private class SaveDataWrapper
        {
            public List<IntSaveData> IntSaveDataList = new List<IntSaveData>();
            public List<FloatSaveData> FloatSaveDataList = new List<FloatSaveData>();
            public List<StringSaveData> StringSaveDataList = new List<StringSaveData>();
        }
    }
}
