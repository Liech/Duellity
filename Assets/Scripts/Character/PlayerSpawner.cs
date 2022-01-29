using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {
  // Start is called before the first frame update
  void Start() {
    PlayerInfo info = new PlayerInfo();
    GameStateSingleton.instance.Players.Add(info);
    info.clr=Random.ColorHSV();
    transform.Find("PlayerColorIndication").GetComponent<SpriteRenderer>().color=info.clr;

  }

  // Update is called once per frame
  void Update() {

  }
}