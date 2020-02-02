using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbeVoice : MonoBehaviour
{
  public List<AudioSource> wrongPartSounds;
  public List<AudioSource> wrongToolSounds;
  bool partCooldown = false;
  bool toolCooldown = false;
  float delay = 4f;
  // Start is called before the first frame update

  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
  }

  IEnumerator delayPart()
  {
    yield return new WaitForSeconds(delay);
    partCooldown = false;
  }

  IEnumerator delayTool()
  {
    yield return new WaitForSeconds(delay);
    toolCooldown = false;
  }

  public void wrongTool()
  {
    if (!toolCooldown)
      wrongToolSounds[Random.Range(0, wrongToolSounds.Count)].Play();
    toolCooldown = true;
    StartCoroutine(delayTool());
  }

  public void wrongPart()
  {
    if (!partCooldown)
      wrongPartSounds[Random.Range(0, wrongPartSounds.Count)].Play();
    partCooldown = true;
    StartCoroutine(delayPart());
  }
}
