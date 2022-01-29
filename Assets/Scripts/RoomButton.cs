using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;

public class RoomButton : MonoBehaviour
{
    GameObject menuManager;
    public TMP_Text text;
    public RoomInfo Rinfo;

    public void SetUp(RoomInfo info)
    {
        Rinfo = info;
        text.text = info.Name;
        menuManager = GameObject.Find("MainMenuManager");
    }

    public void OnClick()
    {
        MainMenu.Instance.JoinRoom(Rinfo);
        MainMenu.Instance.GameLobby();
    }
}
