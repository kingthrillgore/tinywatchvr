using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyScatterer : MonoBehaviour
{
    float forceMultiplier = 5f;

    public void scatterRigidbodies(string tag = "") {
        Rigidbody[] objects = GameObject.FindObjectsOfType<Rigidbody>();

        foreach (Rigidbody obj in objects) {
            if (tag != "" && tag != obj.tag) {
                continue;
            }

            obj.AddForce(Random.insideUnitSphere * forceMultiplier, ForceMode.Impulse);
        }
    }
}
