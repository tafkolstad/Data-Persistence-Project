using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Button startButton;
    Button quitButton;
    TMP_InputField inputField;
    TMP_Text highScoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startButton = GameObject.Find("StartButton").GetComponent<Button>();
        quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
        inputField = GameObject.Find("PlayerNameInput").GetComponent<TMP_InputField>();

        if(DataManager.Instance.highScore > 0){
            highScoreText = GameObject.Find("HighScoreText").GetComponent<TMP_Text>();
            highScoreText.text = $"Best Score: {DataManager.Instance.highScore} : {DataManager.Instance.bestPlayerName}";

        }

        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    void StartGame(){
        Debug.Log(inputField.text);
        DataManager.Instance.playerName = inputField.text;

        SceneManager.LoadScene("main");
    }

    void QuitGame(){
        if(Application.isEditor){
            EditorApplication.ExitPlaymode();

            return;
        }

        Application.Quit();
    }
}
