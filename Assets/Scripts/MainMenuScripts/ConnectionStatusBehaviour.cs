using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionStatusBehaviour : MonoBehaviour
{

    [SerializeField] private Text loadScreenText;
    // Start is called before the first frame update
    [SerializeField] private string messageText;

    private IEnumerator LoadingScreen()
    {
        loadScreenText.text = messageText + ".";
        yield return new WaitForSeconds(0.2f);
        loadScreenText.text = messageText + "..";
        yield return new WaitForSeconds(0.2f);
        loadScreenText.text = messageText + "...";
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(LoadingScreen());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnEnable()
    {
        loadScreenText.text = "Connecting.";
        StartCoroutine(LoadingScreen());
    }
}
