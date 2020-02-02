using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbeVoice : MonoBehaviour
{
  public List<AudioSource> wrongPartSounds;
  public List<AudioSource> wrongToolSounds;
  // Start is called before the first frame update

  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
  }

  public void wrongTool()
  {
    var audio = wrongToolSounds[Random.Range(0, wrongToolSounds.Count)];
    audio.Play();
  }

  public void wrongPart()
  {
    var audio = wrongPartSounds[Random.Range(0, wrongPartSounds.Count)];
    audio.Play();
  }
}
