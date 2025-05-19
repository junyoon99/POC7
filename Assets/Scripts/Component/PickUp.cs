using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUp : MonoBehaviour
{
    PlayerInput playerInput;
    Collider2D col;

    public CanPickUpObject pickUpObject;
    public float MaxThrowForce = 10f;
    public float MinThrowForce = 1f;
    public float ThrowChargeMultiplier = 1f;
    public float ThrowForce = 0f;

    private void Start()
    {
        playerInput = InputManager.Instance.input;
        print(playerInput);
        playerInput.Ingame.X.started += TryPickUp;
        col = GetComponent<Collider2D>();
    }

    public void TryPickUp(InputAction.CallbackContext ctx)
    {
        if (pickUpObject == null)
        {
            if (playerInput.Ingame.Direction.ReadValue<Vector2>().y >= 0) return;

            Collider2D[] contactColliders = Physics2D.OverlapAreaAll(col.bounds.min, col.bounds.max);
            foreach (Collider2D point in contactColliders)
            {
                if (point.GetComponent<CanPickUpObject>())
                {
                    pickUpObject = point.GetComponent<CanPickUpObject>();
                    pickUpObject.GetComponent<HitBulletTime>().HitObjects.Clear();
                    pickUpObject.Join(transform);
                    return;
                }
            }
        }
        else 
        {
            StartCoroutine(Charge());
        }
    }

    IEnumerator Charge() 
    {
        while (playerInput.Ingame.X.IsPressed())
        {
            ThrowForce += Time.deltaTime * ThrowChargeMultiplier;
            ThrowForce = Mathf.Clamp(ThrowForce, 0, MaxThrowForce);
            yield return null;
        }
        ThrowObject();
    }

    void ThrowObject()
    {
        Vector2 inputDirection = playerInput.Ingame.Direction.ReadValue<Vector2>();
        Vector2 mouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized;
        if (inputDirection != Vector2.zero)
        {
            pickUpObject.FlyAway(inputDirection, Mathf.Clamp(ThrowForce, MinThrowForce, MaxThrowForce));
        }
        else 
        {
            pickUpObject.Escape();
        }
        ThrowForce = 0f;
        pickUpObject = null;
    }
}
