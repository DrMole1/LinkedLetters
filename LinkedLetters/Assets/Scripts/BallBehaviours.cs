using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.UI;

// BUT : Régit les comportements d'une balle

public class BallBehaviours : MonoBehaviour
{

	//Déclaration des variables
	//============================================
	public GameObject ptc;

	bool isDone = false;
	int nContact = 0;
	char letter;
	Color color;
	GameObject light;
	GameObject text;
	LettersAndWords gameController;
	AudioSource audioData;

	[Header("-----Clips-----")]
	public AudioClip start;
	//============================================

	void Awake()
	{
		gameController = GameObject.Find("GameController").GetComponent<LettersAndWords>();
		audioData = GetComponent<AudioSource>();
	}


	public bool GetIsDone()
	{
		return isDone;
	}

	public void SetIsDone()
	{
		isDone = true;
	}

	public char GetLetter()
	{
		return letter;
	}

	public void SetLetter(char newLetter)
	{
		letter = newLetter;
	}

	public Color GetColor()
	{
		return color;
	}

	public void SetColor(Color newColor)
	{
		color = newColor;
	}

	public int GetContact()
	{
		return nContact;
	}


	void Start()
	{
		StartCoroutine(InitBall());
	}


	// BUT : Initialisation de la balle et éclosion de particules
	IEnumerator InitBall()
    {
    	yield return new WaitForSeconds(0.5f);

    	// Mise en place de la couleur de lumière et de la lettre
    	light = this.gameObject.transform.GetChild(0).gameObject;
    	text = this.gameObject.transform.GetChild(1).gameObject;
    	light.GetComponent<Light2D>().color = color;
    	text.GetComponent<Text>().text = letter + " ";

    	// Eclosion de particule de la même couleur que la lumière émise
    	GameObject instancePlop;
        instancePlop = Instantiate(ptc, transform.position, transform.rotation);
        ParticleSystem ps = instancePlop.GetComponent<ParticleSystem>();
        var main = ps.main;
		main.startColor = color;

		//Audio start
		audioData.clip = start;
		audioData.Play(0);
    }


    // BUT : Quand une balle touche une balle de la même couleur
    void OnCollisionEnter2D(Collision2D other) 
    {
       	if (other.gameObject.CompareTag(gameObject.tag))
        {
    		nContact++;
        	if(nContact == 1)
        	{
        		gameController.SetBallsAtWord(color, 1);
        	}
        }
    }


    // BUT : Quand une balle ne touche plus une balle de la même couleur
    void OnCollisionExit2D(Collision2D other) 
    {
       	if (other.gameObject.CompareTag(gameObject.tag))
        {
        	nContact--;
        	if(nContact == 0)
        	{
        		gameController.SetBallsAtWord(color, -1);
        	}
        }
    }
}
