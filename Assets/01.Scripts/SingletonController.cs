using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingletonController : MonoBehaviour
{
    public static SingletonController instance;

    // Data to save the state of each scene
    private Dictionary<string, SceneState> sceneStates = new Dictionary<string, SceneState>();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);  // Persistent across scenes
    }

    // Call this method before transitioning to a new scene
    public void SaveCurrentSceneState()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        // If the scene state already exists, update it
        if (sceneStates.ContainsKey(currentScene))
        {
            sceneStates[currentScene] = CaptureSceneState();
        }
        else
        {
            // Capture and store the scene state for the first time
            sceneStates.Add(currentScene, CaptureSceneState());
        }
    }

    // Call this method after loading a new scene to restore the previous state
    public void LoadSceneState()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (sceneStates.ContainsKey(currentScene))
        {
            RestoreSceneState(sceneStates[currentScene]);
        }
        else
        {
            Debug.Log("No saved state for this scene.");
        }
    }

    // Capture all relevant data of the current scene
    private SceneState CaptureSceneState()
    {
        SceneState state = new SceneState();
        
        // Example: Save position and state of all objects with the tag "Saveable"
        GameObject[] saveableObjects = GameObject.FindGameObjectsWithTag("Saveable");

        foreach (GameObject obj in saveableObjects)
        {
            SaveableObject saveable = new SaveableObject
            {
                position = obj.transform.position,
                isActive = obj.activeSelf
            };
            state.saveableObjects.Add(obj.name, saveable);
        }

        // You can also add player data, inventory, etc. here

        return state;
    }

    // Restore the scene state from the saved data
    private void RestoreSceneState(SceneState state)
    {
        foreach (KeyValuePair<string, SaveableObject> pair in state.saveableObjects)
        {
            GameObject obj = GameObject.Find(pair.Key);
            if (obj != null)
            {
                obj.transform.position = pair.Value.position;
                obj.SetActive(pair.Value.isActive);
            }
        }

        // Restore player data, inventory, etc. here
    }
}

[System.Serializable]
public class SceneState
{
    public Dictionary<string, SaveableObject> saveableObjects = new Dictionary<string, SaveableObject>();
}

[System.Serializable]
public class SaveableObject
{
    public Vector3 position;
    public bool isActive;
}
