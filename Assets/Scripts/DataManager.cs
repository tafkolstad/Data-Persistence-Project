using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public string currentPlayerName;
    public List<int> highScores;
    public List<string> bestPlayerNames;

    void Awake()
    {
        if(Instance != null){
            Destroy(gameObject);

            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        LoadGame();
    }

    [System.Serializable]
    class SaveData{
        public List<string> bestPlayerNames;
        public List<int> highScores;
    }

    public void SaveGame(){
        SaveData data = new();
        data.bestPlayerNames = bestPlayerNames;
        data.highScores = highScores;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText($"{Application.persistentDataPath}/savefile.json", json);
    }

    void LoadGame(){
        string path = $"{Application.persistentDataPath}/savefile.json";

        if(File.Exists(path)){
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestPlayerNames = data.bestPlayerNames;
            highScores = data.highScores;
        }

    }
}
