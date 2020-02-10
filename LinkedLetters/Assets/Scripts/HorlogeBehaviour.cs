using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorlogeBehaviour : MonoBehaviour
{

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.GetChild(0).transform.Rotate(0, 0, -0.15f, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "color1" || other.gameObject.tag == "color2" || other.gameObject.tag == "color3" || other.gameObject.tag == "color4" || other.gameObject.tag == "color5")
        {
            GameObject.Find("TimeManager").GetComponent<TimeManager>().DoSlowMotion();

            Camera.main.GetComponent<StressReceiver>().InduceStress(1);

            StartCoroutine(FadeCurtainShake());
        }
    }


    #region FadeCurtain
    IEnumerator FadeCurtainShake()
    {

        LeanTween.alpha(GameObject.Find("rideau").GetComponent<RectTransform>(), 0.6f, 0.6f);

        yield return new WaitForSeconds(0.6f);

        LeanTween.alpha(GameObject.Find("rideau").GetComponent<RectTransform>(), 0f, 0.6f);
    }
    #endregion

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.gameObject.tag == "color1" || other.gameObject.tag == "color2" || other.gameObject.tag == "color3" || other.gameObject.tag == "color4" || other.gameObject.tag == "color5")
    //    {
    //        GameObject.Find("TimeManager").GetComponent<TimeManager>().StopSlowMotion();
    //    }
    //}
}
