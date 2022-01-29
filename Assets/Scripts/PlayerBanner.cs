using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerBanner : MonoBehaviourPunCallbacks
{
    public TMP_Text text;
    //public TMP_Text isHost;
    //public GameObject kickButton;
    Player player;
    public void SetUp(Player _player)
    {
        player = _player;
        text.text = _player.NickName;
        //if (PhotonNetwork.IsMasterClient)
        //{
        //    kickButton.gameObject.SetActive(true);
        //}
        //else
        //{
        //    kickButton.gameObject.SetActive(false);
        //}
        //if (player.IsMasterClient)
        //{
        //    isHost.gameObject.SetActive(true);
        //}
        //else
        //{
        //    isHost.gameObject.SetActive(false);
        //}

    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }

    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }

    public void Kick()
    {
        PhotonNetwork.CloseConnection(player);
    }

}
