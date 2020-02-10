using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.SceneManagement;

public class SpawnerThunder : MonoBehaviour
{
	public GameObject eclair;
	public GameObject aura;

    int cpt = 0;

	void Start()
	{
		StartCoroutine(SpawnEclair());
	}


    // Spawn de l'éclair
    IEnumerator SpawnEclair()
    {
        cpt++;
        int posX = 0;

        if (cpt == 1)
        {
            yield return new WaitForSeconds(1.5f);
            posX = 300;
        }
        else
        {
            yield return new WaitForSeconds(1f);
            posX = 750;
        }




    	GameObject instanceEclair;
        instanceEclair = Instantiate(eclair, new Vector2(posX, 253), transform.rotation);

        // Fait spawn l'aura
        GameObject instanceAura;
        instanceAura = Instantiate(aura, new Vector2(posX, 253), transform.rotation);

        yield return new WaitForSeconds((float)0.04);

        Destroy(instanceEclair);

        yield return new WaitForSeconds((float)0.04);

        GameObject instanceEclair2;
        instanceEclair2 = Instantiate(eclair, new Vector2(posX, 253), transform.rotation);

        yield return new WaitForSeconds((float)0.04);

        Destroy(instanceEclair2);

        yield return new WaitForSeconds((float)0.3);

       	StartCoroutine(LessAura(instanceAura));

        if (cpt == 1)
        {
            StartCoroutine(SpawnEclair());
        }
        else
        {
            yield return new WaitForSeconds(6f);
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }

    // Diminution progressive de l'intensité de l'aura
    IEnumerator LessAura(GameObject instanceAura)
    {
    	instanceAura.GetComponent<Light2D>().intensity -= (float)0.05;

    	yield return new WaitForSeconds((float)0.09);

    	StartCoroutine(LessAura(instanceAura));
    }
}
