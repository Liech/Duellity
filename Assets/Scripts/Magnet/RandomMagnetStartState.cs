using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Magnetic))]
public class RandomMagnetStartState : MonoBehaviour {
  // Start is called before the first frame update
  void Start() {
    if(Random.value>0.5f)
      GetComponent<Magnetic>().MagnetType=MagneticType.Blue;
    else
      GetComponent<Magnetic>().MagnetType=MagneticType.Red;
  }
}
