using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayScreenBehaviour : MonoBehaviour
{
    //Needs to be prive
    [SerializeField] private PlayerCardDeckBehaviour cardDeck;
   

    public void TouchEnter(BaseEventData _data)
    {
        PointerEventData pointerData = _data as PointerEventData;

        Debug.Log("Enter");


        BasicSpellBehaviour playedSpell = HoldingCard(pointerData.pointerId);
        if (playedSpell)
        {
            // DebugManager.Instance.DebugText("card selected");
            playedSpell.CastSpell(Camera.main.ScreenToWorldPoint(pointerData.position));
        }
        else
        {
            Debug.Log("No card selected");
        }
    }


    public void TouchExit(BaseEventData _data)
    {
        PointerEventData pointerData = _data as PointerEventData;
        BasicSpellBehaviour playedSpell = HoldingCard(pointerData.pointerId);
        if (playedSpell)
        {
            playedSpell.EndCast(Camera.main.ScreenToWorldPoint(pointerData.position));
        }

    }

    private BasicSpellBehaviour HoldingCard(int _touchID)
    {

        if (cardDeck)
        {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                for (int i = 0; i < cardDeck.selectedCards.Count; i++)
                {
                    if (cardDeck.selectedCards[i] != null)
                    {
                        if (cardDeck.selectedCards[i].GetTouchID == _touchID)
                        {
                            return cardDeck.selectedCards[i];
                        }
                    }
                }
            }

            if (cardDeck.WindowsSelectedCard)
            {
                return cardDeck.WindowsSelectedCard;
            }
        }
        return null;
    }

    public void SetCardDeck(PlayerCardDeckBehaviour _deck)
    {
        cardDeck = _deck;
    }
   
}
