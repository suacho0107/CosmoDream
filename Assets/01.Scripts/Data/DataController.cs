using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataController : MonoBehaviour
{
    public static DataController Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            saveFilePath = Path.Combine(Application.persistentDataPath);
            InitializeSaveFile();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    string GameDataFileName = "save.json"; //���� ���� xxxx
    string saveFilePath;

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

    void InitializeSaveFile()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, GameDataFileName);
        
        if (!File.Exists(saveFilePath))
        {
            string sourcePath = Path.Combine(Application.streamingAssetsPath, GameDataFileName);
            if (File.Exists(sourcePath))
            {
                File.Copy(sourcePath, saveFilePath);
                Debug.Log("�ʱ� save.json ���� ���� �Ϸ�");
            }
            else
            {
                Debug.LogWarning("StreamingAssets�� �ʱ� save.json ������ �����ϴ�. �⺻ �����ͷ� �����մϴ�.");
                _gameData = new GameData();
                SaveGameData();
            }
        }
    }

    public void LoadGameData()
    {
        if (File.Exists(saveFilePath))
        {
            Debug.Log("�ҷ����� ����");
            string json = File.ReadAllText(saveFilePath);
            _gameData = JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            Debug.LogWarning("save.json ������ ���� �ʱ�ȭ�� �����͸� �����մϴ�.");
            _gameData = new GameData();
            SaveGameData();
        }
    }

    public void SaveGameData()
    {
        try
        {
            string json = JsonUtility.ToJson(gameData, true);
            File.WriteAllText(saveFilePath, json);
        }
        catch (IOException e)
        {
            Debug.LogError($"���� �����͸� �����ϴ� �� ���� �߻�: {e.Message}");
        }
    }

    //jsonFile�ִ��� �˻�

    public bool isSave()
    {
        return File.Exists(saveFilePath);
    }

    /*
    private void OnApplicationQuit()
    {
        SaveGameData();
    }
    */
}
