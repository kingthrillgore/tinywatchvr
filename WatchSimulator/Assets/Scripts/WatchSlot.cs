using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class WatchSlot : MonoBehaviour
{
  public GameObject snapSpot;
  // Start is called before the first frame update
  void Start()
  {
  }

  private void onCollision(Collider collider)
  {
    if (collider.TryGetComponent<WatchPart>(out WatchPart part) && part.valid)
    {
      part.GetComponent<VRTK_InteractableObject>().ForceStopInteracting();
      part.gameObject.transform.position = snapSpot.transform.position;
      part.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
      // TODO: send a message up to the scoring component to say we have a good match.
      // scoreObject.scoreSlot(gameObject)
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
