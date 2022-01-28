using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetSingleton : MonoBehaviour {
  public static MagnetSingleton instance;

  public Material RedMaterial;
  public Material BlueMaterial;

  private void Start() {
    instance=this;
  }

}