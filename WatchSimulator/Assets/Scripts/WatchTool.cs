﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class WatchTool : MonoBehaviour
{
  public bool works;
  WatchPart previouslyWorkingPart;
  public AudioSource snapSound;
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
    if (collider.TryGetComponent<WatchPart>(out WatchPart part) && transform.GetComponent<VRTK.VRTK_InteractableObject>().IsGrabbed())
    {
      if ((works || part.Equals(previouslyWorkingPart)) && !part.snapped && part.canSnap)
      {
        previouslyWorkingPart = part;
        part.GetComponent<Rigidbody>().useGravity = false;
        part.GetComponent<Rigidbody>().isKinematic = true;
        part.transform.parent = transform;
        snapSound.Play();
        works = false;
      }
      else if (!works)
      {
        // Already used that tool?
        FindObjectOfType<AbeVoice>().wrongTool();
        GetComponent<VRTK_InteractableObject>().ForceStopInteracting();
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(Random.insideUnitSphere * 30f, ForceMode.Impulse);

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
