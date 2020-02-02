using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class WatchSlot : MonoBehaviour
{
  public GameObject snapSpot;
  GameStateController gameStateController;
  public TimedEvent timedEvent;

  // Start is called before the first frame update
  void Start()
  {
    gameStateController = GameObject.FindObjectOfType<GameStateController>();
  }

    private void onCollision(Collider collider)
  {
    if (collider.TryGetComponent<WatchPart>(out WatchPart part))
    {
      if (!part.valid)
      {
        Debug.Log("TODO: sound effect or text that the part is wrong.");

        Transform tool = part.transform.parent;
        part.transform.parent = null;
        Destroy(tool.gameObject);

        part.GetComponent<Rigidbody> ().AddForce(Random.insideUnitSphere * 10f, ForceMode.Impulse);

    } else
      {
        if (part.snapped || !part.canSnap) {
            return;
        }

        Debug.Log("VALID!");
        part.GetComponent<Rigidbody>().useGravity = false;
        part.GetComponent<Rigidbody>().isKinematic = true;
        part.gameObject.transform.position = snapSpot.transform.position;
        part.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        gameStateController.scorePart(part);
        part.snap();
        part.transform.parent = transform;
        // TODO: play a sound effect, show the score?
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
