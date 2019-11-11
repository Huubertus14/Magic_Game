using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpell : MonoBehaviour , ISpell
{
    [Header("Debug Things")]
    public GameObject debugTriangle;
    public GameObject debugCircle;

    public void BeginCast(Vector3 _position, PlayerBehaviour _playerOwner, BasicSpellBehaviour _basicSpell)
    {
        CreateObject(_position, debugTriangle);
    }

    public void Casting(Vector3 _position)
    {
        CreateObject(_position, debugCircle);
    }

    public void EndCast(Vector3 _position)
    {
        CreateObject(_position, debugTriangle);
    }

    private void CreateObject(Vector3 _position, GameObject _object)
    {
        _position.z = 10;
        GameObject _triangle = Instantiate(_object, _position, Quaternion.identity);
    }
}
