using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerSpawner))]
public class GetCollisionDamage : MonoBehaviour
{
  public int player = 0;

  public float invincibleTimeAfterRespawn = 1;
  public GameObject invincibleEffect;

  bool isInvincible = false;

  public void respawn() {
    setInvincible(invincibleTimeAfterRespawn);
  }

  public void setInvincible(float time) {
    StopAllCoroutines();
    StartCoroutine(invincibility(time));
  }

  IEnumerator invincibility(float time) {
    if(invincibleEffect)
      invincibleEffect.SetActive(true);
    isInvincible=true;
    yield return new WaitForSeconds(time);
    isInvincible=false;
    if(invincibleEffect)
      invincibleEffect.SetActive(false);
  }

  private void OnCollisionEnter2D(Collision2D collision) {
    if(isInvincible)
      return;
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
