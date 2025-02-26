using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text HighScoreText;
    public GameObject GameOverText;
    public Button HighScoreButton;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    private int maxHighScores = 8;

    
    // Start is called before the first frame update
    void Start()
    {
        if(DataManager.Instance.highScores.Count > 0){
            UpdateHighScoreText();
        }

        HighScoreButton.onClick.AddListener(GoToHighScores);

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        List<int> highScores = DataManager.Instance.highScores;

        if(highScores.Count == 0){
            DataManager.Instance.highScores.Add(m_Points);
            DataManager.Instance.bestPlayerNames.Add(DataManager.Instance.currentPlayerName);
        } else if(highScores.Count < maxHighScores){
            for (int i = 0; i < highScores.Count; i++)
            {
                if(m_Points > highScores[i]){
                    DataManager.Instance.highScores.Insert(i, m_Points);
                    DataManager.Instance.bestPlayerNames.Insert(i, DataManager.Instance.currentPlayerName);

                    break;
                }
            }
        } else {
            for (int i = 0; i < highScores.Count; i++)
            {
                if(m_Points > highScores[i]){
                    DataManager.Instance.highScores.RemoveAt(maxHighScores - 1);
                    DataManager.Instance.bestPlayerNames.RemoveAt(maxHighScores - 1);

                    DataManager.Instance.highScores.Insert(i, m_Points);
                    DataManager.Instance.bestPlayerNames.Insert(i, DataManager.Instance.currentPlayerName);

                    break;
                }
            }
        }

        UpdateHighScoreText();

        DataManager.Instance.SaveGame();

        m_GameOver = true;
        GameOverText.SetActive(true);
        HighScoreButton.gameObject.SetActive(true);
    }

    void UpdateHighScoreText(){
        HighScoreText = GameObject.Find("HighScoreText").GetComponent<Text>();

        string bestPlayerName = DataManager.Instance.bestPlayerNames[0];
        int highScore = DataManager.Instance.highScores[0];

        HighScoreText.text = $"Best Score: {bestPlayerName} : {highScore}";
    }

    void GoToHighScores(){
        SceneManager.LoadScene("highScore");
    }
}
