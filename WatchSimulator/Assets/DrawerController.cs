using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerController : MonoBehaviour
{
    float distanceToMove = 1f;
    float moveTime = 1f;
    bool open = false;
    bool moving = false;

    public GameObject[] spawnableObjects;
    GameObject spawnedObject;


    void spawnRandom() {
        spawnedObject = GameObject.Instantiate(spawnableObjects[Random.Range(0, spawnableObjects.Length)], this.transform);
        spawnedObject.transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
    }

    void handleCollision(Collider col) {
        //Debug.Log("Colliding with " + col.name);
        if (col.tag != "Hand" && col.transform.parent.tag != "Hand") {
            return;
        }

        if (open) {
            //drawerClose();
        } else {
            drawerOpen();
        }
    }

    void drawerOpen() {
        if (moveDrawer(transform.forward)) {
            open = true;

            spawnRandom();

            StartCoroutine(delayedClose());
        }
    }

    IEnumerator delayedClose() {
        yield return new WaitForSeconds(5f);

        drawerClose();
    }

    void drawerClose() {
        if (moveDrawer(-transform.forward)) {
            open = false;

            //Destroy(spawnedObject);
        }
    }

    bool moveDrawer(Vector3 direction) {
        if (moving) {
            return false;
        }
        StartCoroutine(moveDrawerCoroutine(direction));
        return true;
    }

    IEnumerator moveDrawerCoroutine(Vector3 direction) {
        moving = true;

        Vector3 start = transform.position;
        Vector3 destination = transform.position + (direction * distanceToMove);
        float t = 0f;

        while (t < 1f) {
            t += Time.deltaTime / moveTime;
            transform.position = Vector3.Lerp(start, destination, t);
            yield return new WaitForEndOfFrame();
        }

        moving = false;
        yield return null;
    }

    private void OnCollisionEnter(Collision collision) {
        handleCollision(collision.collider);
    }

    private void OnTriggerEnter(Collider other) {
        handleCollision(other);
    }
}
