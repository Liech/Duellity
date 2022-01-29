using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MomentumFreeze : MonoBehaviour {
  public float freezeDuration = 0.1f;
  public float freezeThreshold = 5;

  bool frozen = false;
  Vector2 lastVelocity;
  Vector2 newVelocity;
  Rigidbody2D body;

  void Start() {
    body=GetComponent<Rigidbody2D>();
    lastVelocity=body.velocity;
  }
  
  // Update is called once per frame
  void FixedUpdate() {
    
    if((lastVelocity-body.velocity).magnitude > freezeThreshold) {
      if(!frozen) {
        newVelocity=body.velocity;
        StartCoroutine(freeze());
      }
      else {
        StopAllCoroutines();
        newVelocity=body.velocity;
        StartCoroutine(freeze());
      }
    }
    lastVelocity=body.velocity;

  }
  
  IEnumerator freeze() {
    frozen=true;
    body.velocity=new Vector2();
    lastVelocity=body.velocity;
    body.constraints=RigidbodyConstraints2D.FreezeAll;

    yield return new WaitForSeconds(freezeDuration);

    body.constraints=RigidbodyConstraints2D.None;
    frozen=false;
    body.velocity=newVelocity;
    lastVelocity=body.velocity;
  }

  private void OnDrawGizmos() {
    if(frozen)
      Gizmos.DrawCube(transform.position, new Vector3(1, 2, 1));
  }
}