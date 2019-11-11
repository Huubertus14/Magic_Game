using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class FireBallSpell : MonoBehaviour, ISpell
{

    Vector3 beginSpellCast, endSpellCast;

    PlayerBehaviour player;
    BasicSpellBehaviour basicSpell;

    [Header("Prefabs:")]
    [SerializeField] private GameObject fireBallPrefab;

    public void BeginCast(Vector3 _position, PlayerBehaviour _playerOwner, BasicSpellBehaviour _basicSpell)
    {
        player = _playerOwner;
        beginSpellCast = _position;
        basicSpell = _basicSpell;
       // DebugManager.Instance.DebugText("Begin Cast:" + _position);
    }

    public void Casting(Vector3 _position)
    {
        
    }

    public void EndCast(Vector3 _position)
    {
        endSpellCast = _position;
        endSpellCast.z = 1;
        beginSpellCast.z = 1;
        GameObject _fireball = PhotonNetwork.Instantiate(fireBallPrefab.name, beginSpellCast, Quaternion.identity);
        FireBallBehaviour _behav = _fireball.GetComponent<FireBallBehaviour>();
        player.DrainMana(basicSpell.GetSpellCost);

        if (_behav)
        {
            _behav.ShootFireBall(beginSpellCast, endSpellCast, 40*1000, player);
        }
    }
}
