using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Magnetic))]
public class ToggleMagneticOnKey : MonoBehaviour {
  public KeyCode key = KeyCode.Space;
  Magnetic magnet;
  private void Start() {
    magnet=GetComponent<Magnetic>();
  }
  void Update() {
    if(Input.GetKeyDown(key))
      magnet.toggle();
    }
}
