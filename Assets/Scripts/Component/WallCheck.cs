using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    public List<ContactPoint2D> contactPoints = new List<ContactPoint2D>();
    public Collider2D col;

    public bool Left;
    public bool Right;
    public bool Up;
    public bool Down;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    public void Check()
    {
        // CapsuleCollider2D에 닿아 있는 접점 가져오기
        contactPoints.Clear();
        Physics2D.GetContacts(col, contactPoints);

        // 초기화 후 닿은 벽이 있는지 체크
        Left = false;
        Up = false;
        Right = false;
        Down = false;
        // 벽 확인
        foreach (ContactPoint2D point in contactPoints)
        {
            // 법선과 기준 벡터의 내적 계산
            float dotUp = Vector2.Dot(point.normal, Vector2.up);    // 위/아래 확인
            float dotRight = Vector2.Dot(point.normal, Vector2.right); // 좌/우 확인

            // 충돌 방향 판정 (오차 범위 ±0.1 허용)
            if (dotUp > 0.9f) { Down = true; } //바닥
            else if (dotUp < -0.9f) { Up = true; } // 천장

            if (dotRight > 0.9f) Left = true;  // 왼쪽 벽
            else if (dotRight < -0.9f) Right = true; // 오른쪽 벽
        }
    }
}
