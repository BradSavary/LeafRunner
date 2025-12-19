using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gère le spawn infini des segments de route
/// Utilise un système de pooling pour optimiser les performances
/// </summary>
public class SegmentSpawner : MonoBehaviour
{
    [Header("Segments")]
    [Tooltip("Segment de démarrage (sans obstacles, toujours le premier)")]
    public GameObject startSegmentPrefab;
    
    [Tooltip("Liste des prefabs de segments aléatoires (avec obstacles)")]
    public List<GameObject> segmentPrefabs = new List<GameObject>();
    
    [Tooltip("Nombre de segments visibles en même temps")]
    public int visibleSegments = 5;
    
    [Tooltip("Longueur d'un segment (doit correspondre à la taille réelle)")]
    public float segmentLength = 10f;

    [Header("Spawn")]
    [Tooltip("Position Z du prochain spawn")]
    private float spawnZ = 0f;
    
    [Tooltip("Position de départ du premier segment")]
    public float initialSpawnZ = 0f;

    [Header("Pooling")]
    [Tooltip("Liste des segments actifs actuellement")]
    private List<GameObject> activeSegments = new List<GameObject>();
    
    [Tooltip("Pool de segments réutilisables")]
    private Queue<GameObject> segmentPool = new Queue<GameObject>();
    
    private int segmentCounter = 0; // Compte les segments spawnés

    [Header("Joueur")]
    [Tooltip("Référence au Transform du joueur pour détecter quand recycler")]
    public Transform playerTransform;
    
    [Tooltip("Distance derrière le joueur avant de recycler un segment")]
    public float recycleDistance = 20f;

    void Start()
    {
        // Trouver le joueur si non assigné
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
            
            if (playerTransform == null)
            {
                Debug.LogError("Joueur non trouvé ! Assurez-vous qu'il a le tag 'Player'.");
            }
        }

        // Vérifier qu'il y a au moins un segment
        if (segmentPrefabs.Count == 0)
        {
            Debug.LogError("Aucun segment assigné dans le SegmentSpawner !");
            return;
        }

        // Initialiser la position de spawn
        spawnZ = initialSpawnZ;

        // Spawner les premiers segments
        for (int i = 0; i < visibleSegments; i++)
        {
            SpawnSegment();
        }
    }

    void Update()
    {
        // Vérifier si on doit spawner un nouveau segment
        if (playerTransform != null && spawnZ < playerTransform.position.z + (visibleSegments * segmentLength))
        {
            SpawnSegment();
        }

        // Recycler les segments derrière le joueur
        RecycleOldSegments();
    }

    /// <summary>
    /// Spawne un nouveau segment à la position de spawn
    /// </summary>
    void SpawnSegment()
    {
        GameObject segment;

        // Vérifier s'il y a des segments dans le pool
        if (segmentPool.Count > 0)
        {
            // Réutiliser un segment du pool
            segment = segmentPool.Dequeue();
            segment.SetActive(true);
        }
        else
        {
            // Créer un nouveau segment
            GameObject prefabToSpawn;
            
            // Premier segment : toujours le segment de démarrage
            if (segmentCounter == 0 && startSegmentPrefab != null)
            {
                prefabToSpawn = startSegmentPrefab;
            }
            // Segments suivants : aléatoires
            else if (segmentPrefabs.Count > 0)
            {
                prefabToSpawn = segmentPrefabs[Random.Range(0, segmentPrefabs.Count)];
            }
            else
            {
                Debug.LogError("Aucun segment disponible !");
                return;
            }
            
            segment = Instantiate(prefabToSpawn, transform);
        }

        // Positionner le segment
        segment.transform.position = new Vector3(0, 0, spawnZ);

        // Ajouter à la liste des segments actifs
        activeSegments.Add(segment);

        // Récupérer la longueur du segment (personnalisée ou par défaut)
        float currentSegmentLength = segmentLength; // Longueur par défaut
        SegmentInfo segmentInfo = segment.GetComponent<SegmentInfo>();
        if (segmentInfo != null)
        {
            currentSegmentLength = segmentInfo.GetLength();
        }

        // Mettre à jour la position du prochain spawn
        spawnZ += currentSegmentLength;
        
        // Incrémenter le compteur
        segmentCounter++;
    }

    /// <summary>
    /// Recycle les segments qui sont derrière le joueur
    /// </summary>
    void RecycleOldSegments()
    {
        if (playerTransform == null || activeSegments.Count == 0) return;

        // Vérifier le premier segment de la liste (le plus ancien)
        GameObject oldestSegment = activeSegments[0];
        
        // Si le segment est assez loin derrière le joueur
        if (oldestSegment.transform.position.z < playerTransform.position.z - recycleDistance)
        {
            // Retirer de la liste active
            activeSegments.RemoveAt(0);

            // Désactiver et ajouter au pool
            oldestSegment.SetActive(false);
            segmentPool.Enqueue(oldestSegment);
        }
    }

    /// <summary>
    /// Réinitialise le spawner (pour le restart)
    /// </summary>
    public void ResetSpawner()
    {
        // Désactiver tous les segments actifs et les remettre dans le pool
        foreach (GameObject segment in activeSegments)
        {
            segment.SetActive(false);
            segmentPool.Enqueue(segment);
        }

        activeSegments.Clear();

        // Réinitialiser la position de spawn
        spawnZ = initialSpawnZ;
        
        // Réinitialiser le compteur
        segmentCounter = 0;

        // Respawner les premiers segments
        for (int i = 0; i < visibleSegments; i++)
        {
            SpawnSegment();
        }
    }

    /// <summary>
    /// Visualise les zones de spawn et recyclage dans l'éditeur
    /// </summary>
    void OnDrawGizmos()
    {
        if (playerTransform == null) return;

        // Zone de spawn (vert)
        Gizmos.color = Color.green;
        float spawnZoneZ = playerTransform.position.z + (visibleSegments * segmentLength);
        Gizmos.DrawWireCube(
            new Vector3(0, 1, spawnZoneZ),
            new Vector3(10, 2, 2)
        );

        // Zone de recyclage (rouge)
        Gizmos.color = Color.red;
        float recycleZoneZ = playerTransform.position.z - recycleDistance;
        Gizmos.DrawWireCube(
            new Vector3(0, 1, recycleZoneZ),
            new Vector3(10, 2, 2)
        );
    }
}
