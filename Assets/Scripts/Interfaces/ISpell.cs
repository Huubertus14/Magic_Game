using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpell
{
     void BeginCast(Vector3 _position, PlayerBehaviour _playerOwner, BasicSpellBehaviour _basicSpell);

     void Casting(Vector3 _position);
     void EndCast(Vector3 _position);
}
