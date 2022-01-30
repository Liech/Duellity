using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinnerScreenButtons : MonoBehaviour
{
    // Start is called before the first frame update
   public void PlayAgain()
   {
        SceneManager.LoadScene("NikiMagneticDevScene");
   }
    public void BackToMain()
    {
        SceneManager.LoadScene("LocalMainMenu");
    }
}
