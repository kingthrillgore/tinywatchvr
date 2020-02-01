using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertToggle : MonoBehaviour
{
    public AudioSource source;
    public GameObject text;
    float toggleTime = 3f;


    public void alert() {
        StartCoroutine(alertCoroutine());
    }

    IEnumerator alertCoroutine() {
        source.Play();
        text.SetActive(true);

        yield return new WaitForSeconds(toggleTime);

        text.SetActive(false);
    }
}
