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
  public GameObject       Ball;
  public List<Mesh>       Models;
  public int              AmountBalls = 8;
 

  private void Awake() {
    instance=this;
  }

  private void Start() {
    instance=this;
    if(GUI) {
      GUI=Instantiate(GUI);
    }
    for(int i = 0;i <AmountBalls+1;i++) {
      Vector2 point = getRandomLocation();
      var ball = Instantiate(Ball);
      ball.transform.position=new Vector3(point.x,point.y, 0);
      ball.GetComponent<Magnetic>().MagnetType = (i<AmountBalls/2)?MagneticType.Blue: MagneticType.Red;
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

  public Vector2 getRandomLocation() {
    var area = GameStateSingleton.instance.RespawnArea;
    if(!area)
      return new Vector2(0,0);
    var poly = area.GetComponent<PolygonCollider2D>();

    for(int tries = 100;tries>0;tries--) {
      float x = poly.bounds.min.x + Random.value * poly.bounds.size.x;
      float y = poly.bounds.min.y + Random.value * poly.bounds.size.y;
      Vector2 point = new Vector2(x,y);
      if(poly.OverlapPoint(point)) {
        return point;
      }
    }
    return new Vector2(0, 0);
  }
}
