using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchTool : MonoBehaviour
{
  public WatchPart part;
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }


  private void onCollision(Collider collider)
  {
    if (collider.TryGetComponent<WatchPart>(out WatchPart part) && part.Equals(part) && !part.snapped)
    {
      part.transform.parent = transform;
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
