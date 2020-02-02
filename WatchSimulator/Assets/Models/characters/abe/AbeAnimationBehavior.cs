using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbeAnimationBehavior : MonoBehaviour
{
    public Animator animator;
    public float idleActionsPerMinuteMinimum = 1f;
    public float idleActionsPerMinuteMaximum = 6f;

    private float nextIdleActionTime = 0f;

    public void Start()
    {
        Randomize();
        PickNextIdleActionTime();
    }

    public void Update()
    {
        if (
            Time.time > nextIdleActionTime
            && animator.GetCurrentAnimatorStateInfo(0).IsName("old man idle")
        )
        {
            PickNextIdleActionTime();

            int rand = Random.Range(0, 3);
            if (rand == 0) {
                Debug.Log("Idle");
                IdleAction();
            } else if (rand == 1) {
                Debug.Log("Work");
                Work();
            } else {
                Debug.Log("Happy");
                ReactHappy();
            }
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
