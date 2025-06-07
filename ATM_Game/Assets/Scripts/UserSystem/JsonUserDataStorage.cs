using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// <b>JSON 파일 기반의 유저 데이터 저장소 클래스입니다.</b><br/>
/// - 유저 데이터를 JSON 형식으로 저장하고 불러옵니다.<br/>
/// - 파일 경로는 Application.persistentDataPath 기준으로 설정됩니다.
/// </summary>
public class JsonUserDataStorage : IUserDataStorage
{
    private readonly string savePath;

    /// <summary>
    /// 생성자: 저장 파일 이름을 지정하여 저장소를 초기화합니다.
    /// </summary>
    /// <param name="filename">저장할 JSON 파일명 (기본값: saveData.json)</param>
    public JsonUserDataStorage(string filename = "saveData.json")
    {
        savePath = Path.Combine(Application.persistentDataPath, filename);
    }

    /// <summary>
    /// JSON 파일로부터 유저 데이터를 불러옵니다.<br/>
    /// - 파일이 존재하지 않으면 빈 UserDataList를 반환합니다.
    /// </summary>
    /// <returns>역직렬화된 유저 데이터 리스트</returns>
    public UserDataList Load()
    {
        if (!File.Exists(savePath))
        {
            return new UserDataList();
        }

        var json = File.ReadAllText(savePath);
        return JsonUtility.FromJson<UserDataList>(json);
    }

    /// <summary>
    /// 유저 데이터를 JSON으로 직렬화하여 저장합니다.
    /// </summary>
    /// <param name="data">저장할 유저 데이터 리스트</param>
    public void Save(UserDataList data)
    {
        var json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
    }
}
