using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Magnetic))]
public class MagneticForce : MonoBehaviour {
  public float MaxStrength = 0.05f;
  public float MinStrength = 0.01f;
  public float MaxRange = 5;
  public bool  DrawGizmo = true;
  public AnimationCurve curve;

  void Update() {
    foreach(var obj in GameObject.FindObjectsOfType<AffectedFromMagnetic>()) {
      var diff = obj.transform.position - transform.position;
      if(diff.magnitude>MaxRange)
        continue;

      float magneticDir = (GetComponent<Magnetic>().MagnetType==obj.GetComponent<Magnetic>().MagnetType)?-1:1;
      var time = (diff.magnitude / MaxRange);
      var value = curve.Evaluate(time);

      var strength = (value-1) * MaxStrength + (value) * MinStrength;
      var dir = diff.normalized;
      obj.GetComponent<Rigidbody2D>().AddForce(dir*strength*magneticDir);
    }
  }

  private void OnDrawGizmos() {
    if(DrawGizmo) {
      Gizmos.DrawWireSphere(transform.position, MaxRange);
    }
  }

}
