using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon;

public class OnlineKugelMaterial : MonoBehaviourPunCallbacks
{
    MagneticType oldType;
    PhotonView PV;
    private void Start()
    {
        PV = GetComponent<PhotonView>();
        PV.RPC("RPC_RPCRNGMaterial", RpcTarget.All);
        oldType = MagneticType.Undefined;
    }

    private void Update()
    {
        PV.RPC("RPC_UpdateMaterial", RpcTarget.All);
    }
    [PunRPC]
    void RPC_RNGMaterial()
    {
         if (Random.value > 0.5f)
            GetComponent<Magnetic>().MagnetType = MagneticType.Blue;
        else
            GetComponent<Magnetic>().MagnetType = MagneticType.Red;

    }
    [PunRPC]
    void RPC_UpdateMaterial()
    {
        MeshRenderer renderer = null;
        if (GetComponent<MeshRenderer>())
            renderer = GetComponent<MeshRenderer>();
        else if (transform.childCount > 0 && transform.GetChild(0).GetComponent<MeshRenderer>())
            renderer = transform.GetChild(0).GetComponent<MeshRenderer>();

        if (renderer && GetComponent<Magnetic>().MagnetType != oldType)
        {
            if (GetComponent<Magnetic>().MagnetType == MagneticType.Blue)
            {
                renderer.material = GameManagr.Instance.BlueMaterial;
                oldType = MagneticType.Blue;
            }
            else
            {
                renderer.material = GameManagr.Instance.RedMaterial;
                oldType = MagneticType.Red;
            }

        }
    }
}
