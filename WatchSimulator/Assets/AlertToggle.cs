using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertToggle : MonoBehaviour
{
    public AudioSource source;
    public GameObject text;
    float toggleTime = 3f;
    public bool scatterOnAwake = false;
    public RigidbodyScatterer scatter;

    private void OnEnable() {
        if (scatterOnAwake) {
            StartCoroutine(delayedAlert());
        }
    }

    IEnumerator delayedAlert() {
        yield return new WaitForSeconds(2f);

        alert();
    }

    public void alert() {
        StartCoroutine(alertCoroutine());
    }

    IEnumerator alertCoroutine() {
        source.Play();
        text.SetActive(true);
        if (scatter != null) {
            scatter.scatterAtPoint(this.transform);
        }

        yield return new WaitForSeconds(toggleTime);

        text.SetActive(false);
    }
}
