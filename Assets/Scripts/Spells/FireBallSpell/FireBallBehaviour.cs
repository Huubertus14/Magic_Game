using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FireBallBehaviour : MonoBehaviourPunCallbacks
{
    Rigidbody2D rb;
    PlayerBehaviour player;
    PhotonView PV;


    [SerializeField] private float fireBallDamage;

    public void ShootFireBall(Vector3 _beginPoint, Vector3 _endPoint, float _speed, PlayerBehaviour _playerOwner)
    {
        PV = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
        player = _playerOwner;

        fireBallDamage = 10;

        Vector2 _addedForce = _endPoint - _beginPoint;
        _addedForce.Normalize();

        rb.AddForce(_addedForce * _speed);

      //  DebugManager.Instance.DebugText("Create fire ball");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageAble _damage = collision.gameObject.GetComponent<DamageAble>();
        if (_damage)
        {
            if (!_damage.GetPV.IsMine)
            {
                _damage.GetPV.RPC(_damage.DamageName, RpcTarget.AllBuffered, (int)fireBallDamage);
                if (PV != null)
                {
                    PhotonNetwork.Destroy(PV);
                }
            }
        }
    }
}
