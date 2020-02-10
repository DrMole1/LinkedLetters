using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// BUT : Se diriger vers un level en cliquant sur une icone dédiée dans le menu

public class ClicRoom : MonoBehaviour
{
	const int DISTANCE = 40;


    //Déclaration variables
    //==================================
    public string room;
    //==================================

	void Update()
	{
		if(Input.touchCount > 0)
    	{
    		Touch touch = Input.GetTouch(0);

    		if(touch.position.x <= transform.position.x + DISTANCE && touch.position.x >= transform.position.x - DISTANCE && touch.position.y <= transform.position.y + DISTANCE && touch.position.y >= transform.position.y - DISTANCE && touch.phase == TouchPhase.Ended)
    		{
    			StartCoroutine(Clic());
    		}
    	}

        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.x <= transform.position.x + DISTANCE && Input.mousePosition.x >= transform.position.x - DISTANCE && Input.mousePosition.y <= transform.position.y + DISTANCE && Input.mousePosition.y >= transform.position.y - DISTANCE)
            {
                StartCoroutine(Clic());
            }
        }

    }

    /// <summary>
    /// <para>Fondu en noir puis chargement de la scène du menu</para>
    /// </summary>
    /// <returns></returns>
    IEnumerator Clic()
    {
        GameObject.Find("rideau").GetComponent<FadeCurtain>().FadeToMax(false);
        yield return new WaitForSeconds(2);
   	   	SceneManager.LoadScene(room, LoadSceneMode.Single);
   	}

}
