using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class SignalRController : MonoBehaviour
{
    public string signalRHubURL = "http://localhost:5000/hubs/pong";

    public string SendPlayerPos = "SendPlayerPosition";

    private const string ReceivePlayerPos = "ReceivePlayerPosition";
    private const string ReceiveBalPos = "ReceiveBallPosition";

    private GameRenderer _gameRenderer;
    private UIController _UIController;

    private SignalR signalR = new SignalR();

    void Start()
    {
        _gameRenderer = gameObject.GetComponent<GameRenderer>();
        _UIController = gameObject.GetComponent<UIController>();

        signalR.Init(signalRHubURL);

        //checks if playerPositions are received
        signalR.On(ReceivePlayerPos, (ClassLibrary.PongGame payload) =>
        {
            _gameRenderer.SetPlayerPosition(payload);
        });
        
        //checks if ballPositions are received
        signalR.On(ReceiveBalPos, (ClassLibrary.PongGame payload) =>
        {
            _gameRenderer.SetBalPosition(payload);
        });

        signalR.On("GameAdded", (List<ClassLibrary.PongGame> payload) =>
        {
            _UIController.UpdateGameList(payload);
        });

        //attempts to make a connection
        signalR.ConnectionStarted += (object sender, ConnectionEventArgs e) =>
        {
            Debug.Log($"Connected: {e.ConnectionId}");

            signalR.Invoke("CreateGame", ("initialUpdateList"));
        };

        //dislays a debug message when connection lost
        signalR.ConnectionClosed += (object sender, ConnectionEventArgs e) =>
        {
            Debug.Log($"Disconnected: {e.ConnectionId}");
        };

        signalR.Connect();
    }

    public void JoingGame(string gameName)
    {
        signalR.Invoke("JoingGame", gameName, 2);
    }
}
