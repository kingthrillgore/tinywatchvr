using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
  public List<WatchPart> parts = new List<WatchPart>();

  public void scorePart(WatchPart part)
  {
    parts.Add(part);
    if (parts.Count >= 4)
    {
      // TODO: send timer callback to play audio and show a success screen?
      Debug.Log("You have won this video game.");
    }
  }
}
