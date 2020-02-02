using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class WatchTool : MonoBehaviour
{
  public bool works;
  public AudioSource snapSound;
  // Start is called before the first frame update
  void Start()
  {
    works = (Random.value > 0.2f);
  }

  private void OnEnable()
  {
    GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed += handleUngrab;
    GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += handleGrab;
  }

  private void OnDisable()
  {
    GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed -= handleUngrab;
    GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed -= handleGrab;
  }

  void handleGrab(object sender, InteractableObjectEventArgs e)
  {
    if (!works)
    {
      FindObjectOfType<AbeVoice>().wrongTool();
      GetComponent<VRTK_InteractableObject>().ForceStopInteracting();
      GetComponent<Rigidbody>().useGravity = true;
      GetComponent<Rigidbody>().isKinematic = false;
    }
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
  }

  private void onCollision(Collider collider)
  {
    if (collider.TryGetComponent<WatchPart>(out WatchPart part) && transform.GetComponent<VRTK.VRTK_InteractableObject>().IsGrabbed() && works && !part.snapped && part.canSnap)
    {
      part.GetComponent<Rigidbody>().useGravity = false;
      part.GetComponent<Rigidbody>().isKinematic = true;
      part.transform.parent = transform;
      snapSound.Play();
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
