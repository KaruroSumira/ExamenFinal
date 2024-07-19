using UnityEngine;

public class GeneradorDeObstaculos : MonoBehaviour
{
    public GameObject prefab; // Prefab a generar
    public int cantidadDePrefabs = 10; // Número de prefabs a generar
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

        // Generar prefabs en la posición inicial del objeto que tiene el script
        Vector3 puntoDeGeneracion = transform.position;

        for (int i = 0; i < cantidadDePrefabs; i++)
        {
            // Calcular la posición del nuevo prefab
            Vector3 posicionPrefab = puntoDeGeneracion + new Vector3(i * distanciaEntrePrefabs, 0, 0);
            // Instanciar el prefab en la posición calculada
            Instantiate(prefab, posicionPrefab, Quaternion.identity);
        }
    }
}
