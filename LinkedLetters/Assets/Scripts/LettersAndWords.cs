using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// BUT : Renseigne quels mots doivent être écrits, ainsi que l'ordre de passage des lettres

public class LettersAndWords : MonoBehaviour
{
	const float DISTANCEX = 3.5f;
	const int DISTANCEY = 132;

	//Déclaration des variables
	//==========================================
	[Header("-----Puzzle Design-----")]
	public char[] letters;
	public string[] words;
	public Color[] maxcolors;
	public Color[] colors;
	public string[] frenchwords;
	public GameObject[] balls;

	[Header("-----Prefab-----")]
	public GameObject ballPrefab;
	public GameObject Grab;
	public GameObject ptcDestroy;
	public GameObject victory;
	public GameObject end;
	public GameObject SoundDestruct;
	public GameObject txtScore;
	public GameObject bonus;
	public GameObject malus;
    public GameObject ptc_etoile;
    public GameObject floatingText;

	int cptBall = 0;
	int[] ballsAtWord = {0,0,0,0,0,0,0};
	int cptFinishedWords = 0;
    bool[] stop = { false, false, false, false, false };
	//==========================================


	void Start()
	{
		CreateBall();
		SetFrenchWords();
		SetEnglishWords();
        StartCoroutine(ActiveScore());
	}


	// BUT : Ajoute 1 au compteur de balle, pour savoir quelle balle est en cours
    public void AddCptBall()
    {
    	cptBall ++;
    }


    /// <summary>
    /// <para>Méthode permettant d'instancier un objet de classe ball au niveau du grab ainsi que de lui assigner un tag</para>
    /// </summary>
    public void CreateBall()
    {
    	GameObject instanceBall;
       	instanceBall = Instantiate(ballPrefab, new Vector2(Grab.transform.position.x - DISTANCEX, Grab.transform.position.y - DISTANCEY), transform.rotation);
       	Grab.GetComponent<GrabBehaviours>().actualBall = instanceBall;
       	instanceBall.transform.SetParent(Grab.transform);
       	instanceBall.GetComponent<BallBehaviours>().SetLetter(letters[cptBall]);
       	instanceBall.GetComponent<BallBehaviours>().SetColor(colors[cptBall]);
       	instanceBall.transform.name = "ball";

       	//On donne un tag à l'instance
       	int nColor = 4;

       	for(int i = 0; i < maxcolors.Length; i++)
       	{
       		if(maxcolors[i] == instanceBall.GetComponent<BallBehaviours>().GetColor())
       		{
       			nColor = i;
       		}
       	}
       	if(nColor == 0)
       	{
       		instanceBall.tag = "color1";
       	}
       	else if(nColor == 1)
       	{
       		instanceBall.tag = "color2";
       	}
       	else if(nColor == 2)
       	{
       		instanceBall.tag = "color3";
       	}
       	else if(nColor == 3)
       	{
       		instanceBall.tag = "color4";
       	}
       	else if(nColor == 4)
       	{
       		instanceBall.tag = "color5";
       	}


       	AddCptBall();
    }


    // BUT : Mettre en place les mots français qu'il faudra retrouver
    public void SetFrenchWords()
    {
    	GameObject.Find("Mot1").GetComponent<Text>().text = frenchwords[0];
    	GameObject.Find("Mot2").GetComponent<Text>().text = frenchwords[1];
    	GameObject.Find("Mot3").GetComponent<Text>().text = frenchwords[2];
    	GameObject.Find("Mot4").GetComponent<Text>().text = frenchwords[3];
    	GameObject.Find("Mot5").GetComponent<Text>().text = frenchwords[4];
    }


    // BUT : Mettre en place les mots anglais
    public void SetEnglishWords()
    {
    	GameObject.Find("Traduction1").GetComponent<Text>().text = "";
    	GameObject.Find("Traduction2").GetComponent<Text>().text = "";
    	GameObject.Find("Traduction3").GetComponent<Text>().text = "";
    	GameObject.Find("Traduction4").GetComponent<Text>().text = "";
    	GameObject.Find("Traduction5").GetComponent<Text>().text = "";
    }


