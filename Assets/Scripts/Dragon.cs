using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Attack))]
public class Dragon : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 1f;
    private InputAction moveAction;
    private Rigidbody2D rb;
    private Vector2 moveValue;
    private Attack attack;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody2D>();
        attack = GetComponent<Attack>();
    }

    private void Update()
    {
        moveValue = moveAction.ReadValue<Vector2>();
        //transform.Translate(moveValue);
    }

    private void FixedUpdate()
    {
        if (moveValue.x > 0)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
            attack.FacingDirection = new Vector2(1, 0);
        }
        else if (moveValue.x < 0)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
            attack.FacingDirection = new Vector2(-1, 0);
        }
        rb.MovePosition(rb.position + (moveSpeed * Time.fixedDeltaTime * moveValue));
    }

}
