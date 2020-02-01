using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomController : MonoBehaviour
{
    public Transform cameraParent;
    public float multiplier = 2f;
    bool scaled = false;
    float cooldown = 3f;
    float lastScale = 0f;

    void handleCollision(Collider col) {
        if (col.tag != "Goggles") {
            return;
        }
        Debug.Log("Colliding with " + col.name);


        toggleScale();
    }

    void toggleScale() {
        if (Time.time - lastScale < cooldown) {
            return;
        }

        if (scaled) {
            cameraParent.localScale /= multiplier;
        } else {
            cameraParent.localScale *= multiplier;
        }

        scaled = !scaled;
        lastScale = Time.time;
    }

    private void OnCollisionEnter(Collision collision) {
        handleCollision(collision.collider);
    }

    private void OnTriggerEnter(Collider other) {
        handleCollision(other);
    }
}
