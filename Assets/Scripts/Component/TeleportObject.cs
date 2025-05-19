using UnityEngine;

public class TeleportObject : MonoBehaviour
{
    public void Teleport(Transform target)
    {
        target.position = transform.position + Vector3.up;
        CanPickUpObject pickup = GetComponent<CanPickUpObject>();
        pickup.Join(target);
        target.GetComponent<PickUp>().pickUpObject = pickup;
    }
}
