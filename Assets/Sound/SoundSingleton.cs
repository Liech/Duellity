using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSingleton : MonoBehaviour
{
  public static SoundSingleton instance;

  public GameObject Collide;
  public GameObject PlayerHit;
  public GameObject Impulse;
  public GameObject Hui;


  public void playHui() {
    if(PlayerHit) Instantiate(Hui, transform);
  }
  public void playPlayerHit() {
    if(PlayerHit) Instantiate(PlayerHit, transform);
  }
  public void playImpulse() {
    if(Impulse) Instantiate(Impulse, transform);
  }
  public void playCollide()
  {
    if (Collide) Instantiate(Collide,transform);
  }

  public SoundSingleton()
  {
    instance = this;
  }
}
