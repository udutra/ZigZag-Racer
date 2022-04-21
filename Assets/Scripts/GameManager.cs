using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    
    public static GameManager instance;

    private int score = 0;
    private int highScore;
    private AudioSource audioSource;
    [SerializeField] private int timeScore, diamondScore;


    public bool gameStarted;
    public GameObject platformSpawner;
    public GameObject gamePlayUI, menuUI;
    public TextMeshProUGUI scoreText, highScoreText;
    public AudioClip[] gameMusic;

    
    

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        audioSource = GetComponent<AudioSource>();
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
        audioSource.clip = gameMusic[1];
        audioSource.Play();
        StartCoroutine(nameof(UpdaterScore));
    }

    public void GameOver() {
        platformSpawner.SetActive(false);
        StopCoroutine(nameof(UpdaterScore));
        SaveHighScore();
        Invoke(nameof(ReloadLevel), 1f);
    }

    private void ReloadLevel() {
        SceneManager.LoadScene("Game");
    }

    private IEnumerator UpdaterScore() {
        while (true) {
            yield return new WaitForSeconds(1f);
            IncrementScore("time");
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

    public void IncrementScore(string item) {
        int val;

        Debug.Log(item);

        switch (item) {
            case "time": {
                    val = timeScore;
                    break;
                }
            case "diamond": {
                    val = diamondScore;
                    audioSource.PlayOneShot(gameMusic[2],0.2f);

                    break;
                }
            default: {
                    val = -999;
                    break;
                }
        }
        Debug.Log(val);
        score += val;
        scoreText.text = score.ToString();
    }
}
