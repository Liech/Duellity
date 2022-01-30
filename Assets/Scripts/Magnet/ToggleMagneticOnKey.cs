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

  public void ToggleMagnet(UnityEngine.InputSystem.InputAction.CallbackContext ctx) {
    magnet.toggle();//_playerInputDirection=ctx.ReadValue<Vector2>();
  }
  //void Update() {
  //  if(Input.GetKeyDown(key))
  //    magnet.toggle();
  //}
}
