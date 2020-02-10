using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//BUT : Pour éviter que les particules restent sur la scene du projet et prennent de la ressource ! Eviter les lags et augmenter la performance
//ENTREE : La particule auquel est attaché le script
//SORTIE : Destruction de l'object
//NOTE : Utilisation de la méthode Destroy(GameObject, float)

public class DestroyParticles : MonoBehaviour
{
    float DestructTime = 5f;

    void Start()
    {
        Destroy(gameObject, DestructTime);
    }
}
