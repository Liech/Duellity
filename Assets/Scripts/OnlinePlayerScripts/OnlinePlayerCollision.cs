using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
using Photon.Pun;

public class OnlinePlayerCollision : MonoBehaviourPunCallbacks
{
    public float whiteDuration = 0.1f;
    PhotonView PV;
    void Start()
    {
        PV = GetComponent<PhotonView>();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var obj = collision.collider.gameObject;
        if (obj.GetComponent<IsDangerous>())
        {
            Debug.Log("collided");
            PV.RPC("RPC_cooldown", RpcTarget.All);
            //StartCoroutine(cooldown());
            //GameStateSingleton.instance.Players[GetComponent<PlayerSpawner>().playerNumber].Deaths++;
            //Room oder PlayerManager
            // if (SoundSingleton.instance.PlayerHit)
            //SoundSingleton.instance.playPlayerHit();
            //if (GetComponent<Respawner>())
            //GetComponent<Respawner>().respawn();

            //SOUND ABSPIELEN +1 TOT UND RESPAWN
            PV.RPC("RPC_RespawnPlayer", RpcTarget.All);

        }
    }
    [PunRPC]
    IEnumerator RPC_cooldown()
    {
        Debug.Log("cd triggered");
        Material oldMaterial;
        MeshRenderer renderer = null;
        if (GetComponent<MeshRenderer>())
            renderer = GetComponent<MeshRenderer>();
        else if (transform.childCount > 0 && transform.GetChild(0).GetComponent<MeshRenderer>())
            renderer = transform.GetChild(0).GetComponent<MeshRenderer>();

        if (renderer && renderer.sharedMaterial.name != MagnetSingleton.instance.GotHitMaterial.name)
        {
            oldMaterial = renderer.sharedMaterial;
            renderer.sharedMaterial = GameManagr.Instance.GotHitMaterial;
            yield return new WaitForSeconds(whiteDuration);
            if (MagnetSingleton.instance.GotHitMaterial.name == renderer.sharedMaterial.name)
                renderer.material = oldMaterial;
        }
    }
    [PunRPC]
    void RPC_RespawnPlayer()
    {
        //respawn
    }
}
