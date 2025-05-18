using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUp : MonoBehaviour
{
    PlayerInput playerInput;
    Collider2D col;
    public List<ContactPoint2D> contactPoints = new List<ContactPoint2D>();

    PickUpObject pickUpObject;
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

    void TryPickUp(InputAction.CallbackContext ctx)
    {
        if (pickUpObject == null)
        {
            if (playerInput.Ingame.Direction.ReadValue<Vector2>().y >= 0) return;

            contactPoints.Clear();
            Physics2D.GetContacts(col, contactPoints);
            foreach (ContactPoint2D point in contactPoints)
            {
                if (point.collider.GetComponent<PickUpObject>())
                {
                    pickUpObject = point.collider.GetComponent<PickUpObject>();
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
