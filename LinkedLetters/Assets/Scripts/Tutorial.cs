using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.UI;

// Script utilisé uniquement dans le level 1 pour apprendre au joueur les mécaniques de jeu
public class Tutorial : MonoBehaviour
{
    // Déclaration des variables
    // =========================================
    public GameObject lightGrab;
    public GameObject ptc_ArrowLeft;
    public GameObject ptc_ArrowRight;
    public GameObject pannelTuto;
    public GameObject pannelTuto2;
    public GameObject txtPig;

    int cpt = 0;
    bool stop = false;
    // =========================================




    void Start()
    {
        StartCoroutine(CreateParticleGrab());

        CreatePannelTuto();
    }


    void Update()
    {
        TouchPannel();

        if(txtPig.GetComponent<Text>().text == "PIG" && stop == false)
        {
            stop = true;
            CreatePannelTuto2();
        }
    }


    // Feedback pour indiquer au joueur qu'il faut manipuler le grab
    IEnumerator CreateParticleGrab()
    {
        yield return new WaitForSeconds(10);

        //Instantiation des particules du grab
        GameObject instanceLeft;
        GameObject instanceRight;
        instanceLeft = Instantiate(ptc_ArrowLeft, new Vector2(lightGrab.transform.position.x, lightGrab.transform.position.y - 100), Quaternion.identity);
        instanceRight = Instantiate(ptc_ArrowRight, new Vector2(lightGrab.transform.position.x, lightGrab.transform.position.y - 100), Quaternion.identity);

        LeanTween.alpha(instanceLeft, 0f, 4f);
        LeanTween.alpha(instanceRight, 0f, 4f);

        LeanTween.moveX(instanceLeft, instanceLeft.transform.position.x - 140, 4f);
        LeanTween.moveX(instanceRight, instanceRight.transform.position.x + 140, 4f);

        StartCoroutine(MoreLight(lightGrab, cpt));
    }


    // Augmente la luminosité de l'objet progressivement
    IEnumerator MoreLight(GameObject myObject, int i)
    {
        myObject.GetComponent<Light2D>().intensity += 0.1f;

        yield return new WaitForSeconds((float)0.04f);

        if(myObject.GetComponent<Light2D>().intensity >= 5)
        {
            StartCoroutine(LessLight(myObject, i));
            cpt++;
        }
        else
        {
            StartCoroutine(MoreLight(myObject, i));
        }
    }


    // Baisse la luminosité de l'objet progressivement
    IEnumerator LessLight(GameObject myObject, int i)
    {
        myObject.GetComponent<Light2D>().intensity -= 0.1f;

        yield return new WaitForSeconds((float)0.04f);

        if (myObject.GetComponent<Light2D>().intensity <= 0.5f && cpt < 3)
        {
            StartCoroutine(MoreLight(myObject, i));
        }
        else if (myObject.GetComponent<Light2D>().intensity > 0.5f)
        {
            StartCoroutine(LessLight(myObject, i));
        }
    }


    // Met en place le Tuto 1
    void CreatePannelTuto()
    {
        pannelTuto.transform.position = new Vector2(590, 200);
        LeanTween.scale(pannelTuto, new Vector3(6f, 3f, 1f), 2);
    }


    // Met en place le Tuto 2
    void CreatePannelTuto2()
    {
        pannelTuto2.transform.SetParent(GameObject.Find("Canvas").transform);
        pannelTuto2.transform.position = new Vector2(590, 200);
        LeanTween.scale(pannelTuto2, new Vector3(6f, 3f, 1f), 2);
    }


    // Si un panneau est touché, de manière tactile ou par la souris
    void TouchPannel()
    {
        // Manière tactile

        if (GameObject.Find("pannelTuto") != null && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended && touch.position.x < pannelTuto.transform.position.x + 265 && touch.position.x > pannelTuto.transform.position.x - 265 && touch.position.y < pannelTuto.transform.position.y + 135 && touch.position.y > pannelTuto.transform.position.y - 135)
            {
                LeanTween.scale(pannelTuto, new Vector3(0f, 0f, 1f), 2);
                Destroy(pannelTuto, 2f);
            }
        }

        if (GameObject.Find("pannelTuto2") != null && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended && touch.position.x < pannelTuto2.transform.position.x + 265 && touch.position.x > pannelTuto2.transform.position.x - 265 && touch.position.y < pannelTuto2.transform.position.y + 135 && touch.position.y > pannelTuto2.transform.position.y - 135)
            {
                LeanTween.scale(pannelTuto2, new Vector3(0f, 0f, 1f), 2);
                Destroy(pannelTuto2, 2f);
            }
        }

        // A la souris

        if (Input.GetMouseButtonDown(0) && GameObject.Find("pannelTuto") != null)
        {
            if (Input.mousePosition.x <= pannelTuto.transform.position.x + 265 && Input.mousePosition.x >= pannelTuto.transform.position.x - 265 && Input.mousePosition.y <= pannelTuto.transform.position.y + 135 && Input.mousePosition.y >= pannelTuto.transform.position.y - 135)
            {
                LeanTween.scale(pannelTuto, new Vector3(0f, 0f, 1f), 2);
                Destroy(pannelTuto, 2f);
            }
        }

        if (Input.GetMouseButtonDown(0) && GameObject.Find("pannelTuto2") != null)
        {
            if (Input.mousePosition.x <= pannelTuto2.transform.position.x + 265 && Input.mousePosition.x >= pannelTuto2.transform.position.x - 265 && Input.mousePosition.y <= pannelTuto2.transform.position.y + 135 && Input.mousePosition.y >= pannelTuto2.transform.position.y - 135)
            {
                LeanTween.scale(pannelTuto2, new Vector3(0f, 0f, 1f), 2);
                Destroy(pannelTuto2, 2f);
            }
        }
    }
}
