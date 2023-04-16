using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public bool gameIsActive = false;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
           StartGame();
        }
    }

    public void StartGame()
    {
        gameIsActive = true;
    }
}
