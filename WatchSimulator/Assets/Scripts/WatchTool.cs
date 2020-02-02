using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class WatchTool : MonoBehaviour
{
  public bool works;
  WatchPart previouslyWorkingPart;
  // Start is called before the first frame update
  void Start()
  {
    // XXX
    //works = (Random.value > 0.5f);
    works = true;
  }

  private void OnEnable()
  {
    GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed += handleUngrab;
  }

  private void OnDisable()
  {
    GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed -= handleUngrab;
  }


  void handleUngrab(object sender, InteractableObjectEventArgs e)
  {
    var part = GetComponentInChildren<WatchPart>();
    if (part)
    {
      part.transform.parent = null;
      part.GetComponent<Rigidbody>().useGravity = true;
      part.GetComponent<Rigidbody>().isKinematic = false;
      part.GetComponent<Rigidbody>().AddForce(Random.insideUnitSphere * 30f, ForceMode.Impulse);
    }
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
