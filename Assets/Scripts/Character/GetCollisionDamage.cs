using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCollisionDamage : MonoBehaviour
{
  public int player = 0;

  private void OnCollisionEnter2D(Collision2D collision) {
    var obj = collision.collider.gameObject;
    if(obj.GetComponent<IsDangerous>()) {
      GameStateSingleton.instance.Players[player].Deaths++;
      if(SoundSingleton.instance.PlayerHit)
        SoundSingleton.instance.playPlayerHit();
      if(GetComponent<Respawner>())
        GetComponent<Respawner>().respawn();
    }
  }
}
