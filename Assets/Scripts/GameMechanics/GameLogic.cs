using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject crosshair;
    [SerializeField] Transform endTarget;
    [SerializeField] Transform player;
    public bool active;
    public bool pause;
    public bool movable;
    public int score;
    public bool newHighScore;
    public int highScore;
    // Start is called before the first frame update
    void Start()
    {
        LockCursor();
        movable = true;
        active = true;
        pause = false;
        newHighScore = false;
        pauseMenu.SetActive(false);
        crosshair.SetActive(true);
        if (PlayerPrefs.HasKey("highScore"))
        {
            highScore = PlayerPrefs.GetInt("highScore");
        }
        else
        {
            PlayerPrefs.SetInt("highScore", 0);
        }
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
        if(Vector3.Distance(player.position,endTarget.position) < 1)
        {
            movable = false;
            crosshair.SetActive(false);
            UnlockCursor();
            if(PlayerPrefs.GetInt("highScore") < score)
            {
                PlayerPrefs.SetInt("highScore", score);
                newHighScore = true;
            }
            else
            {
                newHighScore = false;
                highScore = PlayerPrefs.GetInt("highScore");
            }
        }
    }

    public void Pause()
    {
        UnlockCursor();
        pause = true;
        active = false;
        pauseMenu.SetActive(true);
        crosshair.SetActive(false);
    }

    public void Resume()
    {
        LockCursor();
        pause = false;
        active = true;
        crosshair.SetActive(true);
    }

    public void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Deffusing()
    {
        UnlockCursor();
        movable = false;
        crosshair.SetActive(false) ;
    }

    public void Deffused(int points)
    {
        score += points;
        LockCursor();
        movable = true;
        crosshair.SetActive(true);
    }
}
