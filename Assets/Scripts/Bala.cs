using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public static int damageMultiplier = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            int totalDamage = damage * damageMultiplier;

            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(totalDamage);
                Destroy(gameObject);
                return;
            }

            Enemy2 enemy2 = collision.gameObject.GetComponent<Enemy2>();
            if (enemy2 != null)
            {
                enemy2.TakeDamage(totalDamage);
                Destroy(gameObject);
                return;
            }

            SapoJefe sapoJefe = collision.gameObject.GetComponent<SapoJefe>();
            if (sapoJefe != null)
            {
                sapoJefe.TakeDamage(totalDamage);
                Destroy(gameObject);
                return;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
