using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateSingleton : MonoBehaviour {
  public static GameStateSingleton instance;

  public List<PlayerInfo> Players = new List<PlayerInfo>();
  public GameObject       GUI = null;
  public GameObject       RespawnArea;


  private void Awake() {
    instance=this;
  }
  private void Start() {
    instance=this;
    Players.Add(new PlayerInfo());
    if(GUI) {
      GUI=Instantiate(GUI);
    }
  }

  private void Update() {
    if(GUI)
      for(int i = 0;i<Players.Count;i++) {
        GUI.transform.GetChild(i).Find("HitCount").GetComponent<Text>().text="Hits: "+Players[0].Deaths.ToString();

        var rect = GUI.transform.GetChild(i).Find("EnergybarBackground").Find("EnergybarCurrent").GetComponent<RectTransform>();
        var background = GUI.transform.GetChild(i).Find("EnergybarBackground").GetComponent<RectTransform>();
        float value = 0.5f;
        rect.sizeDelta=new Vector2(background.sizeDelta.x * value, background.sizeDelta.y);        
      }
  }
}
