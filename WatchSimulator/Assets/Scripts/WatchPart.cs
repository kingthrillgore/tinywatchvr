using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchPart : MonoBehaviour
{
  public bool valid;
  public bool snapped = false;
  public bool canSnap = true;

  // Start is called before the first frame update
  void Start()
  {
    // XXX
    //valid = (Random.value > 0.9f);
    valid = true;
  }

    IEnumerator delay() {
        canSnap = false;

        yield return new WaitForSeconds(2f);

        canSnap = true;
    }


    public void snap() {
        if (!canSnap) {
            return;
        }

        StartCoroutine(delay());
        snapped = true;
    }

    public void unsnap() {
        StartCoroutine(delay());
        snapped = false;
    }
}
