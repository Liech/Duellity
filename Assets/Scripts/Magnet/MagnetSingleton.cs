using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetSingleton : MonoBehaviour {
  public static MagnetSingleton instance;

  public Material RedMaterial;
  public Material BlueMaterial;
  public Material GotHitMaterial;
  public GameObject ImpulseEffectRed;
  public GameObject ImpulseEffectBlue;

  private void Start() {
    instance=this;
  }
}