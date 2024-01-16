using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float currentSpeed;

    public float chaseSpeed;
    public float chaseWaitTime = 0;
    
    public float patrolSpeed;
    public float patrolWaitTime = 2;

    public float hurtForce;

    public Vector3 FaceDir
    {
        get => new Vector3(-transform.localScale.x, 0, 0);
        set => transform.localScale = new Vector3(-value.x, transform.localScale.y, transform.localScale.z);
    }

    Rigidbody2D rb;
    public Animator animator;
    public PhysicsCheck physicsCheck;
    public SightDetection sightDetection;

    
    public bool isWaiting;
    public bool isHurt;
    bool isDead;

    public BaseState patrolState;
    public BaseState chaseState;

    private BaseState currentState;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
        sightDetection = GetComponent<SightDetection>();
    }

    private void OnEnable()
    {
        ChangeState(patrolState);
    }

    private void OnDisable()
    {
        currentState.OnExit();
    }

    private void Update()
    {
        currentState.LogicUpdate();
    }

    public IEnumerator TurnAndWait(float waitTime, int preMoveType=1)
    {
        TurnAround();

        isWaiting = true;
        animator.SetInteger("moveType", 0);
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(waitTime);
        animator.SetInteger("moveType", preMoveType);
        isWaiting = false;
    }

    private void TurnAround()
    {
        FaceDir = Vector3.Scale(new Vector3(-1, 1, 1), FaceDir);
        physicsCheck.TurnAround();
    }

    private void FixedUpdate()
    {
        if (isWaiting || isHurt || isDead) { return; }
        Move();
        currentState.PhysicsUpdate();
    }

    public virtual void Move()
    {
        rb.velocity = new Vector2(currentSpeed * FaceDir.x, rb.velocity.y);
    }

    public void onHurt(Transform attackerTransform)
    {
        Vector2 attackDir = new Vector2(transform.position.x - attackerTransform.position.x, 0).normalized;

        StartCoroutine(OnKnockBack(attackDir));
    }

    IEnumerator OnKnockBack(Vector2 attackDir)
    {
        isHurt = true;
        animator.SetTrigger("hurt");

        rb.AddForce(hurtForce * attackDir, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.333f);

        if (attackDir.x * FaceDir.x > 0)
        {
            TurnAround();
        }

        yield return new WaitForSeconds(0.333f);

        isHurt = false;
    }

    public void onDeath()
    {
        isDead = true;
        animator.SetBool("dead", true);
    }

    public void DestroyAfterAnimation()
    {
        Destroy(this.gameObject);
    }

    public void ChangeState(BaseState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }
        currentState = newState;
        currentState.OnEnter(this);
    }
}
