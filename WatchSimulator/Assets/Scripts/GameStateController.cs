using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
  public List<WatchPart> parts = new List<WatchPart>();
  public AudioSource dingSound;
  bool canScore = true;
  public Transform winText;
    public GameObject trophy;
    public AudioSource outroSource;
    public AudioSource cheerSource;


    public void scorePart(WatchPart part)
  {
    if (!canScore)
      return;

    if (!parts.Contains(part))
    {
      Debug.Log("adding " + part.name);
      parts.Add(part);
      dingSound.Play();

      StartCoroutine(delay());
    }
    if (parts.Count >= 4)
    {
      // TODO: send timer callback to play audio and show a success screen?
      Debug.Log("You have won this video game.");
      winGame();
    }
  }

  IEnumerator delay()
  {
    canScore = false;

    yield return new WaitForSeconds(2f);

    canScore = true;
  }

  public void unscorePart(WatchPart part)
  {
    StartCoroutine(delay());

    Debug.Log("Removing part " + part.name + " " + parts.Contains(part));
    parts.Remove(part);
  }

  public void winGame()
  {
    // Audio
    outroSource.Play();
    cheerSource.Play();

    // Spinning text
    winText.gameObject.SetActive(true);
    StartCoroutine(spin());

    // Trophy
    trophy.SetActive(true);
    StartCoroutine(delayedSmash());

    // Reload scene
    StartCoroutine(reload());
  }

    IEnumerator delayedSmash() {
        yield return new WaitForSeconds(0.3f);
        GetComponent<RigidbodyScatterer>().scatterRigidbodies();

        yield return null;
    }

  IEnumerator spin()
  {
    while (true)
    {
      winText.Rotate(winText.up, 1f);
      yield return new WaitForSeconds(0.01f);
    }
  }

  IEnumerator reload()
  {
    yield return new WaitForSeconds(10f);

    Application.LoadLevel(Application.loadedLevel);

    yield return null;
  }
}