    // BUT : Set les compteurs de balles 
    public void SetBallsAtWord(Color color, int nValue)
    {
    	int nIndex = 4;
    	for(int i = 0; i < maxcolors.Length; i ++)
    	{
    		if(maxcolors[i] == color)
    		{
    			nIndex = i;
    		}
    	}

    	ballsAtWord[nIndex] += nValue;

    	CheckBalls(nIndex);
    }


    // BUT : Si toutes les balles d'une couleur possèdent minimum un contact ET elles ne forment aucun groupe de 2, alors destruction
    public void CheckBalls(int nIndex)
    {
    	if(ballsAtWord[nIndex] == words[nIndex].Length)
    	{
    		int nBallWithOneContact = 0;
    		string tag = "";

    		if(nIndex == 0)
    		{
    			tag = "color1";
    		}
    		else if(nIndex == 1)
    		{
    			tag = "color2";
    		}
    		else if(nIndex == 2)
    		{
    			tag = "color3";
    		}
    		else if(nIndex == 3)
    		{
    			tag = "color4";
    		}
    		else if(nIndex == 4)
    		{
    			tag = "color5";
    		}

    		balls = GameObject.FindGameObjectsWithTag(tag);

    		for(int i = 0; i < balls.Length; i++)
    		{
    			if(balls[i].GetComponent<BallBehaviours>().GetContact() == 1)
    			{
    				nBallWithOneContact++;
    			}
    		}

    		if(nBallWithOneContact <= 2 && words[nIndex].Length <= 5)
    		{
    			//Destruction des balles pour former le mot
    			ExplodeBall(balls, nIndex);
    			txtScore.GetComponent<Text>().text = (int.Parse(txtScore.GetComponent<Text>().text) + 25).ToString();

                GameObject instancePtcEtoile;
                instancePtcEtoile = Instantiate(ptc_etoile, txtScore.transform.position, txtScore.transform.rotation);
            }
    		if(nBallWithOneContact <= 4 && words[nIndex].Length <= 8)
    		{
    			//Destruction des balles pour former le mot
    			ExplodeBall(balls, nIndex);
    			txtScore.GetComponent<Text>().text = (int.Parse(txtScore.GetComponent<Text>().text) + 25).ToString();

                GameObject instancePtcEtoile;
                instancePtcEtoile = Instantiate(ptc_etoile, txtScore.transform.position, txtScore.transform.rotation);
            }
    		if(nBallWithOneContact <= 6 && words[nIndex].Length <= 11)
    		{
    			//Destruction des balles pour former le mot
    			ExplodeBall(balls, nIndex);
    			txtScore.GetComponent<Text>().text = (int.Parse(txtScore.GetComponent<Text>().text) + 25).ToString();

                GameObject instancePtcEtoile;
                instancePtcEtoile = Instantiate(ptc_etoile, txtScore.transform.position, txtScore.transform.rotation);
            }
    	}
    }


