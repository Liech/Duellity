using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PowerupInstance))]
[RequireComponent(typeof(CircleCollider2D))]
public class Powerup : MonoBehaviour {
  public void ApplyPowerup(GameObject g) {
    if(GameStateSingleton.instance.SmokeEffect)
      Instantiate(GameStateSingleton.instance.SmokeEffect,transform.position,transform.rotation);
    GetComponent<PowerupInstance>().Apply(g);
    SoundSingleton.instance.playKatsching();
    Destroy(gameObject);
  }
}
