using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour
{
    private static WaitForSeconds _waitForSeconds0_2 = new(0.2f);
    [SerializeField] private LayerMask attackableLayer;
    [SerializeField] private Transform attackSpawnPoint;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private float baseDamage = 1f;
    [SerializeField] private float attackRadius = 1f;
    [SerializeField] private float attackAngle = 90f;
    [SerializeField] private int debugSegments = 20;
    [SerializeField] private Vector2 facingDirection = new(1, 0);
    private float _attackTimer = 1f;
    private LineRenderer _debugLine;


    private void Awake()
    {
        _debugLine = GetComponent<LineRenderer>();
    }


    private void Update()
    {
        _attackTimer -= Time.deltaTime;
        if (_attackTimer <= 0)
        {
            AutoAttack();
            _attackTimer = 1f / attackSpeed;
        }
    }

    private void AutoAttack()
    {
        DrawAttackCone();
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackSpawnPoint.position, attackRadius, attackableLayer);
        foreach (var collider in hits)
        {
            Vector2 directionToCollider = (collider.transform.position - attackSpawnPoint.position).normalized;
            float angle = Vector2.Angle(facingDirection, directionToCollider);
            if (angle < attackAngle / 2f)
            {
                print(collider);
            }
        }
    }

    private void DrawAttackCone()
    {
        Vector3 center = attackSpawnPoint.position;
        Vector2 attackDirection = facingDirection;

        float halfAngle = attackAngle / 2f;
        int pointCount = debugSegments + 2;
        _debugLine.positionCount = pointCount;
        _debugLine.SetPosition(0, center);

        for (int i = 0; i < debugSegments; i++)
        {
            float t = (float)i / (debugSegments - 1);
            float angle = Mathf.Lerp(-halfAngle, halfAngle, t);

            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Vector2 direction = rotation * attackDirection;
            Vector3 point = center + (Vector3)(direction * attackRadius);
            _debugLine.SetPosition(i + 1, point);
        }

        _debugLine.SetPosition(pointCount - 1, center);
        StartCoroutine(ClearDebug());
    }

    private IEnumerator ClearDebug()
    {
        yield return _waitForSeconds0_2;
        _debugLine.positionCount = 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 forward = facingDirection;

        Quaternion leftRotation = Quaternion.Euler(0, 0, attackAngle / 2);
        Quaternion rightRotation = Quaternion.Euler(0, 0, -attackAngle / 2);

        Vector3 leftDirection = leftRotation * forward;
        Vector3 rightDirection = rightRotation * forward;

        Gizmos.DrawLine(
            attackSpawnPoint.position,
            attackSpawnPoint.position + leftDirection * attackRadius
        );

        Gizmos.DrawLine(
            attackSpawnPoint.position,
            attackSpawnPoint.position + rightDirection * attackRadius
        );
    }

    public void SetFacingDirection(Vector2 newDirection)
    {
        facingDirection = newDirection;
    }
}
