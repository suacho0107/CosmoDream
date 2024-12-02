using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataController : MonoBehaviour
{

    #region �̱���
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

    //private void Awake()
    //{
    //    DontDestroyOnLoad(this);
    //}
    public string GameDataFileName = "save.json"; //���� ���� xxxx

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

    public void newGameData()
    {
        _gameData = new GameData();

        SaveGameData();
    }

    public void LoadGameData()
    {
        string filePath = "Assets/" + GameDataFileName;
        if (File.Exists(filePath))
        {
            Debug.Log("�ҷ����� ����");
            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);
        }
        else
        {
            // GameData ��ü �ʱ�ȭ
            _gameData = new GameData();
            SaveGameData();
        }
    }

    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(gameData, true);
        string filePath = "Assets/" + GameDataFileName;
        File.WriteAllText(filePath, ToJsonData);
    }

    //jsonFile�ִ��� �˻�

    public bool isSave()
    {
        string filePath = "Assets/" + GameDataFileName;

        if (File.Exists(filePath))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /*
    private void OnApplicationQuit()
    {
        SaveGameData();
    }
    */
}
