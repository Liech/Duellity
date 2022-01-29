using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;
using Photon.Pun;
using Photon.Realtime;

public class MainMenu : MonoBehaviourPunCallbacks
{
    public static MainMenu Instance;
    public Slider[] volumeSliders;
    public AudioMixer masterMixer;
    public GameObject[] menus;
    public TMP_InputField playerNameInput;
    public TMP_InputField roomNameInput;
    public TMP_Text roomName;
    public Transform RoomList;
    public Transform playerList;
    public GameObject playerBannerPrefab;
    public GameObject roomJoinButton;
    public GameObject startButton;
    public TMP_Text placeHolderName;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.EnableCloseConnection = true;
        PhotonNetwork.GameVersion = "1.0.0";
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void mainMenu()
    {
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].gameObject.SetActive(false);
        }
        menus[0].SetActive(true);
    }
    public void openOptions()
    {
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].gameObject.SetActive(false);
        }
        menus[1].SetActive(true);
    }
    public void namingScreen()
    {
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].gameObject.SetActive(false);
        }
        menus[2].SetActive(true);
    }
    public void FindRoom()
    {
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].gameObject.SetActive(false);
        }
        menus[3].SetActive(true);
    }
    public void CreateRoomMenu()
    {
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].gameObject.SetActive(false);
        }
        menus[4].SetActive(true);
    }
    public void GameLobby()
    {
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].gameObject.SetActive(false);
        }
        menus[5].SetActive(true);
    }
    public void HostRoom()
    {
        if (string.IsNullOrEmpty(roomNameInput.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInput.text);
    }
    public override void OnCreatedRoom()
    {
        if (placeHolderName.text == "")
        {
            placeHolderName.text = "Player" + Random.Range(0, 9000).ToString("0000");
            playerNameInput.text = placeHolderName.text;
            PhotonNetwork.NickName = placeHolderName.text;
        }
        else
        {
            PhotonNetwork.NickName = playerNameInput.text;
        }
        roomName.text = roomNameInput.text;
        Debug.Log("Room Created" + roomNameInput.text);
        Debug.Log(PhotonNetwork.CurrentRoom);
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].gameObject.SetActive(false);
        }
        menus[5].SetActive(true);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.CountOfRooms);
        //Room menu setactive
        Player[] players = PhotonNetwork.PlayerList;
        foreach (Transform child in playerList)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(playerBannerPrefab, playerList.transform).GetComponent<PlayerBanner>().SetUp(players[i]);
        }
        startButton.SetActive(PhotonNetwork.IsMasterClient);
        roomName.text = roomNameInput.text;
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed");
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log(PhotonNetwork.CountOfRooms);
        Debug.Log(roomList.Count);
        foreach (Transform trans in RoomList)
        {
            Destroy(trans.gameObject);
        }
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(roomJoinButton, RoomList.transform).GetComponent<RoomButton>().SetUp(roomList[i]);
        }
    }

    public void JoinRoom(RoomInfo info)
    {
        if (placeHolderName.text == "")
        {
            placeHolderName.text = "Player" + Random.Range(0, 9000).ToString("0000");
            playerNameInput.text = placeHolderName.text;
            PhotonNetwork.NickName = placeHolderName.text;
        }
        else
        {
            PhotonNetwork.NickName = playerNameInput.text;
        }
        PhotonNetwork.JoinRoom(info.Name);
        roomName.text = info.Name;
        //loading screen
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerBannerPrefab, playerList.transform).GetComponent<PlayerBanner>().SetUp(newPlayer);
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        namingScreen();
    }
    public void PregameLobbyScreen()
    {
        if (string.IsNullOrEmpty(roomNameInput.text))
        {
            return;
        }
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].gameObject.SetActive(false);
        }
        menus[5].SetActive(true);
    }
    public void SetMasterVolume(float sliderValue)
    {
        masterMixer.SetFloat("MasterVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MasterVol", sliderValue);
    }
    public void SetMusicVolume(float sliderValue)
    {
        masterMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVol", sliderValue);
    }
    public void SetSFXVolume(float sliderValue)
    {
        masterMixer.SetFloat("SfxVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SfxVol", sliderValue);
    }
}
