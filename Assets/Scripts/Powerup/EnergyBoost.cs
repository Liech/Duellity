using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBoost : PowerupInstance
{
  public override void Apply(GameObject g) {
    g.GetComponent<Dash>().Duration*=1.1f;
  }
}
