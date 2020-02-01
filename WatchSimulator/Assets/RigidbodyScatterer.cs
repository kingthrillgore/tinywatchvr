using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyScatterer : MonoBehaviour
{
    float forceMultiplier = 150f;
    float radius = 1f;
    public string triggerTag = "";

    public void scatterRigidbodies() {
        Rigidbody[] objects = GameObject.FindObjectsOfType<Rigidbody>();

        foreach (Rigidbody obj in objects) {
            if (triggerTag != "" && triggerTag != obj.tag) {
                continue;
            }

            obj.AddForce(Random.insideUnitSphere * forceMultiplier, ForceMode.Impulse);
        }
    }

    public void scatterAtPoint(Transform explosionPoint) {
        Vector3 explosionPos = explosionPoint.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders) {
            if (triggerTag != "" && triggerTag != hit.tag) {
                continue;
            }

            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(forceMultiplier, explosionPos, radius, 1.0f);
        }
    }
}
