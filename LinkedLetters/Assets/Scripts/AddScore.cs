using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddScore : MonoBehaviour
{
    // Déclaration des variables
    // ====================================
    public GameObject ptc_etoile;
    public GameObject SoundBling;
    // ====================================


    /// <summary>
    /// <para>Ajoute du score à l'UI txtScore</para>
    /// </summary>
    /// <param name="score">Détermine le score à ajouter à l'UI txtScore</param>
    public void CreateScore(float score)
    {
        StartCoroutine(AddTempoScore(score));
    }


    /// <summary>
    /// <para>Ajoute le score 1 à 1 pour sensation de fluidité et satisfaction</para>
    /// </summary>
    IEnumerator AddTempoScore(float score)
    {
        yield return new WaitForSeconds((float)0.04);

        if (score > 0)
        {
            GetComponent<Text>().text = (int.Parse(GetComponent<Text>().text) + 1).ToString();
            score--;

            // Tout les 20 points de score ajoutés, instanciation de particules d'étoile et son de "bling"
            if (score % 20 == 0)
            {
                GameObject instancePtc;
                instancePtc = Instantiate(ptc_etoile, transform.position, transform.rotation);

                GameObject bling;
                bling = Instantiate(SoundBling, transform.position, transform.rotation);
            }
        }
        else if (score < 0)
        {
            GetComponent<Text>().text = (int.Parse(GetComponent<Text>().text) - 1).ToString();
            score++;
        }

        if (!(score == 0))
        {
            StartCoroutine(AddTempoScore(score));
        }
    }
}
