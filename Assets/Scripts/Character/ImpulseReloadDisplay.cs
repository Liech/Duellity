using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Dash))]
public class ImpulseReloadDisplay : MonoBehaviour
{
  public List<Sprite> sprites;
  void Update() {
    var renderer = transform.Find("PlayerColorIndication").GetComponent<SpriteRenderer>();
    var imp = GetComponent<Dash>();
    float perc = (Time.time-imp.TimeSinceImpulse)/imp.Cooldown;
    int index = (int)((sprites.Count)*perc);
    if(index<0)
      index=0;
    if(index>=sprites.Count-1)
      index=sprites.Count-1;
    renderer.sprite=sprites[index];
  }
}