using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TimedEvent {
    public float time;
    public UnityEvent ev;
    public bool triggered = false;
}

public class TimeTrigger : MonoBehaviour
{
    public TimedEvent[] events;
    public float gameTime = 180f;
    float tickTime = 1f;
    float curTime = 0f;

    private void Start() {
        startGame();
    }

    void startGame() {
        StartCoroutine(timeCoroutine());
    }

    IEnumerator timeCoroutine() {
        while (curTime < gameTime) {
            foreach (TimedEvent t in events) {
                if (!t.triggered && t.time < curTime) {
                    t.ev.Invoke();
                    t.triggered = true;
                }
            }

            curTime += tickTime;
            yield return new WaitForSeconds(tickTime);
        }
        yield return null;
    }
}