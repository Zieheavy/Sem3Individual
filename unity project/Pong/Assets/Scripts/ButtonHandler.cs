using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    private Canvas menu;
    private SignalRController gameController;

    private void Start()
    {
        menu = GameObject.FindGameObjectWithTag("Menu").GetComponent<Canvas>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<SignalRController>();
    }

    public void JoinGame()
    {
        Text txt = transform.Find("Text").GetComponent<Text>();

        Debug.Log("JoingGame: " + txt.text);

        gameController.JoingGame(txt.text);
        menu.enabled = false;
    }
}
