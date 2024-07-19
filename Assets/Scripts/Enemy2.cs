using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    private int lives = 2;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Movimiento.Instance.DecreaseLives(1);
            Movimiento.Instance.TriggerDamageAnimation();
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        lives -= damage;
        if (lives <= 0)
        {
            Destroy(gameObject);
        }
    }
}
