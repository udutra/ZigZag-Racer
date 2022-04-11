using UnityEngine;

public class CarController : MonoBehaviour {

    public float moveSpeed;

    private void Update() {
        Move();
    }

    private void Move() {
        transform.position += moveSpeed * Time.deltaTime * Vector3.forward;
    }
}