using UnityEngine;
using UnityEngine.InputSystem;

public class Dragon : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 0.2f;
    InputAction moveAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>() * moveSpeed;
        transform.Translate(moveValue);
    }
}
