using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Magnetic))]
[RequireComponent(typeof(MeshRenderer))]
public class MagneticMaterialSwap : MonoBehaviour
{
  private void Update() {
    if(GetComponent<Magnetic>().MagnetType==MagneticType.Blue)
      GetComponent<MeshRenderer>().material=MagnetSingleton.instance.BlueMaterial;
    else
      GetComponent<MeshRenderer>().material=MagnetSingleton.instance.RedMaterial;
  }
}
