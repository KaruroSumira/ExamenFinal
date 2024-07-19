using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Movimiento : MonoBehaviour
{
    public static Movimiento Instance;
    public float runSpeed = 2f;
    public float jumpSpeed = 3f;
    private float originalRunSpeed;
    private float originalJumpSpeed;

    public int lives = 5;
    private float tiempoTranscurrido = 0f;
    private string powerUpActual = "";

    private Rigidbody2D rb2D;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public ParticleSystem footParticles;
    public Text vidasText;
    public Text powerUpText;
    public Text timeText;
    public Text jefeVidaText;
    public Transform puntoDisparo;

    public bool mirandoDerecha = true;
    private bool canDoubleJump;
    private bool hasDoubleJumped;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        originalRunSpeed = runSpeed;
        originalJumpSpeed = jumpSpeed;
        Instance = this;

        UpdateVidasText();
        UpdatePowerUpText();
        UpdateTimeText();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal > 0)
        {
            rb2D.velocity = new Vector2(runSpeed, rb2D.velocity.y);
            spriteRenderer.flipX = false;
            animator.SetBool("run", true);
            mirandoDerecha = true;
        }
        else if (horizontal < 0)
        {
            rb2D.velocity = new Vector2(-runSpeed, rb2D.velocity.y);
            spriteRenderer.flipX = true;
            animator.SetBool("run", true);
            mirandoDerecha = false;
        }
        else
        {
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
            animator.SetBool("run", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Checkground.IsGrounded)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
                canDoubleJump = true;
                hasDoubleJumped = false;
            }
            else if (canDoubleJump && !hasDoubleJumped)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
                hasDoubleJumped = true;
            }
        }

        Voltear();
        ManageParticles(horizontal);

        tiempoTranscurrido += Time.deltaTime;
        UpdateTimeText();
    }

    void Voltear()
    {
        if (mirandoDerecha)
        {
            puntoDisparo.localPosition = new Vector3(Mathf.Abs(puntoDisparo.localPosition.x), puntoDisparo.localPosition.y, puntoDisparo.localPosition.z);
        }
        else
        {
            puntoDisparo.localPosition = new Vector3(-Mathf.Abs(puntoDisparo.localPosition.x), puntoDisparo.localPosition.y, puntoDisparo.localPosition.z);
        }
    }

    void ManageParticles(float horizontal)
    {
        if (Checkground.IsGrounded && Mathf.Abs(horizontal) > 0)
        {
            if (!footParticles.isPlaying)
            {
                footParticles.Play();
            }
        }
        else
        {
            if (footParticles.isPlaying)
            {
                footParticles.Stop();
            }
        }
    }

    public void ApplyPowerUp(PowerUpType powerUp)
    {
        switch (powerUp)
        {
            case PowerUpType.SpeedBoost:
                StartCoroutine(SpeedBoost());
                break;
            case PowerUpType.JumpBoost:
                StartCoroutine(JumpBoost());
                break;
            case PowerUpType.DoubleDamage:
                StartCoroutine(DoubleDamage());
                break;
        }
        powerUpActual = powerUp.ToString();
        UpdatePowerUpText();
    }

    IEnumerator SpeedBoost()
    {
        runSpeed *= 1.5f;
        yield return new WaitForSeconds(4.5f);
        runSpeed = originalRunSpeed;
        powerUpActual = "";
        UpdatePowerUpText();
    }

    IEnumerator JumpBoost()
    {
        jumpSpeed *= 1.15f;
        yield return new WaitForSeconds(4);
        jumpSpeed = originalJumpSpeed;
        powerUpActual = "";
        UpdatePowerUpText();
    }

    IEnumerator DoubleDamage()
    {
        Bullet.damageMultiplier = 2;
        yield return new WaitForSeconds(5.5f);
        Bullet.damageMultiplier = 1;
        powerUpActual = "";
        UpdatePowerUpText();
    }

    public void DecreaseLives(int amount)
    {
        lives -= amount;
        if (lives <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            UpdateVidasText();
        }
    }

    void UpdateVidasText()
    {
        vidasText.text = "Vidas: " + lives.ToString();
    }

    void UpdatePowerUpText()
    {
        powerUpText.text = "Power-Up: " + powerUpActual;
    }

    void UpdateTimeText()
    {
        int minutos = Mathf.FloorToInt(tiempoTranscurrido / 60f);
        int segundos = Mathf.FloorToInt(tiempoTranscurrido % 60f);

        string tiempoTexto = string.Format("{0:00}:{1:00}", minutos, segundos);
        timeText.text = "Tiempo: " + tiempoTexto;
    }

    public void UpdateJefeVidaText(int vidaJefe)
    {
        if (jefeVidaText != null)
        {
            jefeVidaText.text = "Vida Jefe: " + vidaJefe;
        }
    }

    public void SetWinText()
    {
        if (jefeVidaText != null)
        {
            jefeVidaText.text = "¡Has ganado!";
        }
    }

    public void TriggerDamageAnimation()
    {
        StartCoroutine(PlayDamageAnimation());
    }

    IEnumerator PlayDamageAnimation()
    {
        animator.SetBool("Daño", true);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("Daño", false);
    }
}
