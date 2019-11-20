using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSpell : MonoBehaviour, ISpell
{
    [Header("References:")]
    [SerializeField] private GameObject shieldPartPrefab;

    [Header("Spell Values:")]
    [SerializeField] private float shieldPartSize;

    private PlayerBehaviour player;
    private BasicSpellBehaviour basicSpell;
    private GameObject shieldObject;
    private PhotonView PV;

    private ShieldBehaviour shield;

    Vector3 anchorPosition;

    public void BeginCast(Vector3 _position, PlayerBehaviour _playerOwner, BasicSpellBehaviour _basicSpell)
    {
        PV = GetComponent<PhotonView>();
        player = _playerOwner;
        anchorPosition = _position;
        basicSpell = _basicSpell;

        shieldObject = new GameObject();
        shieldObject.name = "shieldObject (" + _playerOwner.name + ")";
        shieldObject.AddComponent<LifeTime>();
        shieldObject.AddComponent<PhotonView>();
       // shieldObject.AddComponent<ShieldBehaviour>();
        shieldObject.GetComponent<LifeTime>().lifetime = 25f;

        //shield = shieldObject.GetComponent<ShieldBehaviour>();

    }

    public void Casting(Vector3 _position)
    {
        if (player.HasEnoughMana(basicSpell.GetSpellCost))
        {
            if (Vector3.Distance(_position, anchorPosition) >= shieldPartSize)
            {
                //create shield part
                CreateShieldPart(anchorPosition, _position);
                anchorPosition = _position;
                player.DrainMana(basicSpell.GetSpellCost);
            }
        }
        else
        {
            basicSpell.EndCast(_position);
        }
    }

    public void EndCast(Vector3 _position)
    {
        CreateShieldPart(anchorPosition, _position);
    }

    private void CreateShieldPart(Vector3 _beginPoint, Vector3 _endPoint)
    {
        //shield.CreatePart(shieldPartPrefab, _beginPoint, _endPoint);

        GameObject _shieldPart = PhotonNetwork.Instantiate(shieldPartPrefab.name, _beginPoint, Quaternion.identity);
       _shieldPart.GetComponent<ShieldSpellPartBehaviour>().SetValues(gameObject, _beginPoint, _endPoint);
        // GameObject _shieldPart = Instantiate(shieldPartPrefab, _beginPoint, Quaternion.identity, shieldObject.transform);
        //strechBetween(_beginPoint, _endPoint, _shieldPart.GetComponent<SpriteRenderer>());
        
        //Strech(_shieldPart, _beginPoint, _endPoint, true);
    }

    

 
   
}
