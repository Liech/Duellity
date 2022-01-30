using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemCollector : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D collision) {
    var obj = collision.gameObject.GetComponent<Powerup>();
    if(obj) {
      Debug.Log("Collect");
      obj.ApplyPowerup(gameObject);
    }
  }
}
