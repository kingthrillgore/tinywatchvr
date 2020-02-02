using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophySpawner : MonoBehaviour
{
    public GameObject trophy;
    public AudioSource source;

    private void Start() {
        StartCoroutine(spawn());
    }

    IEnumerator spawn() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(5f, 20f));

            GameObject.Instantiate(trophy);
            source.Play();
        }
    }
}
