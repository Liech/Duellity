using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateSingleton : MonoBehaviour
{
  public List<PlayerInfo> Players;
  public GameObject       GUI;

  private void Start() {
    Players.Add(new PlayerInfo());
    Instantiate(GUI);
  }
}
