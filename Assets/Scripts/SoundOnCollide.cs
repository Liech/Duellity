using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnCollide : MonoBehaviour
{
  private void OnCollisionEnter2D(Collision2D collision) {
    SoundSingleton.instance.playCollide();
  }
}
