using UnityEngine;

public class InputManager : MonoBehaviour
{
    static InputManager _instance;
    public static InputManager Instance 
    {
        get 
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType(typeof(InputManager)) as InputManager;
                if (_instance == null)
                {
                    Debug.LogError("InputManager not found in the scene.");
                }
            }
            return _instance;
        }
    }

    public PlayerInput input;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        input = new PlayerInput();
        input.Enable();
    }
}
