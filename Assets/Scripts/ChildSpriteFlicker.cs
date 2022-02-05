using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildSpriteFlicker : MonoBehaviour {
  // Update is called once per frame
  void Update() {
    for(int i = 0;i<transform.childCount;i++) {
      var c = transform.GetChild(i);
      if(c.GetComponent<SpriteRenderer>()) {
        c.GetComponent<SpriteRenderer>().color=Random.ColorHSV();
      }
    }
  }
}
