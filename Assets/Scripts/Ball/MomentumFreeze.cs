using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpeedControl))]
[RequireComponent(typeof(Rigidbody2D))]
public class MomentumFreeze : MonoBehaviour {
  public float minFreezeDuration = 0.1f;
  public float maxFreezeDuration = 2;

  public float freezeDuration = 0.1f;
  public float freezeThreshold = 5;
  public AnimationCurve freezeDurationFactor;

  private float maxSpeed = float.MaxValue;
  bool frozen = false;
  Vector2 lastVelocity;
  Vector2 newVelocity;
  Rigidbody2D body;
  private float beefigkeit = 0;

  void Start() {
    body=GetComponent<Rigidbody2D>();
    lastVelocity=body.velocity;
    if(GetComponent<SpeedControl>())
      maxSpeed = GetComponent<SpeedControl>().MaxSpeed;
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

    float perc = ((newVelocity.magnitude-freezeThreshold) / (maxSpeed-freezeThreshold));
    beefigkeit=perc;
    float dynamicFreeze = freezeDurationFactor.Evaluate(perc)*maxFreezeDuration;
    yield return new WaitForSeconds(minFreezeDuration + dynamicFreeze);

    body.constraints=RigidbodyConstraints2D.None;
    frozen=false;
    body.velocity=newVelocity;
    lastVelocity=body.velocity;
  }

  private void OnDrawGizmos() {
    if(frozen)
      Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1) * beefigkeit);
  }
}