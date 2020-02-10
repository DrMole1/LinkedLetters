using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// BUT : Se rendre au menu après une victoire
public class ClicEnd : MonoBehaviour
{
    const int BTNDISTANCE = 90;

    void Update()
	{
		if(Input.touchCount > 0)
    	{
    		Touch touch = Input.GetTouch(0);

    		if(touch.position.x <= transform.position.x + BTNDISTANCE && touch.position.x >= transform.position.x - BTNDISTANCE && touch.position.y <= transform.position.y + BTNDISTANCE && touch.position.y >= transform.position.y - BTNDISTANCE && touch.phase == TouchPhase.Ended)
    		{
                StartCoroutine(Clic());
            }
    	}

        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.x <= transform.position.x + BTNDISTANCE && Input.mousePosition.x >= transform.position.x - BTNDISTANCE && Input.mousePosition.y <= transform.position.y + BTNDISTANCE && Input.mousePosition.y >= transform.position.y - BTNDISTANCE)
            {
                StartCoroutine(Clic());
            }
        }
    }


    /// <summary>
    /// <para>Diminution du bouton END, fondu en noir puis chargement de la scène du menu</para>
    /// </summary>
    /// <returns></returns>
    IEnumerator Clic()
    {
        LeanTween.scale(gameObject, new Vector3(0, 0, 1), 2);
        yield return new WaitForSeconds(2);
        GameObject.Find("rideau").GetComponent<FadeCurtain>().FadeToMax(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
