using System.Collections;
using UnityEngine;
using UnityEngine.Windows;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType(typeof(GameManager)) as GameManager;
                if (_instance == null)
                {
                    Debug.LogError("GameManger not found in the scene.");
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(this);
        }
    }

    public void BulletTime(float force, float seconds)
    {
        StartCoroutine(TimeSlow(force, seconds));
    }

    IEnumerator TimeSlow(float value, float seconds) 
    {
        Time.timeScale = value;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        yield return new WaitForSecondsRealtime(seconds);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }
}
