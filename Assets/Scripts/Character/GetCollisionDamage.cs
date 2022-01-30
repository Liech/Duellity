using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerSpawner))]
public class GetCollisionDamage : MonoBehaviour
{
  public int player = 0;

  private void OnCollisionEnter2D(Collision2D collision) {
    var obj = collision.collider.gameObject;
    if(obj.GetComponent<IsDangerous>()) {
      GameStateSingleton.instance.Players[GetComponent<PlayerSpawner>().playerNumber].Deaths++;
      if(SoundSingleton.instance.PlayerHit)
        SoundSingleton.instance.playWahhh();
      if(GameStateSingleton.instance.SmokeEffect)
        Instantiate(GameStateSingleton.instance.SmokeEffect, transform.position, transform.rotation);
      if(GetComponent<Respawner>())
        GetComponent<Respawner>().respawn();
    }
  }
}
