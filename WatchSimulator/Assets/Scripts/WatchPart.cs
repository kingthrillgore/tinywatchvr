using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchPart : MonoBehaviour
{
  public bool valid;
  public bool snapped = false;
  // Start is called before the first frame update
  void Start()
  {
    valid = (Random.value > 0.9f);
  }

  void Update()
  {

  }
}
