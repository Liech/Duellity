using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {
  public int playerNumber;

  // Start is called before the first frame update
  void Start() {
    var info = new PlayerInfo();
    info.clr=Random.ColorHSV();
    info.clr.a=1;
    transform.Find("PlayerColorIndication").GetComponent<SpriteRenderer>().color=info.clr;
    playerNumber = GameStateSingleton.instance.addPlayer(info);
  }

  // Update is called once per frame
  void Update() {

  }
}