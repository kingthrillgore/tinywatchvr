using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomController : MonoBehaviour
{

    public Transform cameraParent;
    public float multiplier = 2f;
    bool scaled = false;

    void handleCollision(Collider col) {
        Debug.Log("Colliding with " + col.name);

        toggleScale();
    }

    void toggleScale() {
        if (scaled) {
            cameraParent.localScale /= multiplier;
        } else {
            cameraParent.localScale *= multiplier;
        }

        scaled = !scaled;
    }

    private void OnCollisionEnter(Collision collision) {
        handleCollision(collision.collider);
    }

    private void OnTriggerEnter(Collider other) {
        handleCollision(other);
    }
}
