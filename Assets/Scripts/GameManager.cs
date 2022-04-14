using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private int score = 0;
    public static GameManager instance;
    public bool gameStarted;
    public GameObject platformSpawner;
    public GameObject gamePlayUI;
    public TextMeshProUGUI scoreText;
    

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
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
        gamePlayUI.SetActive(true);
        StartCoroutine(UpdaterScore());
    }

    public void GameOver() {
        platformSpawner.SetActive(false);
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
}
