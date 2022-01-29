using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleEffectSuicider : MonoBehaviour {
  ParticleSystem system;
  private void Start() {
    system=GetComponent<ParticleSystem>();
  }
  // Update is called once per frame
  void Update() {
    if(!system.IsAlive())
      Destroy(gameObject);
  }
}
