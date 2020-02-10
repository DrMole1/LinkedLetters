using UnityEngine;

public class TimeManager : MonoBehaviour
{

    public float slowDownFactor = 0.05f;
    public float slowDownLength = 7f;


    void Update()
    {
        Time.timeScale += (1f / slowDownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }

    public void DoSlowMotion()
    {
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    public void StopSlowMotion()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}
