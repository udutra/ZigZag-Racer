using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    private int score = 0;
    private int highScore;
    [SerializeField] private int adCounter = 0;
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
        CheckAdCount();
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

        //show ad
        //#if UNITY_ANDROID
        //AdsManager.instance.ShowAd();
        //AdsManager.instance.ShowRewaredAd();
        //#endif

        //Invoke(nameof(ReloadLevel), 1f);


        if (adCounter >=4) {
            adCounter = 0;
            PlayerPrefs.SetInt("AdCount", adCounter);
            AdsManager.instance.ShowRewaredAd();
        }
        else {
            Invoke(nameof(ReloadLevel), 1f);
        }
    }

    public void ReloadLevel() {
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

        switch (item) {
            case "time": {
                    val = timeScore;
                    break;
                }
            case "diamond": {
                    val = diamondScore;
                    audioSource.PlayOneShot(gameMusic[2], 0.2f);

                    break;
                }
            default: {
                    val = -999;
                    break;
                }
        }

        score += val;
        scoreText.text = score.ToString();
    }

    private void CheckAdCount() {
        if (PlayerPrefs.HasKey("AdCount")) {
            adCounter = PlayerPrefs.GetInt("AdCount");
            adCounter++;

            PlayerPrefs.SetInt("AdCount", adCounter);
        }
        else {
            PlayerPrefs.SetInt("AdCount", 0);
        }
    }
}
