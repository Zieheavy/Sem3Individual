using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRenderer : MonoBehaviour
{
    public Transform ball;

    public void SetPlayerPosition(ClassLibrary.PongGame pongGame)
    {
        Debug.Log("PlayerPos: " + pongGame.GameName);
    }

    public void SetBalPosition(ClassLibrary.PongGame pongGame)
    {
        Debug.Log("BalPos: " + pongGame.GameName);
        Vector3 balPos = new Vector3(pongGame.BalX/10, (pongGame.BalY/10)*-1);
        ball.position = Vector3.Lerp(ball.position, balPos, 200);
    }
}
