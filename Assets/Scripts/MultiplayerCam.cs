using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MultiplayerCam : MonoBehaviour
{

    PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(transform.parent.GetComponentInChildren<Camera>().gameObject);
            //Destroy(rb);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
