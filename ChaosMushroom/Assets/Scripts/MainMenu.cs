﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
  public void GameIsOver()
  {
    gameObject.SetActive(true);
  }
 public void PlayGame () 
  {
    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    SceneManager.LoadScene("MainGame");
  }
  public void QuitGame ()
  {
    Debug.Log ("Quit");
    Application.Quit();
  }
}
  