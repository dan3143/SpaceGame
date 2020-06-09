﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardGameManager : MonoBehaviour
{
    const float offsetX = 3f;
    const float offsetY = 3f;
    private int score = 0;
    private Card[,] cards;
    private int current_x;
    private int current_y;
    private Card firstRevealed;
    private Card secondRevealed;
    private bool _canReveal = true;
    [SerializeField] int rows = 3;
    [SerializeField] int columns = 4;
    [SerializeField] private Sprite[] images;
    [SerializeField] private Card originalCard;
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private GameObject returnMenu;

    void Awake() 
    {
        cards = new Card[rows, columns];
        current_x = 0;
        current_y = 0;
    }

    public bool canReveal {
        get {
            return secondRevealed == null;
        }
    }

    public void CardRevealed(Card card)
    {
        if (firstRevealed == null){
            firstRevealed = card;
        } else {
            secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch() 
    {
        if (firstRevealed.id == secondRevealed.id) {
            score++;
            scoreTxt.text = "SCORE: " + score;
            if (score == images.Length) {
                returnMenu.SetActive(true);
                
            }
        } else {
            yield return new WaitForSeconds(.5f);
            firstRevealed.Unreveal();
            secondRevealed.Unreveal();
        }
        firstRevealed = null;
        secondRevealed = null;
    }

    void Start()
    {
        Vector3 startPos = originalCard.transform.position;
        int[] ids = getIds();
        ids = Shuffle(ids);
        for (int i = 0; i < columns; i++) {
            for (int j = 0; j < rows; j++) {
                Card card;
                if (i == 0 && j == 0) {
                    card = originalCard;
                } else {
                    card = Instantiate(originalCard) as Card;
                }
                int index = j * columns + i;
                int id = ids[index];
                card.SetCard(id, images[id]);
                float x = (offsetX * i) + startPos.x;
                float y = -(offsetY * j) + startPos.y;
                card.transform.position = new Vector3(x, y, startPos.z);
                cards[j, i] = card;
            }
        }
        cards[current_y, current_x].toggleSelect();
        showCards();
    }

    int [] getIds() 
    {
        int[] ids = new int[images.Length*2];
        int currentId = 0;
        for (int i = 0; i < ids.Length; i++) {
            ids[i] = currentId;
            if (i % 2 == 1) {
                currentId++;
            }
        }
        return ids;
    }

    int[] Shuffle(int[] arr) {
        int[] shuffled = arr.Clone() as int[];
        for (int i = 0; i < shuffled.Length; i++) {
            int tmp = shuffled[i];
            int rand = Random.Range(i, shuffled.Length);
            shuffled[i] = shuffled[rand];
            shuffled[rand] = tmp;
        }
        return shuffled;
    }

    void Message(string message)
    {
        cards[current_y, current_x].toggleSelect();
        if (message == "up" && current_y > 0) {
            current_y -= 1;
        } else if (message == "down" && current_y < rows-1) {
            current_y += 1;
        }
        if (message == "left" && current_x > 0) {
            current_x -= 1;
        } else if (message == "right" && current_x < columns-1) {
            current_x += 1;
        }
        cards[current_y, current_x].toggleSelect();
        if (message == "a") {
            cards[current_y, current_x].Reveal();
        }
    }

    void Update() {
        if (Application.platform == RuntimePlatform.LinuxEditor || Application.platform == RuntimePlatform.WindowsEditor) {
            if (Input.GetButtonDown("Left")) Message("left");
            if (Input.GetButtonDown("Right")) Message("right");
            if (Input.GetButtonDown("Down")) Message("down");
            if (Input.GetButtonDown("Up")) Message("up");
            if (Input.GetButtonDown("A")) Message("a");
        }
    }

    void showCards() {
        for (int i=0; i < cards.GetLength(0); i++) {
            string row = "";
            for (int j = 0; j < cards.GetLength(1); j++) {
                row += cards[i, j].id +  " ";
            }
            Debug.Log(row);
        }
    }

    public void ReturnToMenu()
    {
        
    }
}