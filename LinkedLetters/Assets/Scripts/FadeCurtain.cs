using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// BUT : Gestion du rideau pour la transition entre les scènes
public class FadeCurtain : MonoBehaviour
{


    void Awake()
    {
        
    }

    void Start()
    {
        transform.position = new Vector2(510, 260);
        FadeToMin();
    }


    /// <summary>
    /// <para>L'opacité du rideau augmente jusqu'à 1, rendu final sombre</para>
    /// </summary>
    /// <returns></returns>
    public void FadeToMax(bool isEnd)
    {
        if (isEnd == false)
            transform.SetAsLastSibling();

        LeanTween.alpha(gameObject.GetComponent<RectTransform>(), 1f, 2f);
    }


    /// <summary>
    /// <para>L'opacité du rideau diminue jusqu'à 0, rendu final transparent</para>
    /// </summary>
    /// <returns></returns>
    public void FadeToMin()
    {
        LeanTween.alpha(gameObject.GetComponent<RectTransform>(), 0f, 2f);
    }
}
