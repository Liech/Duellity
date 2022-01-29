using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Magnetic))]
public class MaterialSwap : MonoBehaviour
{
  MagneticType oldType;
  private void Start() {
    oldType=MagneticType.Undefined;
  }

  private void Update() {
    MeshRenderer renderer = null;
    if(GetComponent<MeshRenderer>())
      renderer=GetComponent<MeshRenderer>();
    else if(transform.childCount>0&&transform.GetChild(0).GetComponent<MeshRenderer>())
      renderer=transform.GetChild(0).GetComponent<MeshRenderer>();

    if(renderer && GetComponent<Magnetic>().MagnetType != oldType) {
      if(GetComponent<Magnetic>().MagnetType==MagneticType.Blue) {
        renderer.material=MagnetSingleton.instance.BlueMaterial;
        oldType=MagneticType.Blue;
      }
      else { 
        renderer.material=MagnetSingleton.instance.RedMaterial; 
        oldType=MagneticType.Red;
      }

    }
  }
}
