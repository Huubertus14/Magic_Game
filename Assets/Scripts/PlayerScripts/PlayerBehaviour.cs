using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerBehaviour : MonoBehaviourPun, IDamageAble
{
    [Header("Debug Values")]
    [SerializeField] private bool DebugMode;


    [Header("Player Values:")]
    [SerializeField] private string playerName;
    [SerializeField] private float playerMana;

    [Header("References:")]
    [SerializeField] private CustomSliderScript manaUI;
    [SerializeField] private CustomSliderScript hpUI;
    [SerializeField] private PlayerCardDeckBehaviour cardDeck;
    [SerializeField] private GameObject playerCard;
    [SerializeField] private DamageAble damageAble;
    [SerializeField] private PhotonView PV;
    [SerializeField] private SpriteRenderer spriteRenderer;

    //Static game values
    private readonly float playerBeginMana = 2.8f;
    private readonly float playerMaxMana = 10f;
    private readonly float manaReplentishSpeed = 1f;
    //---- Needs to be replaced to antoher place ---- //

    private void Start()
    {
        //initialize card deck
        cardDeck = GetComponentInChildren<PlayerCardDeckBehaviour>();
        damageAble = GetComponent<DamageAble>(); ;
        PV = GetComponent<PhotonView>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        playerMana = playerBeginMana;

        manaUI.InitSlider(playerMaxMana, playerMana);
        hpUI.InitSlider(damageAble.StartHealth, damageAble.StartHealth);

        if (!DebugMode)
            spriteRenderer.enabled = false;

        if (cardDeck)
        {
            cardDeck.GiveCardSet(this);
        }
    }

    private void FixedUpdate()
    {
        RegainMana();
        Debug.LogWarning("Remove this when in game");

        if (MatchManager.Instance.IsGameStarted)
        {
            if (!spriteRenderer.enabled)
            {
                if (!DebugMode)
                    spriteRenderer.enabled = true;
            }
            RegainMana();
        }
        else
        {
            if (!DebugMode)
                spriteRenderer.enabled = false;
        }
    }

    public void SetPlayerCards(bool _value)
    {
        playerCard.SetActive(_value);
    }

    private void RegainMana()
    {
        playerMana += manaReplentishSpeed * Time.deltaTime;
        if (playerMana > playerMaxMana)
        {
            playerMana = playerMaxMana;
        }
        manaUI.SetSlider(playerMana);
    }

    public PlayerCardDeckBehaviour GetCardDeck
    {
        get
        {
            if (!cardDeck)
            {
                cardDeck = GetComponentInChildren<PlayerCardDeckBehaviour>();
            }
            return cardDeck;
        }
    }

    public bool HasEnoughMana(float _spellCost)
    {
        return (playerMana >= _spellCost);
    }

    public void DrainMana(float _drainAmount)
    {
        playerMana -= _drainAmount;
        manaUI.SetSlider(playerMana);
    }

    public void UpdateDamage(float _value)
    {
        hpUI.SetSlider(_value);
        if (PV.IsMine)
        {
            if (_value <= 0)
            {
                MatchManager.Instance.GameOver(PhotonNetwork.NickName);
            }
        }
    }
}
