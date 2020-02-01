using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchTool : MonoBehaviour
{
  public bool works = false;
  WatchPart previouslyWorkingPart;
  // Start is called before the first frame update
  void Start()
  {
    works = (Random.value > 0.5f);
  }

  // Update is called once per frame
  void Update()
  {

  }


  private void onCollision(Collider collider)
  {
    //
    if (collider.TryGetComponent<WatchPart>(out WatchPart part) && (this.works || part.Equals(previouslyWorkingPart)))
    {
      previouslyWorkingPart = part;
      part.transform.SetParent(transform);
      works = false;
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
