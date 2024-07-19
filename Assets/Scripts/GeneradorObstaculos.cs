using UnityEngine;

public class GeneradorDeObstaculos : MonoBehaviour
{
    public GameObject prefab; // Prefab a generar
    public int cantidadDePrefabs = 10; // N�mero de prefabs a generar
    public float distanciaEntrePrefabs = 2.0f; // Distancia entre prefabs

    void Start()
    {
        GenerarPrefabs();
    }

    void GenerarPrefabs()
    {
        if (prefab == null)
        {
            Debug.LogError("Prefab no asignado en el inspector.");
            return;
        }

        // Generar prefabs en la posici�n inicial del objeto que tiene el script
        Vector3 puntoDeGeneracion = transform.position;

        for (int i = 0; i < cantidadDePrefabs; i++)
        {
            // Calcular la posici�n del nuevo prefab
            Vector3 posicionPrefab = puntoDeGeneracion + new Vector3(i * distanciaEntrePrefabs, 0, 0);
            // Instanciar el prefab en la posici�n calculada
            Instantiate(prefab, posicionPrefab, Quaternion.identity);
        }
    }
}
