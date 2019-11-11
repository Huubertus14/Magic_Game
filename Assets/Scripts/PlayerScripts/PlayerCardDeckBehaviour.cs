using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCardDeckBehaviour : MonoBehaviour
{

    [SerializeField] private PlayerBehaviour player;

    private BasicSpellBehaviour windowsSelectedSpell;

    [Header("Referencs:")]
    public GameObject cardPrefab;
    [SerializeField] private GameObject[] cardSet;


    private Vector3[] cardPlaces = new Vector3[5] { new Vector3(-400, 65, 0), new Vector3(-200, 65, 0), new Vector3(0, 65, 0), new Vector3(200, 65, 0), new Vector3(400, 65, 0) };

    private BasicSpellBehaviour[] spellsOnDeck = new BasicSpellBehaviour[5];

    public List<BasicSpellBehaviour> selectedCards = new List<BasicSpellBehaviour>();

    public void GiveCardSet(PlayerBehaviour _player)
    {
        player = _player;
        /*
                if (_cardSet != null)
                {
                    cardSet = _cardSet;

                    //scramble card set
                    for (int i = 0; i < cardSet.Length; i++)
                    {
                        GameObject _temp = cardSet[i];
                        int _x = Random.Range(0, cardSet.Length);
                        cardSet[i] = cardSet[_x];
                        cardSet[_x] = _temp;
                    }
                }*/
    }

    private void FixedUpdate()
    {
        //needs to be called when a card is played
        CheckSpells();
    }

    public BasicSpellBehaviour WindowsSelectedCard
    {
        get
        {
            return windowsSelectedSpell;
        }
        set
        {
            windowsSelectedSpell = value;
        }
    }

    /// <summary>
    /// used to check if the spells on the player are empty
    /// </summary>
    private void CheckSpells()
    {
        for (int i = 0; i < spellsOnDeck.Length; i++)
        {
            if (spellsOnDeck[i] == null)
            {
                GetNewSpell(i);
            }
        }
    }

    private void GetNewSpell(int _indexPlace)
    {
        spellsOnDeck[_indexPlace] = Instantiate(cardPrefab, transform).GetComponent<BasicSpellBehaviour>();
        spellsOnDeck[_indexPlace].SetValues(this);
        spellsOnDeck[_indexPlace].transform.localPosition = cardPlaces[_indexPlace];
    }

    #region Propertys

    public PlayerBehaviour GetPlayer => player;

    #endregion

}
