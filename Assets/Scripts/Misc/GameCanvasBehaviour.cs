using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvasBehaviour : MonoBehaviour
{
    [Header("References:")]
    [SerializeField] private Text timeLeftText;


    public void SetTimeText(float _timeLeft)
    {
        int _minutes = (int)(_timeLeft / 60);
        int _seconds = (int)(_timeLeft % 60);
        if (_seconds < 10)
        {
            timeLeftText.text = _minutes + ":0" + _seconds;
        }
        else
        {
            timeLeftText.text = _minutes + ":" + _seconds;
        }
    }
}
