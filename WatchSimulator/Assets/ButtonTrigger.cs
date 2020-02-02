using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonTrigger : MonoBehaviour
{
    public UnityEvent events;
    public string triggerTag = "";

    private void onCollision(Collider collider) {
        if (collider.tag == triggerTag || collider.transform.parent.tag == triggerTag) {
            events.Invoke();
        }
    }

    private void OnCollisionEnter(Collision collision) {
        onCollision(collision.collider);
    }

    private void OnTriggerEnter(Collider other) {
        onCollision(other);
    }
}
