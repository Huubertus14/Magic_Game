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

    Vector3 anchorPosition;

    public void BeginCast(Vector3 _position, PlayerBehaviour _playerOwner, BasicSpellBehaviour _basicSpell)
    {
        player = _playerOwner;
        anchorPosition = _position;
        basicSpell = _basicSpell;

        shieldObject = new GameObject();
        shieldObject.name = "shieldObject (" + _playerOwner.name + ")";
        shieldObject.AddComponent<LifeTime>();
        shieldObject.GetComponent<LifeTime>().lifetime = 25f;

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
        GameObject _shieldPart = Instantiate(shieldPartPrefab, _beginPoint, Quaternion.identity, shieldObject.transform);
        //strechBetween(_beginPoint, _endPoint, _shieldPart.GetComponent<SpriteRenderer>());
        Strech(_shieldPart, _beginPoint, _endPoint, true);
    }

    public void Strech(GameObject _sprite, Vector3 _initialPosition, Vector3 _finalPosition, bool _mirrorZ)
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
