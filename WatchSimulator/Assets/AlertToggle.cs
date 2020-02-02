using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AlertToggle : MonoBehaviour
{
  public AudioSource source;
  public AudioSource clickSource;
  public GameObject canvas;
  public TextMeshProUGUI randomText;
  float alertDelay = 10f;
  public bool scatterOnAwake = false;
  public RigidbodyScatterer scatter;
  public GameObject blankScreen;
  public Transform explosionPos;
  bool silenced = true;

  string[] alerts = { "SUN STILL EXISTS", "EVERYTHING IS FINE", "NOT FLOODING", "KINDA WARM", "NO WIND", "SMELLS NICE" };

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
    silenced = false;
    StartCoroutine(alertCoroutine());
  }


  IEnumerator alertCoroutine()
  {
    silenced = false;
    while (true)
    {
      source.Stop();
      blankScreen.SetActive(false);
      randomText.text = alerts[Random.Range(0, alerts.Length)];
      canvas.SetActive(true);
      source.Play();
      if (scatter != null)
      {
        if (explosionPos != null)
        {
          scatter.scatterAtPoint(explosionPos);
        }
        else
        {
          scatter.scatterAtPoint(this.transform);
        }
      }

      if (!scatterOnAwake)
      {
        yield return new WaitForSeconds(alertDelay);
      }
      else
      {
        yield break;
      }
    }
  }

  void onCollision(Collider collider)
  {
    if (collider.tag == "Hand" || (collider.transform.parent != null && collider.transform.parent.tag == "Hand" && !silenced))
    {
      silenced = true;
      clickSource.Play();
      StopAllCoroutines();
      source.Stop();
      blankScreen.SetActive(true);
      canvas.SetActive(false);
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
