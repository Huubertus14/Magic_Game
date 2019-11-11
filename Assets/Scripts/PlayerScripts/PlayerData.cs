using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerData : MonoBehaviour
{

    public static PlayerData Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    [Header("References:")]
    [SerializeField] private MainMenuBehaviour mainMenu;


    [Header("Player Values")]
    private string playerName;

    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(this.gameObject);

        if (!PlayerPrefs.HasKey("Player Name"))
        {
            mainMenu.SetNameInput(true);
        }
        else
        {
            PhotonNetwork.NickName = PlayerPrefs.GetString("Player Name");
        }
    }

    public void SetName(string _name)
    {
        PlayerPrefs.SetString("Player Name", _name);
        PhotonNetwork.NickName = PlayerPrefs.GetString("Player Name");
        playerName = _name;
        PlayerPrefs.Save();
    }




}
