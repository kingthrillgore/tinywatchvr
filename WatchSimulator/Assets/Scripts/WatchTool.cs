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
      if (transform.GetComponent<VRTK.VRTK_InteractableObject> ().IsGrabbed() && (works || part.Equals(previouslyWorkingPart)) && !part.snapped && part.canSnap)
      {
        previouslyWorkingPart = part;
        part.GetComponent<Rigidbody>().useGravity = false;
        part.GetComponent<Rigidbody>().isKinematic = true;
        part.transform.parent = transform;
        works = false;
      }
      else if (!works)
      {
        FindObjectOfType<AbeVoice>().wrongTool();
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
