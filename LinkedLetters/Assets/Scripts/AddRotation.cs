using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//BUT : Faire tourner un objet de façon constante

public class AddRotation : MonoBehaviour
{
	//Déclaration variables
	//===============================
	public float speed;
	//===============================

    void Update()
    {
        transform.Rotate(0, 0, speed, Space.Self);
    }
}


