using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMgr : MonoSingle<UIMgr>
{
    //ref
    public Text survivalTimeText = null;
    public Text killCountText = null;

    //Popup
    [SerializeField] private GameOverPopup gameOverPopup = null;
    [SerializeField] private GameObject pausePopup = null;


    public void OnGameOverPopup(string text)
    {
        gameOverPopup.gameovertext.text = text;
        if (gameOverPopup.gameObject.activeSelf)
        {
            gameOverPopup.gameObject.SetActive(false);
        }
        else
        {
            gameOverPopup.gameObject.SetActive(true);
        }
    }

    public void OnPausePopup()
    {
        if (pausePopup.activeSelf)
        {
            pausePopup.SetActive(false);
        }
        else
        {
            pausePopup.SetActive(true);
        }
    }
}
