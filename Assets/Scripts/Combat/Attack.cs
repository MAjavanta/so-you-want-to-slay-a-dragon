using UnityEngine;

public class Attack : MonoBehaviour
{

    [SerializeField] private float attackSpeed = 1f;
    private float attackTimer = 1f;


    private void Update()
    {
        attackTimer -= Time.deltaTime * attackSpeed;
        if (attackTimer <= 0)
        {
            AutoAttack();
            attackTimer = 1f;
        }
    }

    public void AutoAttack()
    {
        Debug.Log($"{this} attack");
    }
}
