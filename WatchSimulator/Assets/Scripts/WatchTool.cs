using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchTool : MonoBehaviour
{
  public bool works = false;
  WatchPart previouslyWorkingPart;
  // Start is called before the first frame update
  void Start()
  {
    // XXX
    //works = (Random.value > 0.5f);
    works = true;
  }


  private void onCollision(Collider collider)
  {
    if (collider.TryGetComponent<WatchPart>(out WatchPart part))
    {
      if ((works || part.Equals(previouslyWorkingPart)) && !part.snapped)
      {
        previouslyWorkingPart = part;
        part.GetComponent<Rigidbody>().useGravity = false;
        part.GetComponent<Rigidbody>().isKinematic = true;
        part.transform.parent = transform;
        works = false;
      }
      else if (!works)
      {
        Debug.Log("TODO: sound effect or text to say the tool is wrong.");
      }
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
