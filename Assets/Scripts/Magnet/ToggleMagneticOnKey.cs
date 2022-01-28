using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Magnetic))]
public class ToggleMagneticOnKey : MonoBehaviour {
  public KeyCode key = KeyCode.Space;
    // Update is called once per frame
    void Update() {
    if(Input.GetKeyDown(key))
      GetComponent<Magnetic>().toggle();
    }
}
