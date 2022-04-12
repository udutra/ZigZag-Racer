using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public bool gameStarted;
    public GameObject platformSpawner;

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
    }

    public void GameOver() {
        platformSpawner.SetActive(false);
        Invoke(nameof(ReloadLevel), 1f);

        //gameStarted = false;
    }

    private void ReloadLevel() {
        SceneManager.LoadScene("Game");
    }
}
