using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum groundTileStatus {
  Open, Closing, Closed, Opening
}

[RequireComponent(typeof(Rigidbody2D))]
public class GroundTile : MonoBehaviour {

  public float AnimationTime = 1f;
  public float openingRange = 5f;
  public groundTileStatus status = groundTileStatus.Closed;
  float openingAmount = 0;

  float door1Pos;
  float door2Pos;
  float platform;
  bool ready = true;

  public Vector3 spawnOffset = new Vector3(0,0,0);

  private void Start() {
    door1Pos=d1().localPosition.z;
    door2Pos=d2().localPosition.z;
    platform=p().localPosition.y;
    //open();
  }
  public void open() {
    StartCoroutine(moving(true));
  }
  public void close() {
    StartCoroutine(moving(false));
  }

  Transform d1() {
    return transform.GetChild(1).transform;
  }
  Transform d2() {
    return transform.GetChild(2).transform;
  }
  Transform p() {
    return transform.GetChild(3).transform;
  }


  IEnumerator moving(bool open) {
    Debug.Log(open);
    ready=false;
    if (open)
      GameStateSingleton.instance.CurrentAmountPowerups++;
    int amountTicks = 60;
    float timePerTick = AnimationTime/amountTicks;

    for(int i = 0;i<amountTicks;i++) {
      yield return new WaitForSeconds(timePerTick);
      if (open)
        openingAmount+=openingRange/amountTicks;
      else
        openingAmount-=openingRange/amountTicks;
      d1().transform.localPosition=new Vector3(d1().localPosition.x, d1().localPosition.y, door1Pos+openingAmount);
      d2().transform.localPosition=new Vector3(d1().localPosition.x, d1().localPosition.y,door1Pos-openingAmount);
      if (open)
        p().transform.localPosition =new Vector3(p().localPosition.x, platform+0.5f*((float)i / amountTicks), p().localPosition.z);
      else
        p().transform.localPosition=new Vector3(p().localPosition.x, platform-0.5f*((float)i/amountTicks), p().localPosition.z);
    }
    if(GameStateSingleton.instance.Powerups.Count!=0&&open) {
      //var powerup = GameStateSingleton.instance.Powerups[Random.Range(0,GameStateSingleton.instance.Powerups.Count)];
      var powerup = GameStateSingleton.instance.Powerups[0];
      var plate = transform.Find("TileGround1_Symbol").gameObject.GetComponent<MeshRenderer>().bounds.center;
      var p = Instantiate(powerup);
      p.transform.GetChild(0).GetComponent<Powerup>().tile=this;
      p.transform.position = plate+spawnOffset;
    }
    else if(open)
      GameStateSingleton.instance.CurrentAmountPowerups--;
    if(!open)
      ready=true;
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    
  }

  public bool isReady() {
    if(!ready)
      return false;

    return true;
  }
}
