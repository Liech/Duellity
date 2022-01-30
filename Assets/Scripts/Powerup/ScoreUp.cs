using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUp : PowerupInstance {
  public override void Apply(GameObject g) {
    GameStateSingleton.instance.Players[g.GetComponent<PlayerSpawner>().playerNumber].Points++;
    Debug.Log(GameStateSingleton.instance.Players[g.GetComponent<PlayerSpawner>().playerNumber].Points);
  }
}
