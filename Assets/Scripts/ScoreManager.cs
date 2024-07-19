using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public Text puntosText;
    private int puntos = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        UpdatePuntosText();
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetPuntos(); // Reinicia los puntos cada vez que se carga una nueva escena
        UpdatePuntosText();
    }

    public void IncrementarPuntos(int cantidad)
    {
        puntos += cantidad;
        UpdatePuntosText();
    }

    void UpdatePuntosText()
    {
        if (puntosText != null)
        {
            puntosText.text = "Puntos: " + puntos.ToString();
        }
    }

    void ResetPuntos()
    {
        puntos = 0; // Reinicia los puntos a cero
        UpdatePuntosText();
    }
}
