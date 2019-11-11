using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DamageAble : MonoBehaviour
{
    [Header("Damage Values")]
    [SerializeField] private float startHealth;
    [SerializeField] private float currentHealth;

    private PhotonView PV;

    IDamageAble damageObject;

    private void Start()
    {
        currentHealth = startHealth;
        PV = GetComponent<PhotonView>();
    }

    [PunRPC]
    void RPC_DoDamage(int _damage)
    {
        currentHealth -= _damage;
        if (damageObject == null)
        {
            damageObject = GetComponent<IDamageAble>();
        }
        damageObject.UpdateDamage(currentHealth);
    }


    public PhotonView GetPV => PV;

    public string DamageName => "RPC_DoDamage";

    public float StartHealth => startHealth;
}
