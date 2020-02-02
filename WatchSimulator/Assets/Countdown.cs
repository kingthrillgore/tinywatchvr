using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
  public TextMesh text;
  int timeLeft = 20;
  // Start is called before the first frame update
  void Start()
  {
    StartCoroutine(tick());
  }

  // Update is called once per frame
  void Update()
  {

  }

  IEnumerator tick()
  {
    while (true)
    {
      yield return new WaitForSeconds(1);
      timeLeft--;
      var absTime = Mathf.Abs(timeLeft);
      var timeString = absTime >= 10 ? absTime.ToString() : $"0{absTime}";
      text.text = timeLeft > 0 ? $"00:{timeString}" : $"-00:{timeString}";
      if (timeLeft < 0)
      {
        text.color = Color.red;
      }
    }
  }
}
