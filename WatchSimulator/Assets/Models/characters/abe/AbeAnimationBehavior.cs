using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbeAnimationBehavior : MonoBehaviour
{
    public Animator animator;
    public float idleActionsPerMinuteMinimum = 4f;
    public float idleActionsPerMinuteMaximum = 9f;

    const float recenterThreshold = 0.95f;
    const float repositionThreshhold = 0.2f;

    private float nextIdleActionTime = 0f;

    public void Start()
    {
        Randomize();
        PickNextIdleActionTime();
    }

    public void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("old man idle")) {
            if (Time.time > nextIdleActionTime)
            {
                PickNextIdleActionTime();

                int rand = Random.Range(0, 3);
                if (rand == 0) {
                    IdleAction();
                } else if (rand == 1) {
                    Work();
                } else {
                    ReactHappy();
                }
            }
            if (Vector3.Dot(animator.transform.forward, transform.forward) < recenterThreshold)
                animator.transform.rotation = Quaternion.Lerp(animator.transform.rotation, transform.rotation, 0.1f);
            if (Vector3.SqrMagnitude(animator.transform.position - transform.position) > Mathf.Pow(repositionThreshhold, 2f))
                animator.transform.position = Vector3.Lerp(animator.transform.position, transform.position, 0.1f);
        }
    }

    public void Randomize()
    {
        animator.SetFloat("Seed", Random.Range(0f, 1f));
    }

    private void PickNextIdleActionTime()
    {
        nextIdleActionTime = Time.time + 60f / Random.Range(idleActionsPerMinuteMinimum, idleActionsPerMinuteMaximum);
    }

    public void IdleAction()
    {
        Randomize();
        animator.SetTrigger("IdleAction");
    }

    public void Work()
    {
        Randomize();
        animator.SetTrigger("Work");
    }

    public void ReactHappy()
    {
        Randomize();
        animator.SetTrigger("ReactHappy");
    }
}
