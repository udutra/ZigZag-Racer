using System.Collections;
using UnityEngine;

public class PlataformSpawner : MonoBehaviour {

    public GameObject platformPrefab;
    public Transform lastPlataform;
    public Vector3 lastPosition;
    public Vector3 newPosition;
    public bool stop;

    private void Start() {
        lastPosition = lastPlataform.position;
        StartCoroutine(SpawnPlataforms());
    }

    private void Update() {
        /*if (Input.GetKeyDown(KeyCode.Space)) {
            SpawnPlataforms();
        }*/

        

    }

    private IEnumerator SpawnPlataforms() {
        while (!stop) {
            GeneratePosition();
            Instantiate(platformPrefab, newPosition, Quaternion.identity);
            lastPosition = newPosition;

            yield return new WaitForSeconds(0.1f);
        }

    }

    /*private void SpawnPlataforms() {
        GeneratePosition();
        Instantiate(plataformPrefab, newPosition, Quaternion.identity);
        lastPosition = newPosition;
    }*/

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