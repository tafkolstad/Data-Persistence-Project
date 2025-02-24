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

        if(DataManager.Instance.highScore > 0){
            highScores.text = $"Name Score\n{DataManager.Instance.bestPlayerName} {DataManager.Instance.highScore}";
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
