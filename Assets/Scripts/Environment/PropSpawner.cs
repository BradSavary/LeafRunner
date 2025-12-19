using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawne des props (arbres, rochers, buissons) sur les côtés des segments
/// Peut être attaché directement sur un segment ou utilisé globalement
/// </summary>
public class PropSpawner : MonoBehaviour
{
    [Header("Props à Spawner")]
    [Tooltip("Liste des prefabs de props (arbres, rochers, etc.)")]
    public List<GameObject> propPrefabs = new List<GameObject>();
    
    [Header("Configuration du Spawn")]
    [Tooltip("Nombre de props à spawner sur ce segment")]
    public int propsPerSegment = 6;
    
    [Tooltip("Distance minimale entre deux props")]
    public float minDistanceBetweenProps = 2f;
    
    [Header("Positions (Côtés)")]
    [Tooltip("Spawner sur le côté gauche")]
    public bool spawnLeft = true;
    
    [Tooltip("Spawner sur le côté droit")]
    public bool spawnRight = true;
    
    [Tooltip("Distance par rapport au centre (axe X)")]
    public float sideDistance = 7f;
    
    [Header("Variation")]
    [Tooltip("Variation aléatoire sur X (pour éviter l'alignement parfait)")]
    public float xVariation = 2f;
    
    [Tooltip("Variation aléatoire sur Z")]
    public float zVariation = 1f;
    
    [Tooltip("Rotation aléatoire sur Y")]
    public bool randomRotation = true;
    
    [Tooltip("Variation d'échelle")]
    public Vector2 scaleRange = new Vector2(0.8f, 1.2f);
    
    [Header("Zone de Spawn")]
    [Tooltip("Longueur du segment (pour spawner tout le long)")]
    public float segmentLength = 10f;
    
    [Tooltip("Décalage de départ sur Z")]
    public float startOffset = 1f;
    
    [Tooltip("Décalage de fin sur Z")]
    public float endOffset = 1f;

    void Start()
    {
        // Spawner les props au démarrage du segment
        SpawnProps();
    }

    /// <summary>
    /// Spawne tous les props sur les côtés
    /// </summary>
    public void SpawnProps()
    {
        if (propPrefabs.Count == 0)
        {
            Debug.LogWarning($"PropSpawner sur {gameObject.name} : aucun prefab assigné !");
            return;
        }

        List<Vector3> spawnedPositions = new List<Vector3>();

        for (int i = 0; i < propsPerSegment; i++)
        {
            // Choisir un prop aléatoire
            GameObject propPrefab = propPrefabs[Random.Range(0, propPrefabs.Count)];
            
            // Choisir un côté (gauche ou droite)
            bool isLeft = Random.value > 0.5f;
            if (!spawnLeft) isLeft = false;
            if (!spawnRight) isLeft = true;
            
            // Calculer la position
            Vector3 position = GetRandomPosition(isLeft, spawnedPositions);
            
            // Spawner le prop
            GameObject prop = Instantiate(propPrefab, position, Quaternion.identity, transform);
            
            // Rotation aléatoire
            if (randomRotation)
            {
                prop.transform.rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
            }
            
            // Échelle aléatoire
            float randomScale = Random.Range(scaleRange.x, scaleRange.y);
            prop.transform.localScale = Vector3.one * randomScale;
            
            // Ajouter à la liste des positions
            spawnedPositions.Add(position);
        }
    }

    /// <summary>
    /// Calcule une position aléatoire valide
    /// </summary>
    Vector3 GetRandomPosition(bool isLeft, List<Vector3> existingPositions)
    {
        Vector3 position;
        int attempts = 0;
        int maxAttempts = 20;

        do
        {
            // Position de base
            float x = isLeft ? -sideDistance : sideDistance;
            
            // Ajouter variation sur X
            x += Random.Range(-xVariation, xVariation);
            
            // Position Z le long du segment
            float z = transform.position.z + startOffset + Random.Range(0f, segmentLength - startOffset - endOffset);
            
            // Ajouter variation sur Z
            z += Random.Range(-zVariation, zVariation);
            
            // Position Y au sol (ou légèrement au-dessus)
            float y = 0f;
            
            position = new Vector3(x, y, z);
            attempts++;
            
            // Vérifier la distance avec les autres props
            if (IsFarEnough(position, existingPositions) || attempts >= maxAttempts)
            {
                break;
            }
            
        } while (true);

        return position;
    }

    /// <summary>
    /// Vérifie si la position est assez loin des autres props
    /// </summary>
    bool IsFarEnough(Vector3 position, List<Vector3> existingPositions)
    {
        foreach (Vector3 existingPos in existingPositions)
        {
            float distance = Vector3.Distance(position, existingPos);
            if (distance < minDistanceBetweenProps)
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Visualise les zones de spawn dans l'éditeur
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        
        // Zone gauche
        if (spawnLeft)
        {
            Vector3 leftStart = transform.position + new Vector3(-sideDistance, 0, startOffset);
            Vector3 leftEnd = transform.position + new Vector3(-sideDistance, 0, segmentLength - endOffset);
            Gizmos.DrawLine(leftStart, leftEnd);
            Gizmos.DrawWireSphere(leftStart, 0.5f);
            Gizmos.DrawWireSphere(leftEnd, 0.5f);
        }
        
        // Zone droite
        if (spawnRight)
        {
            Vector3 rightStart = transform.position + new Vector3(sideDistance, 0, startOffset);
            Vector3 rightEnd = transform.position + new Vector3(sideDistance, 0, segmentLength - endOffset);
            Gizmos.DrawLine(rightStart, rightEnd);
            Gizmos.DrawWireSphere(rightStart, 0.5f);
            Gizmos.DrawWireSphere(rightEnd, 0.5f);
        }
    }
}
