using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// BUT : Bouton permettant de restart le level en cours
public class ReloadScene : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
    	if(Input.touchCount > 0)
    	{
    		Touch touch = Input.GetTouch(0);

    		if(touch.position.x <= transform.position.x + 50 && touch.position.x >= transform.position.x - 50 && touch.position.y <= transform.position.y + 30 && touch.position.y >= transform.position.y - 30 && touch.phase == TouchPhase.Ended)
    		{
                StartCoroutine(Clic());
    		}
    	}

        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.x <= transform.position.x + 50 && Input.mousePosition.x >= transform.position.x - 50 && Input.mousePosition.y <= transform.position.y + 30 && Input.mousePosition.y >= transform.position.y - 30)
            {
                StartCoroutine(Clic());
            }
        }
    }

    /// <summary>
    /// <para>Diminution du bouton RESTART, fondu en noir puis chargement de la scène de jeu actuelle</para>
    /// </summary>
    /// <returns></returns>
    IEnumerator Clic()
    {
        LeanTween.scale(gameObject, new Vector3(0, 0, 1), 2);
        yield return new WaitForSeconds(2);

        //Pour que ce transform suivant soit à la fin de la liste de son parent : ainsi le rideau est le dernier UI à être rendu 
        GameObject.Find("rideau").transform.SetAsLastSibling();

        GameObject.Find("rideau").GetComponent<FadeCurtain>().FadeToMax(false);
        yield return new WaitForSeconds(2);

        string scene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
