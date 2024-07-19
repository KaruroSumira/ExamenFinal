using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkground : MonoBehaviour
{
    public static bool IsGrounded;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IsGrounded = true;
        Debug.Log("Se activó");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsGrounded = false;
        Debug.Log("Se desactivó");
    }
}
