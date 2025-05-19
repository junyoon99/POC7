using System.Drawing;
using UnityEngine;

public class CanPickUpObject : MonoBehaviour
{
    FixedJoint2D fixedJoint2D;
    Transform parentTransform;
    Rigidbody2D rb2d;

    private void Awake()
    {
        fixedJoint2D = GetComponent<FixedJoint2D>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void Escape() 
    {
        fixedJoint2D.connectedBody = null;
        transform.parent = null;
        parentTransform = null;
        fixedJoint2D.enabled = false;
    }

    public void Join(Transform parent) 
    {
        fixedJoint2D.enabled = true;
        fixedJoint2D.connectedBody = parent.GetComponent<Rigidbody2D>();

        parentTransform = parent;
        transform.parent = parent;
        transform.localPosition = new Vector3(0, 1.05f, 0);
        transform.rotation = Quaternion.identity;
    }

    public void FlyAway(Vector2 direction, float force) 
    {
        Escape();
        rb2d.linearVelocityY = Mathf.Clamp(rb2d.linearVelocityY, 0, float.MaxValue);
        rb2d.AddForce(direction * force, ForceMode2D.Impulse);
        rb2d.AddTorque(-direction.x*force, ForceMode2D.Impulse);
    }
}
