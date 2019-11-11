using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// This class is used to navigate in the main menu en set certain panels on/off
/// </summary>
public class MainMenuBehaviour : MonoBehaviour
{

    [Header("Menu Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject nameInputPanel;
    [SerializeField] private GameObject connectionStatusPanel;
    [SerializeField] private GameObject joiningRoomPanel;

    private void Start()
    {
        connectionStatusPanel.SetActive(false);
        joiningRoomPanel.SetActive(false);
    }

    public void SetNameInput(bool _value)
    {
        nameInputPanel.SetActive(_value);
        connectionStatusPanel.SetActive(false);
    }

    public void SetConnectionStatusPanel(bool _value)
    {
        connectionStatusPanel.SetActive(_value);
        nameInputPanel.SetActive(false);
    }

    public void JoiningRoom()
    {
        connectionStatusPanel.SetActive(false);
        nameInputPanel.SetActive(false);

        joiningRoomPanel.SetActive(true);
    }

}
