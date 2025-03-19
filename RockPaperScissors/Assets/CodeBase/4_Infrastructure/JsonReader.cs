using System.Collections;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System;

public class JsonReader : MonoBehaviour, IDataProvider
{
    public void RequestData<T>(string dataId, Action<T> callback)
    {
        StartCoroutine(LoadJsonCoroutine($"{dataId}.json", callback));
    }

    private IEnumerator LoadJsonCoroutine<T>(string relativePath, Action<T> callback)
    {
        string path = Path.Combine(Application.streamingAssetsPath, relativePath);

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            yield return null;

            try
            {
                T data = JsonConvert.DeserializeObject<T>(json);
                callback?.Invoke(data);
            }
            catch (Exception ex)
            {
                Debug.LogError($"JSON deserialization failed: {ex.Message}");
            }
        }
        else
        {
            Debug.LogError("File not found: " + path);
        }
    }
}
