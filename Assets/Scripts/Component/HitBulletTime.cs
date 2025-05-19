using System.Collections.Generic;
using UnityEngine;

public class HitBulletTime : MonoBehaviour
{
    public List<GameObject> HitObjects;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Enemy>() && !HitObjects.Contains(collision.gameObject))
        {
            HitObjects.Add(collision.gameObject);
            GameManager.Instance.BulletTime(0.1f, 0.5f);
            CameraManager.Instance.ChangeTarget(transform);
            CameraManager.Instance.CloseUp(5, 0.2f);
        }
    }
}
