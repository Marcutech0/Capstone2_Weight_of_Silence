using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    private PlayerControls _controls;
    private CharacterController _controller;
    private Vector2 _moveInput;

    [Header("Animation")]
    public Animator animator;

    [Header("Animation Flipping")]
    private SpriteRenderer _spriteRenderer;
    public bool isFacingRight = true;

    void Awake()
    {
        _controls = new PlayerControls();
        _controller = GetComponent<CharacterController>();
    }

    void OnEnable() => _controls.Player.Enable();
    void OnDisable() => _controls.Player.Disable();

    void Start()
    {
        _controls.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        _controls.Player.Move.canceled += ctx => _moveInput = Vector2.zero;

        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // ✅ Convert input (2D plane movement on X and Z)
        Vector3 move = new Vector3(_moveInput.x, 0f, _moveInput.y);
        _controller.Move(move * moveSpeed * Time.deltaTime);

        // ✅ Animator parameters (for idle / walk)
        animator.SetFloat("Horizontal", _moveInput.x);
        animator.SetFloat("Vertical", _moveInput.y);
        animator.SetFloat("Speed", move.sqrMagnitude);

        // ✅ Flip sprite based on movement direction
        if (_moveInput.x > 0 && !isFacingRight)
            FlipSprite();
        else if (_moveInput.x < 0 && isFacingRight)
            FlipSprite();
    }

    //Sprite will flip based on the horizontal input direction
    public void FlipSprite()
    {
        isFacingRight = !isFacingRight;
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }
}
