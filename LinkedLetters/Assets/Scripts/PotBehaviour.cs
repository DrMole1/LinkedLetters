using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotBehaviour : MonoBehaviour
{
    public GameObject potBroken;
    public GameObject soundPotBroken;
    public GameObject dirtPrefab;

    bool isBroken = false;
    GameObject potA;
    GameObject potB;
    GameObject potC;
    GameObject potD;

    void Start()
    {
        
    }

    void Update()
    {

    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isBroken == false && other.gameObject.tag == "color1" || other.gameObject.tag == "color2" || other.gameObject.tag == "color3" || other.gameObject.tag == "color4" || other.gameObject.tag == "color5")
        {
            GameObject potBrokenInstance;
            potBrokenInstance = Instantiate(potBroken, new Vector2(transform.position.x, transform.position.y + 30), Quaternion.identity);

            GameObject soundPotBrokenInstance;
            soundPotBrokenInstance = Instantiate(soundPotBroken, new Vector2(transform.position.x, transform.position.y + 30), Quaternion.identity);

            GameObject dirtInstance;
            dirtInstance = Instantiate(dirtPrefab, new Vector2(transform.position.x, transform.position.y - 30), Quaternion.identity);

            potA = potBrokenInstance.transform.GetChild(0).gameObject;
            potB = potBrokenInstance.transform.GetChild(1).gameObject;
            potC = potBrokenInstance.transform.GetChild(2).gameObject;
            potD = potBrokenInstance.transform.GetChild(3).gameObject;

            potA.transform.position = new Vector3(potA.transform.position.x, potA.transform.position.y, 1);
            potB.transform.position = new Vector3(potB.transform.position.x, potB.transform.position.y, 1);
            potC.transform.position = new Vector3(potC.transform.position.x, potC.transform.position.y, 1);
            potD.transform.position = new Vector3(potD.transform.position.x, potD.transform.position.y, 1);

            potA.transform.SetParent(GameObject.Find("Canvas").transform);
            potB.transform.SetParent(GameObject.Find("Canvas").transform);
            potC.transform.SetParent(GameObject.Find("Canvas").transform);
            potD.transform.SetParent(GameObject.Find("Canvas").transform);

            potA.GetComponent<Rigidbody2D>().gravityScale = 5;
            potB.GetComponent<Rigidbody2D>().gravityScale = 5;
            potC.GetComponent<Rigidbody2D>().gravityScale = 5;
            potD.GetComponent<Rigidbody2D>().gravityScale = 5;

            Destroy(gameObject);
        }
    }
}
