using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour
{
  public MagneticType MagnetType;

  public void toggle() {
    MagnetType = (MagnetType == MagneticType.Blue)?MagneticType.Red:MagneticType.Blue;
  }
}
