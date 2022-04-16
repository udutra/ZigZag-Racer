using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private int score = 0;
    private int highScore;
    public static GameManager instance;
    public bool gameStarted;
    public GameObject platformSpawner;
    public GameObject gamePlayUI, menuUI;
    public TextMeshProUGUI scoreText, highScoreText;
    

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    private void Start() {
        GetHighScore();
    }

    private void Update() {
        if (!gameStarted) {
            if (Input.GetMouseButtonDown(0)) {
                GameStart();
            }
        }
    }

    public void GameStart() {
        gameStarted = true;
        platformSpawner.SetActive(true);
        menuUI.SetActive(false);
        gamePlayUI.SetActive(true);
        StartCoroutine("UpdaterScore");
    }

    public void GameOver() {
        platformSpawner.SetActive(false);
        StopCoroutine("UpdaterScore");
        SaveHighScore();
        Invoke(nameof(ReloadLevel), 1f);
    }

    private void ReloadLevel() {
        SceneManager.LoadScene("Game");
    }

    private IEnumerator UpdaterScore() {
        while (true) {
            yield return new WaitForSeconds(1f);
            score++;
            scoreText.text = score.ToString();
        }
    }

    private void SaveHighScore() {
        if (PlayerPrefs.HasKey("HighScore")) {
            //we already have a highscore
            if (score > PlayerPrefs.GetInt("HighScore")) {
                PlayerPrefs.SetInt("HighScore", score);
            }
        }
        else {
            //playing for the first time
            PlayerPrefs.SetInt("HighScore", score);
        }
    }
    private void GetHighScore() {
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = "BEST SCORE: " + highScore;
    }
}
