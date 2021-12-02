using UnityEngine;
using UnityEngine.UI;
using System;

public class SignalRConnection : MonoBehaviour
{
    public string signalRHubURL = "http://localhost:5000/hubs/pong";

    public string SendPlayerPos = "SendPlayerPosition";

    public string statusText = "Awaiting Connection...";
    public string connectedText = "Connection Started";
    public string disconnectedText = "Connection Disconnected";

    private const string ReceivePlayerPos = "ReceivePlayerPosition";
    private const string ReceiveBalPos = "ReceiveBallPosition";

    private Text uiText;
    private GameRenderer _gameRenderer;

    void Start()
    {
        uiText = GameObject.Find("Text").GetComponent<Text>();
        _gameRenderer = gameObject.GetComponent<GameRenderer>();

        //DisplayMessage(statusText);

        var signalR = new SignalR();
        signalR.Init(signalRHubURL);

        //signalR.On("TestReceive", (string payload) =>
        //{
        //    var json = JsonUtility.FromJson<JsonPayload>(payload);
        //});

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

        //attempts to make a connection
        signalR.ConnectionStarted += (object sender, ConnectionEventArgs e) =>
        {
            Debug.Log($"Connected: {e.ConnectionId}");

            //    var json1 = new JsonPayload
            //    {
            //        message = "Unity Message"
            //    };
            //    //signalR.Invoke("TestFuncion", JsonUtility.ToJson(json1));
            //var json2 = new ClassLibrary.PlayerPositions
            //{
            //    GameName = "test",
            //    Position = 5,
            //    PlayerType = 1,
            //};
            //signalR.Invoke("SendPlayerPosition", (json2));
        };

        //dislays a debug message when connection lost
        signalR.ConnectionClosed += (object sender, ConnectionEventArgs e) =>
        {
            Debug.Log($"Disconnected: {e.ConnectionId}");
        };

        signalR.Connect();
    }
}
