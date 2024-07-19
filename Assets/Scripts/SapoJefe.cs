using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SapoJefe : MonoBehaviour
{
    public float speed = 2f;
    public int health = 50;
    public float attackInterval = 3f;
    public Text vidaTexto;

    private Transform player;
    private bool facingRight = false;
    private Animator animator;
    private float attackTimer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        attackTimer = attackInterval;
        UpdateHealthText();
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            if (direction.x > 0 && !facingRight || direction.x < 0 && facingRight)
            {
                Flip();
            }

            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                Attack();
                attackTimer = attackInterval;
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void Attack()
    {
        animator.SetTrigger("SapoAtaque");
        Invoke("ReturnToIdle", 0.5f);
    }

    void ReturnToIdle()
    {
        animator.SetTrigger("SapoQuieto");
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            UpdateHealthText();
        }
    }

    void UpdateHealthText()
    {
        if (vidaTexto != null)
        {
            vidaTexto.text = "Vidas: " + health;
        }
        Movimiento.Instance.UpdateJefeVidaText(health);
    }

    void Die()
    {
        animator.SetTrigger("SapoMuere");
        StartCoroutine(WaitForDeathAnimation());
    }

    IEnumerator WaitForDeathAnimation()
    {
        float animationDuration = 0.9f;
        yield return new WaitForSeconds(animationDuration);
        Movimiento.Instance.SetWinText();
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Movimiento.Instance.DecreaseLives(2);
        }
    }
}
