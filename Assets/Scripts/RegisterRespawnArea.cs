using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class RegisterRespawnArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      GameStateSingleton.instance.RespawnArea=gameObject;
      Debug.Log(GameStateSingleton.instance.RespawnArea);
    }
}
