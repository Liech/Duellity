using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PowerupInstance))]
[RequireComponent(typeof(CircleCollider2D))]
public class Powerup : MonoBehaviour {
  public GroundTile tile = null;
  public void ApplyPowerup(GameObject g) {
    Debug.Log("CollectApplyPowerup");
    if(GameStateSingleton.instance.SmokeEffect)
      Instantiate(GameStateSingleton.instance.SmokeEffect,transform.position,transform.rotation);
    GetComponent<PowerupInstance>().Apply(g);
    SoundSingleton.instance.playKatsching();
    GameStateSingleton.instance.CurrentAmountPowerups--;
    if(tile)
      tile.close();
    Destroy(gameObject);
  }
}
