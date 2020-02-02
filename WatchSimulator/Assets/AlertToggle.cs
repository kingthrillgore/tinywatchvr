using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertToggle : MonoBehaviour
{
  public AudioSource source;
  public GameObject text;
  float alertDelay = 10f;
  public bool scatterOnAwake = false;
  public RigidbodyScatterer scatter;
  public GameObject blankScreen;

  private void OnEnable()
  {
    if (scatterOnAwake)
    {
      StartCoroutine(delayedAlert());
    }
  }

  IEnumerator delayedAlert()
  {
    yield return new WaitForSeconds(2f);

    alert();
  }

  public void alert()
  {
    StartCoroutine(alertCoroutine());
  }

  IEnumerator snooze()
  {
    yield return new WaitForSeconds(90f * Random.value);
    StartCoroutine(alertCoroutine());
  }


  IEnumerator alertCoroutine()
  {
    while (true)
    {
      source.Stop();
      blankScreen.SetActive(false);
      text.SetActive(true);
      source.Play();
      if (scatter != null)
      {
        scatter.scatterAtPoint(this.transform);
      }

        if (!scatterOnAwake) {
            yield return new WaitForSeconds(alertDelay);
        } else {
            yield break;
        }
    }
  }

  void onCollision(Collider collider)
  {
    if (collider.tag == "Hand" || collider.transform.parent.tag == "Hand")
    {
      StopAllCoroutines();
      source.Stop();
      blankScreen.SetActive(true);
      text.SetActive(false);
      StartCoroutine(snooze());
    }
  }

  private void OnCollisionEnter(Collision collision)
  {
    onCollision(collision.collider);
  }

  private void OnTriggerEnter(Collider other)
  {
    onCollision(other);
  }
}
