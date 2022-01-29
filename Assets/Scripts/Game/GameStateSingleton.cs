using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateSingleton : MonoBehaviour
{
  public static GameStateSingleton instance;

  public List<PlayerInfo> Players = new List<PlayerInfo>();
  public GameObject       GUI = null;


  private void Start() {
    instance=this;
    Players.Add(new PlayerInfo());
    if (GUI)
      GUI = Instantiate(GUI);
  }

  private void Update() {
    if(GUI)
      GUI.transform.Find("HitCount").GetComponent<Text>().text = "Hits: " + Players[0].Deaths.ToString();
  }
}
