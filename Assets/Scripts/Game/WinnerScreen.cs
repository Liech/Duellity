using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerScreen : MonoBehaviour {
  // Start is called before the first frame update
  void Start() {
    GetComponent<UnityEngine.UI.Text>().text=GameStateSingleton.WinnerText;
  }

  // Update is called once per frame
  void Update() {
  }
}