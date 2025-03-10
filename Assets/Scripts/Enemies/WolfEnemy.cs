using System.Collections;
using UnityEngine;

[RequireComponent(typeof(HealthSystem))]
public class WolfEnemy : MonoBehaviour
{
    private HealthSystem healthSystem;
    private Rigidbody rigidbody;
    private Player player;
    private bool isRanged;
    private AttackHitbox attackHitbox;
    private SpriteRenderer enemySpriteRenderer;

    private SpriteEffectsPropertySetter spriteEffectsPropertySetter;
    private SpriteFlasher spriteFlasher;

    private Animator animator;
    private enum AnimationName {
        wolfIdle,
        wolfAttackCharging,
        wolfAttack,
        wolfRun,
        wolfHurt,
    }

    private state currentState = state.Idle;
    private enum state {
        Idle,
        Following,
        Knockback,
        Stunned,
        Attacking,
        Death,
    }

    [Header("Idle State")]
    [SerializeField] private float detectionRadius;

    [Space][Header("Following State")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float disengageRadius;

    [Space][Header("Attack State")]
    [SerializeField] private float attackRadius;
    [SerializeField] private float disengageAttackRadius;
    [SerializeField] private GameObject hitboxSpawnPoint;
    [SerializeField] private float attackChargeupDuration;
    [SerializeField] private float attackDashSpeed;
    [SerializeField] private float attackDashDuration;
    [SerializeField] private float attackCooldown;
    private int enemyAttackDamage = 1;

    [Space][Header("Knockback State")]
    [SerializeField] private float knockbackSpeed;
    [SerializeField] private float knockbackDuration;

    [Space][Header("Stun State")]
    [SerializeField] private float stunDuration;

    [Space][Header("Death State")]
    [SerializeField] private GameObject deathParticles;

    private void Awake() {
        healthSystem = GetComponent<HealthSystem>();
        rigidbody = GetComponent<Rigidbody>();
        attackHitbox = FindObjectOfType<AttackHitbox>();
        enemySpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        spriteFlasher =  GetComponent<SpriteFlasher>();
        spriteEffectsPropertySetter = GetComponent<SpriteEffectsPropertySetter>();
        animator = GetComponent<Animator>();

        healthSystem.OnDamaged.AddListener(OnHit);

        currentState = state.Following;
    }
    
    private void Update() {
        if (currentState == state.Following) {
            FollowPlayer();
            SetAnimation(AnimationName.wolfRun);
        }

        if (currentState == state.Idle) {
            rigidbody.velocity = Vector3.zero;
            SetAnimation(AnimationName.wolfIdle);
        }

        UpdateState();
    }

    private void UpdateState() {
        if (currentState == state.Idle) {
            if (GetDistanceToPlayer() < detectionRadius) {
                currentState = state.Following;
            }
        }

        if (currentState == state.Following) {
            if (GetDistanceToPlayer() > disengageRadius) {
                currentState = state.Idle;
            }

            if (GetDistanceToPlayer() < attackRadius)
            {
                currentState = state.Attacking;
                StartCoroutine(attackCoroutine());
            }
        }
    }

    private void OnHit() {
        currentState = state.Knockback;
        SoundManager.Instance.PlaySound(GameSoundsData.Sound.Impact, transform.position);
        CinemachineCameraShake.Instance.ShakeCamera(1.0f, 0.15f);
        spriteFlasher.SingleFlash(0.35f);
        SetAnimation(AnimationName.wolfHurt);
        StopAllCoroutines();
        if (!healthSystem.IsHealthDepleted()) {
            StartCoroutine(knockbackCoroutine());
        } else {
            StartCoroutine(deathCoroutine());
        }
    }

    private IEnumerator attackCoroutine()
    {
        // Charge up the attack
        rigidbody.velocity = Vector3.zero;
        SetAnimation(AnimationName.wolfAttackCharging);
        spriteFlasher.ChargeUpFlash(attackChargeupDuration, 0.28f);

        float attackChargeupTimer = 0;
        while (attackChargeupTimer < attackChargeupDuration) {
            attackChargeupTimer += Time.deltaTime;

            if (GetDistanceToPlayer() > disengageAttackRadius)
            {
                // If player outside of range, exit attack state
                currentState = state.Idle;
                yield break;
            }

            yield return null;
        }

        // Charge has completed, now attack
        Vector3 attackDirection  = GetDirectionToPlayer();
        FlipSpriteToFaceDirection(GetDirectionToPlayer());

        float attackTimer = 0;
        bool hasAttacked = false;
        while (attackTimer < attackDashDuration) {
            attackTimer += Time.deltaTime;

            float normalizedTime = attackTimer/attackDashDuration;
            rigidbody.velocity = attackDirection * Mathf.Lerp(attackDashSpeed, 0.0f, normalizedTime);

            if (!hasAttacked && normalizedTime > 0.7f) {
                hasAttacked = true;
                SetAnimation(AnimationName.wolfAttack);
                attackHitbox.CreateHitBoxPrefab(enemyAttackDamage, false, hitboxSpawnPoint);
            }
            
            yield return null;
        }

        // Pause after the attack, then go back to idle
        yield return new WaitForSeconds(attackCooldown);
        currentState = state.Idle;
    }
    
    private IEnumerator knockbackCoroutine() {
        float knockbackTimer = 0;
        while (knockbackTimer < knockbackDuration) {
            knockbackTimer += Time.deltaTime;

            Vector3 knockbackDirection = -GetDirectionToPlayer();
            float normalizedTime = knockbackTimer/knockbackDuration;
            rigidbody.velocity = knockbackDirection * Mathf.Lerp(knockbackSpeed, 0.0f, normalizedTime);

            yield return null;
        }

        currentState = state.Stunned;
        yield return new WaitForSeconds(stunDuration);
        currentState = state.Idle;
    }

    private IEnumerator deathCoroutine() {
        spriteEffectsPropertySetter.SetSaturation(0);

        float knockbackTimer = 0;
        while (knockbackTimer < knockbackDuration) {
            knockbackTimer += Time.deltaTime;

            Vector3 knockbackDirection = -GetDirectionToPlayer();
            float normalizedTime = knockbackTimer/knockbackDuration;
            rigidbody.velocity = knockbackDirection * Mathf.Lerp(knockbackSpeed, 0.0f, normalizedTime);

            yield return null;
        }

        currentState = state.Stunned;
        yield return new WaitForSeconds(stunDuration);
        Die();
    }

    private void Die()
    {
        // Maybe play a death animation or particles or something
        Destroy(gameObject);
        player.addKill();
        ParticleSystem particles = Instantiate(deathParticles, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
        Destroy(particles.gameObject, particles.main.duration);
    }

    private void SetAnimation(AnimationName animation)
    {
        animator.Play(animation.ToString());
    }

    private void FollowPlayer() {
        rigidbody.velocity = moveSpeed * GetDirectionToPlayer();
        FlipSpriteToFaceDirection(GetDirectionToPlayer());
    }

    private Vector3 GetDirectionToPlayer() {
        return (player.transform.position - transform.position).normalized;
    }

    private float GetDistanceToPlayer() {
        return (player.transform.position - transform.position).magnitude;
    }
    
    public void FlipSpriteToFaceDirection(Vector2 direction)
    {
        if (direction.x == 0) return;
        enemySpriteRenderer.flipX = direction.x < 0f;
    }
}
