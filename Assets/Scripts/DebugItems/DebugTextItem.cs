using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugTextItem : MonoBehaviour
{

    private Text textComponent;

    float lifeTime;

    public void SetText(string _text)
    {
        lifeTime = 1200f;
        textComponent = GetComponent<Text>();
        textComponent.text = _text;
        textComponent.color = Color.white;
    }
    public void SetText(string _text, float _lifetime)
    {
        lifeTime = _lifetime;
        textComponent = GetComponent<Text>();
        textComponent.text = _text;
        textComponent.color = Color.white;
    }

    public void SetText(string _text, Color _col)
    {
        lifeTime = 15f;
        textComponent = GetComponent<Text>();
        textComponent.text = _text;
        textComponent.color = _col;
    }

    private void FixedUpdate()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0 && lifeTime > -5)
        {
            Destroy(gameObject);
        }
    }
}
