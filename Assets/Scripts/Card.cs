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

    private Vector3 targetPoint;
    private Quaternion targetRot;
    public float moveSpeed = 5f, rotateSpeed = 540f;


    public bool inHand;
    public int handPosition;

    private HandController theHC;

    private bool isSelected;
    private Collider theCol;

    public LayerMask whatIsDeskTop, whatIsPlacement;
    private bool justPressed;

    public CardPlacePoint assignedPlace;


    // Start is called before the first frame update
    void Start()
    {
        if(targetPoint == Vector3.zero)
        {
            targetPoint = transform.position;
            targetRot = transform.rotation;

        }
        
        setupCard();
        
        theHC = FindObjectOfType<HandController>();
        theCol = GetComponent<Collider>();

        
    }

    public void setupCard()
    {
        frontSide.sprite = cardSO.frontSide;
        backSide.sprite = cardSO.backSide;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPoint, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);

        if(isSelected)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit; 
            if(Physics.Raycast(ray,out hit, 100f, whatIsDeskTop))
            {
                MoveToPoint(hit.point + new Vector3(0f, 2f, 0f), Quaternion.identity);
            }

            if(Input.GetMouseButtonDown(1))
            {
                ReturnToHand();
            }

            if(Input.GetMouseButtonDown(0) && justPressed == false)
            {
                if(Physics.Raycast(ray, out hit, 100f, whatIsPlacement))
                {
                    CardPlacePoint selectedPoint = hit.collider.GetComponent<CardPlacePoint>();
                    if(selectedPoint.activeCard == null && selectedPoint.isPlayerPoint)
                    {
                        selectedPoint.activeCard = this;
                        assignedPlace = selectedPoint;

                        MoveToPoint(selectedPoint.transform.position, Quaternion.identity);

                        inHand = false;

                        isSelected = false;
                    } else {
                        ReturnToHand();
                    }

                } else
                {
                    ReturnToHand();
                }
            }
        }

        justPressed = false;
    }

    public void MoveToPoint(Vector3 pointToMoveTo, Quaternion rotToMatch)
    {
        targetPoint = pointToMoveTo;
        targetRot = rotToMatch;
    }

    private void OnMouseOver()
    {
        if(inHand)
        {
            MoveToPoint(theHC.cardPositions[handPosition] + new Vector3(0f, 1f,.5f), Quaternion.identity);
        }
    }

    private void OnMouseExit()
    {
        if(inHand)
        {
            MoveToPoint(theHC.cardPositions[handPosition], theHC.minPos.rotation);
        }
    }

    private void OnMouseDown()
    {
        if(inHand)
        {
            isSelected = true;
            theCol.enabled = false;

            justPressed = true;
        }
    }

    public void ReturnToHand()
    {
        isSelected = false;
        theCol.enabled = true;

        MoveToPoint(theHC.cardPositions[handPosition],theHC.minPos.rotation);
    }


}
