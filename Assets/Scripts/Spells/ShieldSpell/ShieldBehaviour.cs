using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ShieldBehaviour : MonoBehaviour
{

    PhotonView PV;

    GameObject shieldPart;
    // Start is called before the first frame update
    public void CreatePart(GameObject _shieldPart, Vector3 _begin, Vector3 _end)
    {
        if (PV == null)
        {
            PV = GetComponent<PhotonView>();
        }

        shieldPart = _shieldPart;

        GameObject _part = PhotonNetwork.Instantiate(shieldPart.name, _begin, Quaternion.identity);
        PV.RPC("RPC_SetParent", RpcTarget.AllBuffered, _part);
        PV.RPC("RPC_Strech", RpcTarget.AllBuffered, _part, _begin, _end, false);
    }

    [PunRPC]
    void RPC_SetParent(GameObject _object)
    {
        _object.transform.SetParent(transform);
    }


    [PunRPC]
    void RPC_Strech(GameObject _sprite, Vector3 _initialPosition, Vector3 _finalPosition, bool _mirrorZ)
    {
        Vector3 centerPos = (_initialPosition + _finalPosition) / 2f;
        _sprite.transform.position = centerPos;
        Vector3 direction = _finalPosition - _initialPosition;
        direction = Vector3.Normalize(direction);
        _sprite.transform.right = direction;
        if (_mirrorZ) _sprite.transform.right *= -1f;
        Vector3 scale = new Vector3(1, 5, 1);
        scale.x = Vector3.Distance(_initialPosition, _finalPosition);
        _sprite.transform.localScale = scale;
    }



}
