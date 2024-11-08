using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

[System.Serializable]
public class PlayerPositionData
{
    public string sceneName;  // �� �̸��� ����
    public float positionX;
    public float positionY;
    public float positionZ;
}

[System.Serializable]
public class GameSaveData
{
    public string name;
    public string lastScene;
    public PlayerPositionData playerPosition;
}

public class PuzzleManager : MonoBehaviour
{
    public List<Puzzle> puzzles; // ���� �������� ����Ʈ
    public Transform playerTransform; // Stage5������ �÷��̾� Transform
    private string saveFilePath;

    void Start()
    {
        // ���� ��� ����
        saveFilePath = Application.persistentDataPath + "/save.json";

        // Stage5�� ���ƿ� �� ����� ��ġ ����
        if (SceneManager.GetActiveScene().name == "stage5")
        {
            LoadGameData();
        }

        foreach (var puzzle in puzzles)
        {
            puzzle.SetPuzzleManager(this); // �� ���� ������ PuzzleManager�� ����
        }
    }

    public void CheckPuzzleCompletion()
    {
        foreach (var puzzle in puzzles)
        {
            if (!puzzle.IsLocked()) // ���� ������ �������� ���� ���
            {
                return; // ������ ���� �Ϸ���� ����
            }
        }

        Debug.Log("���� ����!"); // ��� ���� ������ ������ ���

        // Stage5�� ���ư��� ���� �÷��̾� ��ġ ����
        SaveGameData();

        // Stage5�� �̵�
        SceneManager.LoadScene("stage5");
    }

    private void SaveGameData()
    {
        GameSaveData data = new GameSaveData
        {
            //name = "PlayerName", // �̸��� �ʿ��� ���
            lastScene = SceneManager.GetActiveScene().name,
            playerPosition = new PlayerPositionData
            {
                sceneName = SceneManager.GetActiveScene().name,
                positionX = playerTransform.position.x,
                positionY = playerTransform.position.y,
                positionZ = playerTransform.position.z
            }
        };

        // JSON���� ����ȭ �� ����
        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, jsonData);

        Debug.Log("���� ���°� ����Ǿ����ϴ�: " + jsonData);
    }

    private void LoadGameData()
    {
        if (File.Exists(saveFilePath))
        {
            // JSON ���� �б�
            string jsonData = File.ReadAllText(saveFilePath);
            GameSaveData data = JsonUtility.FromJson<GameSaveData>(jsonData);

            // ����� ���� ��ġ�ϴ� ��쿡�� ��ġ ����
            if (data.lastScene == SceneManager.GetActiveScene().name)
            {
                playerTransform.position = new Vector3(data.playerPosition.positionX, data.playerPosition.positionY, data.playerPosition.positionZ);
                Debug.Log("�÷��̾� ��ġ�� �ε�Ǿ����ϴ�: " + jsonData);
            }
            else
            {
                Debug.Log("����� ���� ��ġ���� �����Ƿ� ��ġ�� �������� �ʽ��ϴ�.");
            }
        }
        else
        {
            Debug.Log("����� ���� ���°� �����ϴ�.");
        }
    }
}
