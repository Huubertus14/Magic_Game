using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;


public class MatchManager : MonoBehaviourPunCallbacks
{
    public static MatchManager Instance;

    [Header("PLayer Create Values:")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Vector3 hostPosition;
    [SerializeField] private Vector3 clientPosition;
    private PlayerBehaviour player;

    [Header("Game Rules")]
    [SerializeField] private float timeLimit;
    private bool gameStarted;

    [Header("References:")]
    [SerializeField] private GameCanvasBehaviour gameCanvasBehaviour;
    [SerializeField] private PlayScreenBehaviour playScreen;
    [SerializeField] private PhotonView PV;
    [SerializeField] private GameObject waitForPlayerPanel;
    [SerializeField] private GameObject MainGameUI;
    [SerializeField] private GameOverScreenBehaviour gameOverPanel;

    #region Unity Methods

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    private void Start()
    {
        CreatePlayer();

        PV = GetComponent<PhotonView>();

        gameOverPanel.gameObject.SetActive(false);

        waitForPlayerPanel.SetActive(true);

        gameStarted = false;
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Used to create a player in the game
    /// </summary>
    private void CreatePlayer()
    {
        if (PhotonNetwork.IsConnected)
        {
            if (playerPrefab != null)
            {
                GameObject _play;
                if (PhotonNetwork.IsMasterClient)
                {
                    Vector3 _createPosition = hostPosition;

                    _play = PhotonNetwork.Instantiate(playerPrefab.name, _createPosition, Quaternion.Euler(0, 0, 0));
                }
                else
                {
                    Vector3 _createPosition = clientPosition;

                    _play = PhotonNetwork.Instantiate(playerPrefab.name, _createPosition, Quaternion.Euler(0, 0, 180));
                }
                player = _play.GetComponent<PlayerBehaviour>();
                playScreen.SetCardDeck(player.GetCardDeck);
            }
        }
    }

    private void FixedUpdate()
    {
        if (gameStarted)
        {
            MatchRunning();
        }
    }

    [PunRPC]
    private void MatchRunning()
    {
        timeLimit -= Time.deltaTime;
        gameCanvasBehaviour.SetTimeText(timeLimit);
        if (timeLimit <= 0)
        {
            GameOver("None");
        }
    }

    [PunRPC]
    void RPC_StartMatch()
    {
        gameStarted = true;
        waitForPlayerPanel.SetActive(false);
    }

    public void GameOver(string _lostPlayer)
    {
        string _winner = "";

        if (_lostPlayer == PhotonNetwork.PlayerList[0].NickName)
        {
            _winner = PhotonNetwork.PlayerList[1].NickName;
        }
        else
        {
            _winner = PhotonNetwork.PlayerList[0].NickName;
        }

        PV.RPC("RPC_SetGameOverScreen", RpcTarget.AllBuffered, _winner);
    }

    [PunRPC]
    void RPC_SetGameOverScreen(string _winnerName)
    {
        gameStarted = false;
        gameOverPanel.gameObject.SetActive(true);

        // _playerWonText = PhotonNetwork.CurrentRoom.pl

        gameOverPanel.SetText(_winnerName + " Won!");
    }

    #endregion

    #region Public Methods

    public void LeaveGame()
    {
        PhotonNetwork.LeaveRoom();
    }

    #endregion

    #region CallBacks
    public override void OnJoinedRoom()
    {
       // DebugManager.Instance.DebugText(PhotonNetwork.NickName + " Joined the Room");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //DebugManager.Instance.DebugText(newPlayer.NickName + " Joined the Room " + PhotonNetwork.CurrentRoom.Name + " " + PhotonNetwork.CurrentRoom.MaxPlayers + "/" + PhotonNetwork.CurrentRoom.PlayerCount);
       
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            // DebugManager.Instance.DebugText("Lobby full, Lets start te game");
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PV.RPC("RPC_StartMatch", RpcTarget.AllBuffered);
        }
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
    #endregion

    #region Propertys

    public float GetTimeLimit => timeLimit;

    public GameObject GetGameUI => MainGameUI;

    public bool IsGameStarted => gameStarted;

    #endregion
}
