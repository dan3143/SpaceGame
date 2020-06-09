﻿using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Card : MonoBehaviour
{
    public CardGameManager controller;
    private SpriteRenderer frontSprite;
    [SerializeField] private GameObject selector;
    [SerializeField] private GameObject cardBack;
    private int _id;
    private bool selected = false;
    private bool isRevealead = false;
    private SpriteRenderer backSprite;

    public int id {
        get { return _id; }
    }

    public void SetCard(int id, Sprite image)
    {
        _id = id;
        if (frontSprite == null) {
            frontSprite = GetComponent<SpriteRenderer>();
        }
        frontSprite.sprite = image;
    }

    void OnMouseDown()
    {
        Reveal();
    }

    public void toggleSelect() {
        /*if (backSprite == null) {
            backSprite = cardBack.GetComponent<SpriteRenderer>();
        }*/
        selected = !selected;
        selector.SetActive(selected);
        //frontSprite.color = backSprite.color;
    }

    public void Unreveal()
    {
        cardBack.SetActive(true);
        isRevealead = false;
    }

    public void Reveal()
    {
        if (!isRevealead && cardBack.activeSelf && controller.canReveal) {
            cardBack.SetActive(false);
            controller.CardRevealed(this);
            isRevealead = true;
        }    
    }
}