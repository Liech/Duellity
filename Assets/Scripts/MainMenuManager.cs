using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance;
    public Slider[] volumeSliders;
    public GameObject[] menus;
    public Transform playerList;
    void Start()
    {
        
    }

    void Update()
    {
        
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
    public void Startgame()
    {
        SceneManager.LoadScene("NikiMagneticDevScene");
    }
    public void GameLobby()
    {
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].gameObject.SetActive(false);
        }
        menus[2].SetActive(true);
    }
}
