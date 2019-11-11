using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    public static DebugManager Instance;
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    [Header("Debug text Objects:")]
    public GameObject DebugTextPrefab;

    public void DebugText(string _text, float _time)
    {
        GameObject _newTextObject = Instantiate(DebugTextPrefab, transform);
        _newTextObject.GetComponent<DebugTextItem>().SetText(_text, _time);
        Debug.Log(_text);
    }

    public void DebugText(string _text)
    {
        GameObject _newTextObject = Instantiate(DebugTextPrefab, transform);
        _newTextObject.GetComponent<DebugTextItem>().SetText(_text);
        Debug.Log(_text);
    }
    public void DebugText(string _text, Color _col)
    {
        GameObject _newTextObject = Instantiate(DebugTextPrefab, transform);
        _newTextObject.GetComponent<DebugTextItem>().SetText(_text, _col);
    }
}
