using UnityEngine;

public class Platform : MonoBehaviour {



    private void OnCollisionExit(Collision collision) {

        if (collision.gameObject.CompareTag("Player")) {
            Invoke(nameof(Fall), 0.2f);
        }
    }

    private void Fall() {
        GetComponent<Rigidbody>().isKinematic = false;
        Destroy(gameObject, 1f);
    }

}
