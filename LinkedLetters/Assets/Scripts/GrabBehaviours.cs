using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Régit les comportements du grab

public class GrabBehaviours : MonoBehaviour
{

    //Déclaration des variables
    //============================================
    [Header("Limite de déplacement du grab")]
    public float minX = 0;
    public float maxX = 0;

	[HideInInspector]
	public GameObject actualBall;

	LettersAndWords gameController;
	int cptBall = 0;
	//============================================


	void Awake()
	{
		gameController = GameObject.Find("GameController").GetComponent<LettersAndWords>();
	}


    void Update()
    {
        // Enlever les commentaires si portage sur Mobile
        //TranslateGrabMobile();

        if (Input.mousePresent)
            TranslateGrabStandalone();
    }


    // Mouvement du grab pour plateforme : MOBILE
    void TranslateGrabMobile()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            bool isDone = actualBall.GetComponent<BallBehaviours>().GetIsDone();

            if ((!isDone) && touch.position.y > 350 && touch.position.x >= minX && touch.position.x <= maxX && touch.position.x < transform.position.x + 50 && touch.position.x > transform.position.x - 50)
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    // Positionne la balle selon la translation tactile
                    transform.position = new Vector2(touch.position.x, transform.position.y);
                }
                if (touch.phase == TouchPhase.Ended)
                {
                    UnGrabBall();

                    AddNewBall();
                }
            }
        }
    }


    // Mouvement du grab pour plateforme : PC
    void TranslateGrabStandalone()
    {
        if (Input.mousePosition.y > 350 && Input.mousePosition.x >= minX && Input.mousePosition.x <= maxX && Input.mousePosition.x < transform.position.x + 50 && Input.mousePosition.x > transform.position.x - 50)
        {
            if (Input.GetButton("UnGrab"))
            {
                // Positionne la balle selon la translation de la souris
                transform.position = new Vector2(Input.mousePosition.x, transform.position.y);
            }
            if (Input.GetMouseButtonUp(0))
            {
                UnGrabBall();

                AddNewBall();
            }
        }
    }


    // La balle se décroche du Grab et tombe
    void UnGrabBall()
    {
        actualBall.GetComponent<BallBehaviours>().SetIsDone();
        actualBall.transform.SetParent(GameObject.Find("Canvas").transform);
        actualBall.GetComponent<Rigidbody2D>().gravityScale = 3;
        actualBall.GetComponent<Rigidbody2D>().AddTorque(1500, ForceMode2D.Impulse);
    }


    // Remise d'une balle en jeu jusqu'à cours de stock
    void AddNewBall()
    {
        cptBall++;
        if (cptBall < gameController.letters.Length)
        {
            StartCoroutine(NewBall());
        }
    }


    // Appel de la méthode CreateBall dans le script LettersAndWords
    IEnumerator NewBall()
    {
        yield return new WaitForSeconds(1.5f);

        gameController.CreateBall();
    }
}
