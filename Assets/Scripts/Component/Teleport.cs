using UnityEngine;
using UnityEngine.InputSystem;

public class Teleport : MonoBehaviour
{
    public TeleportObject teleportObject;


    private void Start()
    {
        teleportObject = FindAnyObjectByType<TeleportObject>();
        InputManager.Instance.input.Ingame.Space.started += TeleportStart;
    }

    void TeleportStart(InputAction.CallbackContext ctx) 
    {
        if(teleportObject != null)
        {
            teleportObject.Teleport(transform);
        }
    }
}
