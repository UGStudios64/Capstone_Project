using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class SaveSystem
{
    private static string path = Application.persistentDataPath + "/save.json";


    public static void Save(SaveData data)  // SAVE // / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / /
    {
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(path, json);
    }


    public static SaveData Load()  // LOAD // / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / /
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<SaveData>(json);
        }
        else
        {
            return new SaveData { unlockedLevel = 1 };
        }
    }
}