using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerBehavior))]
[RequireComponent(typeof(Rigidbody2D))]
public class Dash : MonoBehaviour
{
  public float Cooldown = 1f;
  public float Strength = 100f;
  public float Duration = 0.3f;

  bool inDash = false;
  Rigidbody2D body;
  private void Start() {
    body=GetComponent<Rigidbody2D>();
  }
  public void ActivateDash(UnityEngine.InputSystem.InputAction.CallbackContext ctx) {
    if(body.velocity.magnitude<1e-3)
      return;
    if (!inDash)
      StartCoroutine(dash());
  }

  IEnumerator dash() {
    SoundSingleton.instance.playHui();
    inDash=true;
    GetComponent<PlayerBehavior>().Stun=true;
    body.velocity=body.velocity.normalized*Strength;
    yield return new WaitForSeconds(Duration);
    GetComponent<PlayerBehavior>().Stun=false;
    yield return new WaitForSeconds(Cooldown);
    inDash=false;
  }
}
