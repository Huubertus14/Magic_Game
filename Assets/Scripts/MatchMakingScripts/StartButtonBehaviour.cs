using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

/// <summary>
/// This class is used  to do all the things needed to start a match. Like the start of the networking and matchmaking
/// </summary>
public class StartButtonBehaviour : MonoBehaviour
{
    [Header("References:")]
    [SerializeField] LaunchManager launchManager;
    [SerializeField] MainMenuBehaviour mainMenu;
    /// <summary>
    /// Called from the inspector, used when the player presses the start game button!
    /// </summary>
    public void StartGame()
    {
        if (!string.IsNullOrEmpty(PhotonNetwork.NickName))
        {
            launchManager.ConnectToPhotonServer();
            mainMenu.SetConnectionStatusPanel(true);
            mainMenu.JoiningRoom();
            StartCoroutine(launchManager.JoinRandomRoom());

        }
    }
}
