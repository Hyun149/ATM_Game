using UnityEngine;
using System.IO;

public class UserDataManager
{
    private const string FileName = "saveData.json";
    public UserData Data {  get; private set; }

    private string SavePath => Path.Combine(Application.persistentDataPath, FileName);

    public void SaveUserData()
    {
        string Json = JsonUtility.ToJson(Data);
        File.WriteAllText(SavePath, Json);
    }

    public void LoadUserData()
    {
        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            Data = JsonUtility.FromJson<UserData>(json);
            Debug.Log("유저 데이터 로드 완료");
        }
        else
        {
            Data = new UserData("조현성", "Hyun99", "9999", 10000000, 20000000);
            Debug.Log("저장 파일 없음!, 기본값 사용");
        }
    }
}
