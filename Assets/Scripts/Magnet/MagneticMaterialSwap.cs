using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Magnetic))]
public class MagneticMaterialSwap : MonoBehaviour
{
  private void Update() {
    if(GetComponent<MeshRenderer>()) {
      if(GetComponent<Magnetic>().MagnetType==MagneticType.Blue)
        GetComponent<MeshRenderer>().material=MagnetSingleton.instance.BlueMaterial;
      else
        GetComponent<MeshRenderer>().material=MagnetSingleton.instance.RedMaterial;
    }
    else if (transform.childCount > 0 && transform.GetChild(0).GetComponent<MeshRenderer>()) {

      if(GetComponent<Magnetic>().MagnetType==MagneticType.Blue)
        transform.GetChild(0).GetComponent<MeshRenderer>().material=MagnetSingleton.instance.BlueMaterial;
      else
        transform.GetChild(0).GetComponent<MeshRenderer>().material=MagnetSingleton.instance.RedMaterial;
    }
  }
}
