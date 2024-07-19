using UnityEngine;

public class FlyEnemy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Movimiento.Instance.DecreaseLives(1);
        }
    }
}
