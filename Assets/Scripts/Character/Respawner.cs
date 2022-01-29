using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
  private void Start() {
    respawn();
  }

  public void respawn() {
    var area = GameStateSingleton.instance.RespawnArea;
    if(!area)
      return;
    var poly = area.GetComponent<PolygonCollider2D>();

    for(int tries = 100;tries>0;tries--) {
      float x = poly.bounds.min.x + Random.value * poly.bounds.size.x;
      float y = poly.bounds.min.y + Random.value * poly.bounds.size.y;
      Vector2 point = new Vector2(x,y);
      if(poly.OverlapPoint(new Vector2(x, y))) {
        respawnHere(point);
        return;
      }
    }
  }
  private void respawnHere(Vector2 v) {
    transform.position=v;
  }
}
