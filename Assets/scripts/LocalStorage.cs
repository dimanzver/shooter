using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

public class LocalStorage
{
    public static object getObject(string key)
    {
        object info = JsonUtility.FromJson<object>(PlayerPrefs.GetString(key));
        return info;
    }

    public static void setObject(string key, object value)
    {
        string stringValue = JsonUtility.ToJson(value);
        PlayerPrefs.SetString(key, stringValue);
    } 

    public static Dictionary<string,object> getDictionary(string key)
    {
        Dictionary<string, object> info = JsonConvert.DeserializeObject<Dictionary<string, object>>(PlayerPrefs.GetString(key));
        return info;
    }

    public static void setDictionary(string key, Dictionary<string,object> value)
    {
        string stringValue = JsonConvert.SerializeObject(value);
        PlayerPrefs.SetString(key, stringValue);
    }
}
