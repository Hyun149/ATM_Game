using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonUserDataStorage : IUserDataStorage
{
    private readonly string savePath;

    public JsonUserDataStorage(string filename = "saveData.json")
    {
        savePath = Path.Combine(Application.persistentDataPath, filename);
    }

    public UserDataList Load()
    {
        if (!File.Exists(savePath))
        {
            return new UserDataList();
        }

        var json = File.ReadAllText(savePath);
        return JsonUtility.FromJson<UserDataList>(json);
    }

    public void Save(UserDataList data)
    {
        var json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
    }
}
