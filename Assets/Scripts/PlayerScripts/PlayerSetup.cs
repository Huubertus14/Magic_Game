using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    public bool IsDebug;
    private PlayerBehaviour player;
    private void Start()
    {
        player = GetComponent<PlayerBehaviour>();

        if (!IsDebug)
        {
            InitializePlayerCards();
            SetCamera();
        }

    }

    private void InitializePlayerCards()
    {
        if (photonView.IsMine)
        {
            //local player
            player.SetPlayerCards(true);
        }
        else
        {
            //not local player
            player.SetPlayerCards(false);
        }
    }

    private void SetCamera()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Quaternion _rot = Quaternion.Euler(0,0,180);
            Vector3 _pos = new Vector3(0,120,0);

            //rotate player camera
            Camera.main.gameObject.transform.rotation = _rot;
            Camera.main.gameObject.transform.position = _pos;

            _pos.z = 10;
            //rotate game canvas
            MatchManager.Instance.GetGameUI.transform.rotation = _rot;
            MatchManager.Instance.GetGameUI.transform.position= _pos;
        }
    }

}
