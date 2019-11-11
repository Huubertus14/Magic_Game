using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BasicSpellBehaviour : MonoBehaviour
{
    private Vector3 fingerPos, orginPosition;

    [Header("Spell Values")]
    [SerializeField] private float spellCost;

    [SerializeField] private Image cardImage;

    [SerializeField] private bool selected;

    [SerializeField] private int fingerfollowID;

    [SerializeField] private ISpell cardSpell;

    private PlayerCardDeckBehaviour cardDeck;

    private bool casted;

    private void Start()
    {
        //componetns and values
        cardImage = GetComponent<Image>();
        cardImage.raycastTarget = true;

        cardSpell = (ISpell)gameObject.GetComponent(typeof(ISpell));

        orginPosition = transform.position;
        fingerPos = transform.position;

        casted = false;
        selected = false;
        fingerfollowID = 0;
    }

    public void SetValues(PlayerCardDeckBehaviour _cardDeck)
    {
        cardDeck = _cardDeck;
    }

    /// <summary>
    /// Called from the inspector
    /// Used when a user pushes on this specific card
    /// Set the card to a selected state
    /// </summary>
    public void SelectSpell(BaseEventData _data)
    {
        if (cardDeck.GetPlayer.HasEnoughMana(spellCost))
        {
            PointerEventData pointerData = (PointerEventData)_data;

            cardDeck.selectedCards.Add(this);
            selected = true;
            cardImage.raycastTarget = false;

            cardDeck.WindowsSelectedCard = this;

            if (Input.touches.Length > 0)
            {
                fingerfollowID = Input.GetTouch(Input.touches.Length - 1).fingerId;
                //  DebugManager.Instance.DebugText("Spell - FingerID = " + pointerData.pointerId);
            }
        }
    }

    /// <summary>
    /// Called from the inspector
    /// Used when a user releases on this specific card
    /// Set the card to a deselected state
    /// </summary>
    public void DeselectSpell(BaseEventData _data)
    {
        PointerEventData pointerData = (PointerEventData)_data;

        cardDeck.selectedCards.Remove(this);

        if (casted)
        {
            cardSpell.EndCast(Camera.main.ScreenToWorldPoint(pointerData.position));
            Destroy(gameObject, 0.2f);
        }
        else
        {
            selected = false;
            casted = false;
            cardImage.raycastTarget = true;
            fingerfollowID = 0;
        }
    }

    public void EndCast(Vector3 _pos)
    {
        cardSpell.EndCast(_pos);
        cardDeck.selectedCards.Remove(this);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (!selected)//If not selected the card will lerp to its own place
        {
            //lerping can be done because there are alway a hand fullof cards on the game
            transform.position = Vector3.Lerp(transform.position, orginPosition, Time.deltaTime * 6);
        }
        else //follow the finger which the player used to select this card
        {
            if (Input.touches.Length > 0)
            {
                SetCardPlaceWithFingerID(fingerfollowID);
            }
            else
            {
                //for now use mouse input, later use fingerID
                Vector3 _input = Input.mousePosition;
                _input.z = 5;
                transform.position = Camera.main.ScreenToWorldPoint(_input);
            }
        }

        if (casted)
        {
            cardSpell.Casting(transform.position);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            cardSpell.EndCast(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    private void SetCardPlaceWithFingerID(int _fingerID)
    {
        for (int i = 0; i < Input.touches.Length; i++)
        {
            if (Input.GetTouch(i).fingerId == fingerfollowID)
            {
                Vector3 _newPos = Input.GetTouch(i).position;
                _newPos.z = 5;
                transform.position = Camera.main.ScreenToWorldPoint(_newPos);
            }
        }
    }

    private Touch GetCurrentTouch(int _id)
    {
        for (int i = 0; i < Input.touches.Length; i++)
        {
            if (Input.GetTouch(i).fingerId == _id)
            {
                return Input.GetTouch(i);
            }
        }
        return Input.GetTouch(0);
    }

    public void CastSpell(Vector3 _eventPosition)
    {
        
        casted = true;

        _eventPosition.z = 5;
        cardSpell.BeginCast(_eventPosition, cardDeck.GetPlayer, this);

        cardImage.color = new Color(0, 0, 0, 0);
    }



    public int GetTouchID => fingerfollowID;

    public float GetSpellCost => spellCost;
}
