using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class RegisterRespawnArea : MonoBehaviour
{

  private void Awake() {
    GameStateSingleton.instance.RespawnArea=gameObject;
  }
}
