using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class PhotonController : MonoBehaviourPunCallbacks
{

    string _gameVersion = "1";

    public PunLogLevel logLevel = PunLogLevel.Informational;

    public byte MaxPlayersPerRoom = 4;

    public Text Status;

    public Text Players;

    public Text InfoTxt;

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        PhotonNetwork.LogLevel = logLevel;
    }

    void Start()
    {
        Connect();
    }


    private void Update()
    {
        if (!PhotonNetwork.InRoom)
        {
            Status.text = "Not Connected";
            Players.text = "Players in Room:" + PhotonNetwork.CountOfPlayers;
            InfoTxt.text = "Connecting...";
            PhotonNetwork.JoinOrCreateRoom("Test", new RoomOptions() { MaxPlayers = MaxPlayersPerRoom }, null);
        }
        else
        {
            Status.text = "Connected";
            if (PhotonNetwork.CurrentRoom.Players.Count == 1)
            {
                InfoTxt.text = "Waiting For Opponent";
            }
            Players.text = "Players in Room: " + PhotonNetwork.CurrentRoom.Players.Count;
        }
    }

    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = _gameVersion;
        }
    }

    public override void OnConnected()
    {
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("DemoAnimator/Launcher: OnConnectedToMaster() was called by PUN");
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("DemoAnimator/Launcher:OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = MaxPlayersPerRoom }, null);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("DemoAnimator/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
    }
}