using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUp : PowerupInstance {
  public override void Apply(GameObject g) {
    var player = GameStateSingleton.instance.Players[g.GetComponent<PlayerSpawner>().playerNumber];
    player.Points++;
  }
}
