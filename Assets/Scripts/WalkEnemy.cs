using UnityEngine;

public class WalkEnemy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Movimiento.Instance.DecreaseLives(1);
        }
    }
}
