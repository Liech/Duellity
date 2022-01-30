using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemCollector : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D collision) {
    var obj = collision.gameObject.GetComponent<GroundTile>();
    if(obj) {
      obj.close();
    }
  }
}
