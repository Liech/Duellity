using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script to steer momentum, reflection,... of ball

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Ball : MonoBehaviour {

  private Rigidbody2D body;
  public Vector2 inV;
  public Vector2 outV;

  private void Start() {
    body=GetComponent<Rigidbody2D>();
  }


}