    // BUT : Lorsque toutes les balles sont réunies dans la configuration, révèle le mot anglais, éclosion de particules puis destructions
    public void ExplodeBall(GameObject[] balls, int nIndex)
    {
    	if(nIndex == 0 && stop[0] == false)
    	{
    		GameObject.Find("Traduction1").GetComponent<Text>().text = words[0];
    		GameObject.Find("Traduction1").GetComponent<Text>().color = maxcolors[0];
            cptFinishedWords++;
            stop[0] = true;
            CreateFloatingText(GameObject.Find("Traduction1").transform);
        }
    	else if(nIndex == 1 && stop[1] == false)
    	{
    		GameObject.Find("Traduction2").GetComponent<Text>().text = words[1];
    		GameObject.Find("Traduction2").GetComponent<Text>().color = maxcolors[1];
            cptFinishedWords++;
            stop[1] = true;
            CreateFloatingText(GameObject.Find("Traduction2").transform);
        }
    	else if(nIndex == 2 && stop[2] == false)
    	{
    		GameObject.Find("Traduction3").GetComponent<Text>().text = words[2];
    		GameObject.Find("Traduction3").GetComponent<Text>().color = maxcolors[2];
            cptFinishedWords++;
            stop[2] = true;
            CreateFloatingText(GameObject.Find("Traduction3").transform);
        }
    	else if(nIndex == 3 && stop[3] == false)
    	{
    		GameObject.Find("Traduction4").GetComponent<Text>().text = words[3];
    		GameObject.Find("Traduction4").GetComponent<Text>().color = maxcolors[3];
            cptFinishedWords++;
            stop[3] = true;
            CreateFloatingText(GameObject.Find("Traduction4").transform);
        }
    	else if(nIndex == 4 && stop[4] == false)
    	{
    		GameObject.Find("Traduction5").GetComponent<Text>().text = words[4];
    		GameObject.Find("Traduction5").GetComponent<Text>().color = maxcolors[4];
            cptFinishedWords++;
            stop[4] = true;
            CreateFloatingText(GameObject.Find("Traduction5").transform);
        }

    	for(int i = 0; i < balls.Length; i++)
    	{
    		//Création aléatoire de bonus
    		float random = 0;
    		random = Random.Range(0.0f, 10.0f);
    		if(random < 1.5f)
    		{
    			CreateBonus(balls[i].transform);
    		}
    		else if(random > 9f)
    		{
    			CreateMalus(balls[i].transform);
    		}

            // Instanciation de particules de destruction
    		GameObject instancePtc;
        	instancePtc = Instantiate(ptcDestroy, balls[i].transform.position, transform.rotation);

            // Instanciation son de destruction
        	GameObject explode;
        	explode = Instantiate(SoundDestruct, balls[i].transform.position, transform.rotation);

            // Particules de destruction prennent la couleur de la balle
        	ParticleSystem ps = instancePtc.GetComponent<ParticleSystem>();
        	var main = ps.main;
			main.startColor = maxcolors[nIndex];

    		Destroy(balls[i]);
    	}


    	if(cptFinishedWords >= 5)
    	{
    		Victory();
    	}
    }


    // BUT : Feedback de victoire
    public void Victory()
    {
    	victory.transform.position = new Vector2(590,260);
        LeanTween.scale(victory, new Vector3(8, 8, 1), 2);
        LeanTween.moveY(end, 100, 2);
        Destroy(Grab);

        //Destruction des bulles bonus/malus
        Destroy(GameObject.Find("poolBonus"));
        Destroy(GameObject.Find("poolMalus"));

        //Affichage du score
        LeanTween.move(txtScore, new Vector2(610, 450), 2);
        StartCoroutine(MakeStars());
    }




    // BUT : Créer un bonus
    public void CreateBonus(Transform balltransform)
    {
    	float random = 0;
    	random = Random.Range(1.0f, 1.8f);

    	float score = Mathf.Ceil((float)random * 75.0f);
    	GameObject instanceBonus;
       	instanceBonus = Instantiate(bonus, balltransform.position, transform.rotation);
       	instanceBonus.GetComponent<Scoring>().SetScore(score);
       	instanceBonus.GetComponent<Scoring>().SetScale(random);
        instanceBonus.transform.SetParent(GameObject.Find("poolBonus").transform);
    }


    // BUT : Créer un malus
    public void CreateMalus(Transform balltransform)
    {
        float random = 0;
        random = Random.Range(1.0f, 1.8f);

        float score = Mathf.Ceil((float)random * -75.0f);
    	GameObject instanceMalus;
       	instanceMalus = Instantiate(malus, balltransform.position, transform.rotation);
       	instanceMalus.GetComponent<Scoring>().SetScore(score);
       	instanceMalus.GetComponent<Scoring>().SetScale(random);
        instanceMalus.transform.SetParent(GameObject.Find("poolMalus").transform);
    }


    IEnumerator MakeStars()
    {
        GameObject instancePtcEtoile;
        instancePtcEtoile = Instantiate(ptc_etoile, txtScore.transform.position, txtScore.transform.rotation);
        instancePtcEtoile.transform.SetParent(GameObject.Find("Canvas").transform);

        yield return new WaitForSeconds((float)0.5);

        StartCoroutine(MakeStars());
    }

    IEnumerator ActiveScore()
    {
        yield return new WaitForSeconds(2);
        txtScore.SetActive(true);
    }


    // Création d'une particule étoile à la révélation du mot
    void CreateFloatingText(Transform txtTransform)
    {
        //Instantiation de l'objet
        GameObject instancePtcEtoile;
        instancePtcEtoile = Instantiate(ptc_etoile, txtTransform.position, txtTransform.rotation);
    }
}