using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(PhotonView))]
public class ShieldSpellPartBehaviour : MonoBehaviour, IDamageAble
{
    GameObject parent;
    PhotonView PV;
    public void SetValues(GameObject _parent, Vector3 _beginPoint, Vector3 _endPoint)
    {
        parent = _parent;
        PV = GetComponent<PhotonView>();
        //PhotonNetwork.AllocateViewID(PV);

        // PV.RPC("RPC_Strech", RpcTarget.AllBuffered, gameObject, _beginPoint, _endPoint, false);
        RPC_Strech(gameObject, _beginPoint, _endPoint, false);

    }

    public void UpdateDamage(float _value)
    {
        if (PV != null)
            PhotonNetwork.Destroy(PV);
        else
            Destroy(gameObject);
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
