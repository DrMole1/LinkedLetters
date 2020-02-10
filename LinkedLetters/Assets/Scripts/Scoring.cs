using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// BUT : S'occupe du scoring et des comportements des balles de score
public class Scoring : MonoBehaviour
{
	const int DISTANCE = 30;

	//Déclaration des variables
	//============================================
	public GameObject ptc;
	public GameObject SoundDestruct;

	GameObject txtScore;
	float score;
	float scale;
	//============================================

	void Awake()
	{
		txtScore = GameObject.Find("Score");
	}


	public void SetScore(float number)
	{
		score = number;
	}

	public void SetScale(float number)
	{
		scale = number;
	}


    void Start()
    {
        transform.localScale = new Vector2(transform.localScale.x * scale, transform.localScale.y * scale);
    }

    
    void Update()
    {
        // Enlever les commentaires pour portage mobile
        //ExplodeBubleMobile();

        if (Input.mousePresent)
            ExplodeBubleStandalone();
    }


    // Permet d'éclater les bulles sur mobile
    void ExplodeBubleMobile()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.position.x <= transform.position.x + DISTANCE * scale && touch.position.x >= transform.position.x - DISTANCE * scale && touch.position.y <= transform.position.y + DISTANCE * scale && touch.position.y >= transform.position.y - DISTANCE * scale && touch.phase == TouchPhase.Ended)
            {
                AddScore();
            }
        }
    }


    // Permet d'éclater les bulles sur pc
    void ExplodeBubleStandalone()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.x <= transform.position.x + DISTANCE * scale && Input.mousePosition.x >= transform.position.x - DISTANCE * scale && Input.mousePosition.y <= transform.position.y + DISTANCE * scale && Input.mousePosition.y >= transform.position.y - DISTANCE * scale)
            {
                AddScore();
            }
        }
    }


    /// <summary>
    /// <para>Ajoute du score en appelant une fonction contenu dans le script AddScore.cs</para>
    /// </summary>
    public void AddScore()
    {
        txtScore.GetComponent<AddScore>().CreateScore(score);

        GameObject instancePtc;
        instancePtc = Instantiate(ptc, transform.position, transform.rotation);

        GameObject explode;
        explode = Instantiate(SoundDestruct, transform.position, transform.rotation);

    	Destroy(gameObject);
    }
}
