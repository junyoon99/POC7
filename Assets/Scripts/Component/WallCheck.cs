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
        // CapsuleCollider2D�� ��� �ִ� ���� ��������
        contactPoints.Clear();
        Physics2D.GetContacts(col, contactPoints);

        // �ʱ�ȭ �� ���� ���� �ִ��� üũ
        Left = false;
        Up = false;
        Right = false;
        Down = false;
        // �� Ȯ��
        foreach (ContactPoint2D point in contactPoints)
        {
            // ������ ���� ������ ���� ���
            float dotUp = Vector2.Dot(point.normal, Vector2.up);    // ��/�Ʒ� Ȯ��
            float dotRight = Vector2.Dot(point.normal, Vector2.right); // ��/�� Ȯ��

            // �浹 ���� ���� (���� ���� ��0.1 ���)
            if (dotUp > 0.9f) { Down = true; } //�ٴ�
            else if (dotUp < -0.9f) { Up = true; } // õ��

            if (dotRight > 0.9f) Left = true;  // ���� ��
            else if (dotRight < -0.9f) Right = true; // ������ ��
        }
    }
}
