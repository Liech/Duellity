using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketButton : MonoBehaviour {

  void OnCollisionEnter2D(Collision2D collision) {
    if(!collision.gameObject.GetComponent<IsDangerous>())
      return;
    var pos = Instantiate(GameStateSingleton.instance.SmokeEffect);
    pos.transform.position=transform.position;
    Destroy(gameObject);
  }
}