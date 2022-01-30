using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketStart : MonoBehaviour {

  bool started = false;
  // Start is called before the first frame update
  void Start() {

  }

  // Update is called once per frame
  void Update() {
    var objs=GameObject.FindObjectsOfType<RocketButton>();
    if (objs.Length ==0 && started == false) {
      Debug.Log("Rocket Start");
      GetComponent<Animator>().SetTrigger("RocketStart");
      started=true;
    }
  }
}