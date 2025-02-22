using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public string bestPlayerName;
    public string playerName;
    public int highScore;

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
        public string bestPlayerName;
        public int highScore;
    }

    public void SaveGame(){
        SaveData data = new();
        data.bestPlayerName = bestPlayerName;
        data.highScore = highScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText($"{Application.persistentDataPath}/savefile.json", json);
    }

    void LoadGame(){
        string path = $"{Application.persistentDataPath}/savefile.json";

        if(File.Exists(path)){
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestPlayerName = data.bestPlayerName;
            highScore = data.highScore;
        }

    }
}
