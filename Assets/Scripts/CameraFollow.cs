using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private Vector3 distance;
    public Transform target;
    public float smoothValue;

    private void Start() {
        distance = target.position - transform.position;
    }

    private void Update() {

        if (target.position.y >= 0) {
            Follow();
        }
    }

    private void Follow() {
        Vector3 currentPos = transform.position;
        Vector3 targetPos = target.position - distance;

        transform.position = Vector3.Lerp(currentPos, targetPos, smoothValue * Time.deltaTime);
    }
}
