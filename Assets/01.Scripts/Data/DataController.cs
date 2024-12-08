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

    string GameDataFileName = "save.json"; //변경 절대 xxxx
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
                Debug.Log("초기 save.json 파일 복사 완료");
            }
            else
            {
                Debug.LogWarning("StreamingAssets에 초기 save.json 파일이 없습니다. 기본 데이터로 시작합니다.");
                _gameData = new GameData();
                SaveGameData();
            }
        }
    }

    public void LoadGameData()
    {
        if (File.Exists(saveFilePath))
        {
            Debug.Log("불러오기 성공");
            string json = File.ReadAllText(saveFilePath);
            _gameData = JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            Debug.LogWarning("save.json 파일이 없어 초기화된 데이터를 생성합니다.");
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
            Debug.LogError($"게임 데이터를 저장하는 중 오류 발생: {e.Message}");
        }
    }

    //jsonFile있는지 검사

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
