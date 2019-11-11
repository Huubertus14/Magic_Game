using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevelopManager : MonoBehaviour
{

    [Header("PLayer Create Values:")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Vector3 hostPosition;
    [SerializeField] private Vector3 clientPosition;
    private PlayerBehaviour player;


    // Start is called before the first frame update
    void Start()
    {
        CreatePlayer();
    }

    void CreatePlayer()
    {
        GameObject _play;

        Vector3 _createPosition = hostPosition;

        _play = Instantiate(playerPrefab, _createPosition, Quaternion.Euler(0, 0, 0));

    }

    // Update is called once per frame
    void Update()
    {

    }
}
