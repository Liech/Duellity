using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateSingleton : MonoBehaviour {
  public static GameStateSingleton instance;

  public List<PlayerInfo> Players = new List<PlayerInfo>();
  public GameObject       GUI = null;
  public GameObject       RespawnArea;
  public GameObject       PlayerUI;


  private void Awake() {
    instance=this;
  }

  private void Start() {
    instance=this;
    if(GUI) {
      GUI=Instantiate(GUI);
    }
  }

  private void Update() {
    if(GUI)
      for(int i = 0;i<Players.Count;i++) {
        var ui = Players[i].GUI;
        ui.transform.Find("HitCount").GetComponent<Text>().text="Hits: "+Players[i].Deaths.ToString();
        
        var rect = ui.transform.Find("EnergybarBackground").Find("EnergybarCurrent").GetComponent<RectTransform>();
        var background = ui.transform.Find("EnergybarBackground").GetComponent<RectTransform>();
        float value = 0.5f;
        rect.sizeDelta=new Vector2(background.sizeDelta.x * value, background.sizeDelta.y);        
      }
  }

  public int addPlayer(PlayerInfo info) {
    Players.Add(info);
    var players = GUI.transform.Find("Players");
    var newUI = Instantiate(PlayerUI,players);
    info.GUI=newUI;
    newUI.transform.Find("HitCount").GetComponent<UnityEngine.UI.Text>().color=info.clr;
    return Players.Count-1;
  }
}
