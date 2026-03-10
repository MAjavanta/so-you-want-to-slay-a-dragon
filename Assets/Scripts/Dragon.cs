using UnityEngine;
using UnityEngine.InputSystem;

public class Dragon : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 1f;
    private InputAction moveAction;
    private Rigidbody2D rb;
    private Vector2 moveValue;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveValue = moveAction.ReadValue<Vector2>();
        //transform.Translate(moveValue);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + (moveSpeed * Time.fixedDeltaTime * moveValue));
    }

}
