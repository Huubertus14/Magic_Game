using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreenBehaviour : MonoBehaviour
{
    [SerializeField] private Text winText;

    public void SetText(string _value)
    {
        winText.text = _value;
    }
}
