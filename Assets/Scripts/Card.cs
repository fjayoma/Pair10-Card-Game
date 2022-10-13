using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public CardScriptableObject cardSO; //pulls in CardScriptable 

    public int cardValue;
    public bool isFaceCard;
    public string cardSuit; // is that needed? 
    public Image frontSide, backSide;

    // Start is called before the first frame update
    void Start()
    {
        setupCard();
    }

    public void setupCard()
    {
        frontSide.sprite = cardSO.frontSide;
        backSide.sprite = cardSO.backSide;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
