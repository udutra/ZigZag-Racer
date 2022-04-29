using UnityEngine;

public class CarController : MonoBehaviour {

    [SerializeField] private bool movingLeft = true;
    [SerializeField] private bool firstInput = true;
    [SerializeField] private bool gameOver = false;
    public GameObject pickUpEffect;
    public float moveSpeed;


    private void Update() {
        if (GameManager.instance.gameStarted) {
            Move();
            CheckInput();
        }

        if (transform.position.y <= -2) {
            if (!gameOver) {
                gameOver = true;
                GameManager.instance.GameOver();
            }
        }
    }

    private void Move() {
        transform.position += moveSpeed * Time.deltaTime * transform.forward;
    }

    private void CheckInput() {
        //if first input then ignore
        if (firstInput) {
            firstInput = false;
            return;
        }


        if (Input.GetMouseButtonDown(0)) {
            ChangeDirection();
        }
    }

    private void ChangeDirection() {
        if (movingLeft) {
            movingLeft = false;
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else {
            movingLeft = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Diamond")) {
            GameManager.instance.IncrementScore("diamond");
            Instantiate(pickUpEffect, other.transform.position, pickUpEffect.transform.rotation);
            //go.GetComponent<ParticleSystem>().Play();
            other.gameObject.SetActive(false);
        }
    }
}