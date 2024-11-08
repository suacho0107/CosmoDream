using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

[System.Serializable]
public class PlayerPositionData
{
    public string sceneName;  // 씬 이름을 저장
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
    public List<Puzzle> puzzles; // 퍼즐 조각들의 리스트
    public Transform playerTransform; // Stage5에서의 플레이어 Transform
    private string saveFilePath;

    void Start()
    {
        // 저장 경로 설정
        saveFilePath = Application.persistentDataPath + "/save.json";

        // Stage5로 돌아올 때 저장된 위치 복원
        if (SceneManager.GetActiveScene().name == "stage5")
        {
            LoadGameData();
        }

        foreach (var puzzle in puzzles)
        {
            puzzle.SetPuzzleManager(this); // 각 퍼즐 조각에 PuzzleManager를 설정
        }
    }

    public void CheckPuzzleCompletion()
    {
        foreach (var puzzle in puzzles)
        {
            if (!puzzle.IsLocked()) // 퍼즐 조각이 고정되지 않은 경우
            {
                return; // 게임이 아직 완료되지 않음
            }
        }

        Debug.Log("게임 성공!"); // 모든 퍼즐 조각이 고정된 경우

        // Stage5로 돌아가기 전에 플레이어 위치 저장
        SaveGameData();

        // Stage5로 이동
        SceneManager.LoadScene("stage5");
    }

    private void SaveGameData()
    {
        GameSaveData data = new GameSaveData
        {
            //name = "PlayerName", // 이름이 필요한 경우
            lastScene = SceneManager.GetActiveScene().name,
            playerPosition = new PlayerPositionData
            {
                sceneName = SceneManager.GetActiveScene().name,
                positionX = playerTransform.position.x,
                positionY = playerTransform.position.y,
                positionZ = playerTransform.position.z
            }
        };

        // JSON으로 직렬화 후 저장
        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, jsonData);

        Debug.Log("게임 상태가 저장되었습니다: " + jsonData);
    }

    private void LoadGameData()
    {
        if (File.Exists(saveFilePath))
        {
            // JSON 파일 읽기
            string jsonData = File.ReadAllText(saveFilePath);
            GameSaveData data = JsonUtility.FromJson<GameSaveData>(jsonData);

            // 저장된 씬과 일치하는 경우에만 위치 복원
            if (data.lastScene == SceneManager.GetActiveScene().name)
            {
                playerTransform.position = new Vector3(data.playerPosition.positionX, data.playerPosition.positionY, data.playerPosition.positionZ);
                Debug.Log("플레이어 위치가 로드되었습니다: " + jsonData);
            }
            else
            {
                Debug.Log("저장된 씬과 일치하지 않으므로 위치를 복원하지 않습니다.");
            }
        }
        else
        {
            Debug.Log("저장된 게임 상태가 없습니다.");
        }
    }
}
