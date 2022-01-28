using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SpeedControl : MonoBehaviour
{
  public float MaxSpeed = 5f;
  public float MinSpeed = 0.4f;
  
  private Rigidbody2D body;

  private void Start() {
    body=GetComponent<Rigidbody2D>();
  }

  private void Update() {
    if(body.velocity.magnitude<1e-6f) {
      float angle = Random.value * Mathf.PI*2;
      body.velocity=new Vector2(Mathf.Cos(angle),Mathf.Sin(angle)) * MinSpeed;
    }
    else {
      float speed = body.velocity.magnitude;
      if(speed>MaxSpeed)
        body.velocity=body.velocity.normalized*MaxSpeed;
      else if (speed < MinSpeed)
        body.velocity=body.velocity.normalized*MinSpeed;
    }
  }
}
