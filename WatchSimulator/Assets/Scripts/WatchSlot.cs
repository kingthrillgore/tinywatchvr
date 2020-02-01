using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchSlot : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
  }

  private void onCollision(Collider collider)
  {
    if (collider.TryGetComponent<WatchPart>(out WatchPart part) && part.valid)
    {
      // TODO: actually do something? snap the part to the slot.
      Debug.Log("WE HAVE COLLIDED WITH A GOOD PART HERE BABY!");
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
