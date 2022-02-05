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
  public List<GameObject> Powerups = new List<GameObject>();
  public GameObject       SmokeEffect;
  public GameObject       SparkEffect;
  public GameObject       RespawnEffect;
  public int              MaxAmountPowerups = 2;
  public float            PowerupCooldownMin = 5;
  public float            PowerupCooldownRandom = 3;
  public float            InitialPowerupCooldown = 2;
  public int              CurrentAmountPowerups = 0;
  public float            GameDuration = 180f;
  public static string    WinnerText = "No One Wins";

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
    StartCoroutine(handlePowerups());
  }

  private void Update() {
    if(GUI)
      for(int i = 0;i<Players.Count;i++) {
        var ui = Players[i].GUI;
        ui.transform.Find("HitCount").GetComponent<Text>().text="Hits: "+Players[i].Deaths.ToString();
        ui.transform.Find("Score").GetComponent<Text>().text="Score: "+Players[i].Points.ToString();
        ui.transform.Find("PlayerName").GetComponent<Text>().text="Player "+(i+1).ToString()+ "   ";
        Players[i].Character.transform.Find("PlayerName").GetComponent<TextMesh>().text=(i+1).ToString();
      }
    float passed = Time.timeSinceLevelLoad;
    int left = (int)(GameDuration-passed);
    GUI.transform.Find("Time").GetComponent<UnityEngine.UI.Text>().text="Time: "+ (left / 60).ToString("00") + ":" + (left%60).ToString("00");

    if (left <0) {
      declareWinner();
    }
  }

  public int addPlayer(PlayerInfo info) {
    Players.Add(info);
    var players = GUI.transform.Find("Players");
    var newUI = Instantiate(PlayerUI,players);
    info.GUI=newUI;
    newUI.transform.Find("HitCount").GetComponent<UnityEngine.UI.Text>().color=info.clr;
    newUI.transform.Find("Score").GetComponent<UnityEngine.UI.Text>().color=info.clr;
    newUI.transform.Find("PlayerName").GetComponent<UnityEngine.UI.Text>().color=info.clr;
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

  IEnumerator handlePowerups() {
    yield return new WaitForSeconds(InitialPowerupCooldown);
    while(true) {
      yield return new WaitForSeconds(PowerupCooldownMin+PowerupCooldownRandom*Random.value);
      if(CurrentAmountPowerups <= MaxAmountPowerups) {
        var objs = GameObject.FindObjectsOfType<GroundTile>();
        List<GroundTile> available = new List<GroundTile>();
        for(int i = 0;i<objs.Length;i++) {
          if(objs[i].GetComponent<GroundTile>().isReady()) {
            available.Add(objs[i]);
          }
        }
        var obj = available[Random.Range(0,available.Count)];
        obj.open();
      } 
    }
  }

  void declareWinner() {
    uint bestDeaths = uint.MaxValue;
    uint bestPoints = 0;
    PlayerInfo winner = null;
    int index= -1;
    for(int i = 0;i <Players.Count;i++) {
      var p = Players[i];
      uint points = p.Points;
      uint deaths = p.Deaths;
      if (points > bestPoints || (points==bestPoints && bestDeaths > deaths)) {
        bestDeaths=deaths;
        bestPoints=points;
        winner=p;
        index=i;
      }
    }

    if(winner != null) {
      WinnerText="Player "+(index+1).ToString()+" wins!";
    }
    
    UnityEngine.SceneManagement.SceneManager.LoadScene("WinnerScene");
  }
}
