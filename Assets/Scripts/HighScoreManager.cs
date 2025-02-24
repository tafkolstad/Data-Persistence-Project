using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    TMP_Text highScores;

    Button menuButton;
    Button gameButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        highScores = GameObject.Find("Scores").GetComponent<TMP_Text>();
        menuButton = GameObject.Find("MenuButton").GetComponent<Button>();
        gameButton = GameObject.Find("GameButton").GetComponent<Button>();

        highScores.text = "Name Score\n";


        if(DataManager.Instance.highScores.Count > 0){
            for (int i = 0; i < DataManager.Instance.highScores.Count; i++)
            {
                Debug.Log($"Highscore: {DataManager.Instance.highScores[i]}");
                highScores.text += $"{DataManager.Instance.bestPlayerNames[i]} {DataManager.Instance.highScores[i]}\n";
            }
        }

        menuButton.onClick.AddListener(GoToMenu);
        gameButton.onClick.AddListener(StartGame);
    }

    void GoToMenu(){
        SceneManager.LoadScene("menu");
    }

    void StartGame(){
        SceneManager.LoadScene("main");
    }
}
