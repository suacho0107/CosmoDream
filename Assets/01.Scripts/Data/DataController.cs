using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataController : MonoBehaviour
{
    #region 싱글톤
    static GameObject container;
    static GameObject Container
    {
        get
        {
            return container;
        }
    }
    static DataController instance;
    public static DataController Instance
    {
        get
        {
            if (!instance)
            {
                container = new GameObject();
                container.name = "DataController";
                instance = container.AddComponent(typeof(DataController)) as DataController;
                DontDestroyOnLoad(container);
            }
            return instance;
        }
    }
    #endregion

    public string GameDataFileName = "save.json"; //변경 절대 xxxx

    public GameData _gameData;
    public GameData gameData
    {
        get
        {
            if(_gameData == null)
            {
                LoadGameData();
                SaveGameData();
            }
            return _gameData;
        }
    }

    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + GameDataFileName;
        if (File.Exists(filePath))
        {
            Debug.Log("불러오기 성공");
            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);
        }
        else
        {
            Debug.Log("새로운 파일 생성");
            _gameData = new GameData();
        }
    }

    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + GameDataFileName;
        File.WriteAllText(filePath, ToJsonData);
        Debug.Log("저장 완료");
    }

    /*
    private void OnApplicationQuit()
    {
        SaveGameData();
    }
    */
}
