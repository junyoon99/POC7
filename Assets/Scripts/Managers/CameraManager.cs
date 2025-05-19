using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    static CameraManager _instance;
    public static CameraManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType(typeof(CameraManager)) as CameraManager;
                if (_instance == null)
                {
                    Debug.LogError("CameraManger not found in the scene.");
                }
            }
            return _instance;
        }
    }

    public CinemachineCamera Camera;

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

    private void Start()
    {
        print(UnityEngine.Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera);
        Camera = UnityEngine.Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera as CinemachineCamera;
    }

    public void ChangeTarget(Transform target)
    {
        Camera.Follow = target;
    }

    public void CloseUp(float force, float time)
    {
        StartCoroutine(Closing(force, time));
    }

    IEnumerator Closing(float force, float time)
    {
        float DefalutValue = Camera.Lens.FieldOfView;
        Camera.Lens.FieldOfView = force;
        yield return new WaitForSecondsRealtime(time);
        Camera.Lens.FieldOfView = DefalutValue;
    }
}
