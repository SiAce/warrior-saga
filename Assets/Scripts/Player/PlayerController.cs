using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;
    public Vector2 moveDirection;
    public float speed;
    public float jumpForce;
    public float hurtForce;
    public bool dead;
    public bool isAttacking;

    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D smooth;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private PhysicsCheck physicsCheck;
    private PlayerAnimation playerAnimation;
    private Collider2D cl;

    private bool isHurt;
    private bool isWallJumping;
    private InteractableController interactable;

    private void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        physicsCheck = GetComponent<PhysicsCheck>();
        playerAnimation = GetComponent<PlayerAnimation>();
        cl = GetComponent<Collider2D>();
        inputControl = new PlayerInputControl();
        inputControl.Gameplay.Jump.started += Jump;
        inputControl.Gameplay.Attack.started += PlayerAttack;
        inputControl.Gameplay.Interact.performed += Interact;
    }

    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        isHurt = false;
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
    }

    public void GetHit(Transform attacker)
    {
        Vector3 hitDirection = (transform.position - attacker.position).normalized;

        rb.AddForce(
            hurtForce * hitDirection,
            ForceMode2D.Impulse
        );

        StartCoroutine(SetHurt());
    }

    public void SetDeath()
    {
        dead = true;
        inputControl.Gameplay.Disable();
    }

    private void FixedUpdate()
    {
        ChangeMaterial();
        if (physicsCheck.isGround)
        {
            isWallJumping = false;
        }
        if (!isHurt && !isAttacking && !isWallJumping)
        {
            Move();
        }
    }

    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            interactable = collision.GetComponent<InteractableController>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            interactable = null;
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(speed * moveDirection.x, rb.velocity.y);

        float faceDir = moveDirection.x switch
        {
            > 0 => 1,
            < 0 => -1,
            _ => transform.localScale.x,
        };

        transform.localScale = new Vector3(faceDir, 1, 1);

    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (physicsCheck.touchLeftWall)
        {
            rb.velocity = new Vector2(8, 17);
            isWallJumping = true;
            return;
        }

        if (physicsCheck.touchRightWall)
        {
            rb.velocity = new Vector2(-8, 17);
            isWallJumping = true;
            return;
        }
        if (physicsCheck.isGround)
        {
            rb.AddForce(
            jumpForce * Vector2.up,
            ForceMode2D.Impulse
            );
            return;
        }



    }

    private void PlayerAttack(InputAction.CallbackContext context)
    {
        playerAnimation.PlayerAttack();
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if (interactable)
        {
            interactable.OnInteraction();
        }
    }


    private void ChangeMaterial()
    {
        cl.sharedMaterial = physicsCheck.isGround ? normal : smooth;
    }

    private IEnumerator SetHurt()
    {
        isHurt = true;
        yield return new WaitForSeconds(0.4f);
        isHurt = false;
    }


}
