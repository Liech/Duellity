using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
  private void Start() {
    respawn();
  }

  public void respawn() {
    respawnHere(GameStateSingleton.instance.getRandomLocation());
  }
  private void respawnHere(Vector2 v) {
    transform.position=v;
    if(GameStateSingleton.instance.RespawnEffect) {
      var obj = Instantiate(GameStateSingleton.instance.RespawnEffect);
      obj.transform.position=v;
    }
    if(GetComponent<GetCollisionDamage>()) {
      GetComponent<GetCollisionDamage>().respawn();
    }
  }
}
