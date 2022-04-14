using System.Collections;
using UnityEngine;

public class PlataformSpawner : MonoBehaviour {

    public GameObject platformPrefab;
    public Transform lastPlataform, platformsParent;
    public Vector3 lastPosition;
    public Vector3 newPosition;
    public bool stop;

    private void Start() {
        lastPosition = lastPlataform.position;
        StartCoroutine(SpawnPlataforms());
    }

    private IEnumerator SpawnPlataforms() {
        while (!stop) {
            GeneratePosition();
            Instantiate(platformPrefab, newPosition, Quaternion.identity, platformsParent);
            lastPosition = newPosition;

            yield return new WaitForSeconds(0.1f);
        }

    }

    private void GeneratePosition() {
        newPosition = lastPosition;

        int rand = Random.Range(0, 2);

        if (rand > 0) {
            newPosition.x += 2f;
        }
        else {
            newPosition.z += 2f;
        }
    }
}