using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyScatterer : MonoBehaviour
{
  float forceMultiplier = 300f;
  float radius = 2f;
  public string triggerTag = "";

  public void scatterRigidbodies()
  {
    Rigidbody[] objects = GameObject.FindObjectsOfType<Rigidbody>();

    foreach (Rigidbody obj in objects) {
        if (triggerTag != "" && triggerTag != obj.tag) {
            continue;
        }

        obj.AddForce(Random.insideUnitSphere * forceMultiplier, ForceMode.Impulse);
    }
  }

  public void scatterAtPoint(Transform explosionPoint)
  {
    Vector3 explosionPos = explosionPoint.position;
    Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
    foreach (Collider hit in colliders)
    {
      if (triggerTag != "" && triggerTag != hit.tag)
      {
        continue;
      }

      Rigidbody rb = hit.GetComponent<Rigidbody>();
    // Detatch watch parts from 
    if (hit.TryGetComponent<WatchPart>(out WatchPart part)) {
        Debug.Log("Scattering part " + part.name);

        part.GetComponent<Rigidbody>().useGravity = true;
        part.GetComponent<Rigidbody>().isKinematic = false;
        part.transform.parent = null;
        part.unsnap();
        GameObject.FindObjectOfType<GameStateController>().unscorePart(part);
    }

    if (rb != null) {
        rb.AddExplosionForce(forceMultiplier, explosionPos, radius, 1.0f);

    }
    }
  }
}
