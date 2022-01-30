using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MagnetImpulse))]
public class ImpulseReloadDisplay : MonoBehaviour
{
  public List<Sprite> sprites;
  void Update() {
    var renderer = transform.Find("PlayerColorIndication").GetComponent<SpriteRenderer>();
    var imp = GetComponent<MagnetImpulse>();
    float perc = (Time.time-imp.TimeSinceImpulse)/imp.cooldownDuration;
    int index = (int)((sprites.Count)*perc);
    if(index<0)
      index=0;
    if(index>=sprites.Count-1)
      index=sprites.Count-1;
    renderer.sprite=sprites[index];
  }
}