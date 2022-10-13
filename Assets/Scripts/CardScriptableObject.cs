using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Card", menuName = "Card", order = 1)] // create something into the asset menu 
public class CardScriptableObject : ScriptableObject
{
    public int cardValue;
    public bool isFaceCard;
    public string cardSuit;
    

    public Sprite frontSide, backSide;

}
