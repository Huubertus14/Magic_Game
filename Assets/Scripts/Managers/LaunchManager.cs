using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;



/// <summary>
/// Thiss class is used to make all connection to the internet and make connection to the server. It also makes matchmaking possible etc
/// </summary>
public class LaunchManager : MonoBehaviourPunCallbacks
{
    [Header("References:")]
    [SerializeField] private MainMenuBehaviour mainMenuBehaviour;

    [Header("Match making settings")]
    private int joinTryCount;
    [SerializeField] private int maxJoinTry = 150;

    #region Unity Methods
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        ConnectToPhotonServer();
    }

    #endregion

    #region Public Mehtods
    public void ConnectToPhotonServer()
    {
        Debug.Log("Reconnect");
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public IEnumerator JoinRandomRoom()
    {
        joinTryCount = 0;
        while (joinTryCount < maxJoinTry)
        {
            joinTryCount++;
            PhotonNetwork.JoinRandomRoom();

            yield return new WaitForSeconds(0.05f);
           // DebugManager.Instance.DebugText("Try to join Room", 3f);
        }
    }

    #endregion

    #region CallBack Methods
    public override void OnConnectedToMaster()
    {
       // DebugManager.Instance.DebugText("--- " + PhotonNetwork.NickName + " - Connect to Photon Servers ---");
       // DebugManager.Instance.DebugText("App " + PhotonNetwork.AppVersion);
       // DebugManager.Instance.DebugText("Game " + PhotonNetwork.GameVersion);

        Debug.Log("Connected to master");

//mainMenuBehaviour.JoiningRoom();

        //find a random room to join!
       // StartCoroutine(JoinRandomRoom());
    }

    public override void OnConnected()
    {
        // DebugManager.Instance.DebugText("--- Connect to Internet ---");
        Debug.Log("Connected");
    }

    public override void OnJoinedRoom()
    {
        StopAllCoroutines();
        StopCoroutine(JoinRandomRoom());
       // DebugManager.Instance.DebugText(PhotonNetwork.NickName + " Joined to " + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel("GameTestScene");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        StopCoroutine(JoinRandomRoom());
       //DebugManager.Instance.DebugText(newPlayer.NickName + " Joined to " + PhotonNetwork.CurrentRoom.Name + " " + PhotonNetwork.CurrentRoom.PlayerCount);
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // base.OnJoinRandomFailed(returnCode, message);
        //  Debug.LogError(message);
        if (joinTryCount >= maxJoinTry)
        {
            CreateAndJoinRandomRoom();
        }
    }
    #endregion

    #region Private Mehtods

    private void CreateAndJoinRandomRoom()
    {
       // DebugManager.Instance.DebugText("Create New Room");

        string _randomRoomName = "Room " + Random.Range(1000, 10000); ;

        RoomOptions roomOptions = new RoomOptions
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 2 
        };

        PhotonNetwork.CreateRoom(_randomRoomName, roomOptions);
       // DebugManager.Instance.DebugText("Max players = " + roomOptions.MaxPlayers.ToString());
    }

    #endregion
}
