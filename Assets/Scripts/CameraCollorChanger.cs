using System.Collections;
using UnityEngine;

public class CameraCollorChanger : MonoBehaviour {

    public Color[] colors;

    private void Start() {
        StartCoroutine(nameof(ColorChanger));
    }

    private IEnumerator ColorChanger() {
        while (true) {
            Camera.main.backgroundColor = colors[Random.Range(0,colors.Length)];
            yield return new WaitForSeconds(10f);
        }
    }
}