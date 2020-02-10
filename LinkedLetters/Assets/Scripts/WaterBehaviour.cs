using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBehaviour : MonoBehaviour
{
    public GameObject prefabEauPtc;
    public GameObject soundWater;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "color1" || other.gameObject.tag == "color2" || other.gameObject.tag == "color3" || other.gameObject.tag == "color4" || other.gameObject.tag == "color5")
        {
            GameObject instanceEauPtc;
            instanceEauPtc = Instantiate(prefabEauPtc, other.gameObject.transform.position, Quaternion.identity);

            GameObject instanceSoundWater;
            instanceSoundWater = Instantiate(soundWater, other.gameObject.transform.position, Quaternion.identity);
        }
    }
}
