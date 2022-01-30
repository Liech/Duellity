using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{

    PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (PV.IsMine)
        {
            InstantiatePlayerController();
        }
    }

    void InstantiatePlayerController()
    {
        PhotonNetwork.Instantiate(Path.Combine("Photon", "OnlinePlayer"), Vector3.zero, Quaternion.identity);
    }
}
