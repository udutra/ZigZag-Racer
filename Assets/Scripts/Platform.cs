using UnityEngine;

public class Platform : MonoBehaviour {

    public GameObject diamond;

    private void Start() {

        int randDiamond = Random.Range(0, 5);
        Vector3 diamondPos = transform.position;
        diamondPos.y += 1;

        if (randDiamond < 1) {
            GameObject diamondInstance = Instantiate(diamond, diamondPos, diamond.transform.rotation);
            diamondInstance.transform.SetParent(gameObject.transform);
        }
    }

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
