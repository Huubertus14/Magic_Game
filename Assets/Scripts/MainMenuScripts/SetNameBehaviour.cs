using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetNameBehaviour : MonoBehaviour
{

    [SerializeField] private InputField nameInputField;
    // Start is called before the first frame update
    void Start()
    {
        nameInputField = GetComponentInChildren<InputField>();
    }

    private void OnEnable()
    {
        if (nameInputField == null)
        {
            nameInputField = GetComponentInChildren<InputField>();
        }
        nameInputField.text = PlayerPrefs.GetString("Player Name");
    }

    public void SaveName()
    {
        if (nameInputField.text == "")
        {
            
            return;
        }

        PlayerData.Instance.SetName(nameInputField.text);
        gameObject.SetActive(false);
    }
}
