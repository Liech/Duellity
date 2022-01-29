using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSingleton : MonoBehaviour
{
  public static SoundSingleton instance;

  public GameObject Collide;

  public void playCollide()
  {
    if (Collide) Instantiate(Collide,transform);
  }

  public SoundSingleton()
  {
    instance = this;
  }
}
