# 📚 Bonnes Pratiques Unity pour Débutants

## 🗂️ Organisation du Projet

### Structure des Dossiers
```
Assets/
├── Scenes/              ← Vos scènes Unity
├── Scripts/             ← Tous vos scripts C#
│   ├── Player/          ← Scripts du joueur
│   ├── Managers/        ← Scripts de gestion (GameManager, etc.)
│   ├── Environment/     ← Scripts des obstacles, segments, etc.
│   └── UI/              ← Scripts d'interface
├── Prefabs/             ← Objets réutilisables
│   ├── Segments/
│   ├── Obstacles/
│   └── Collectibles/
├── Materials/           ← Matériaux et couleurs
├── Models/              ← Modèles 3D (FBX, OBJ)
├── Textures/            ← Images et textures
├── Audio/               ← Sons et musiques
│   ├── SFX/
│   └── Music/
└── Settings/            ← Configurations URP, etc.
```

---

## 💻 Conventions de Nommage

### Scripts C#
- **PascalCase** : `PlayerController.cs`, `GameManager.cs`
- Un script = une classe = un fichier
- Nom de fichier = nom de la classe

### GameObjects dans la scène
- **PascalCase** : `Player`, `MainCamera`, `GameManager`
- Descriptif et clair

### Variables
```csharp
public float forwardSpeed;      // camelCase pour variables publiques
private int currentLane;        // camelCase pour variables privées
private const string PLAYER_TAG = "Player"; // SNAKE_UPPER pour constantes
```

### Prefabs
- Descriptif avec type : `Segment_Simple`, `Obstacle_Rock`, `Collectible_Leaf`

---

## 🏗️ Architecture des Scripts

### 1. Commentaires et Documentation
```csharp
/// <summary>
/// Description claire du rôle de la classe
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Description de la méthode
    /// </summary>
    /// <param name="direction">Description du paramètre</param>
    void MoveLane(int direction)
    {
        // Commentaire sur le code complexe
    }
}
```

### 2. Organisation d'un Script
```csharp
using UnityEngine;

public class ExampleScript : MonoBehaviour
{
    // 1. Variables publiques avec [Header] et [Tooltip]
    [Header("Mouvement")]
    [Tooltip("Vitesse de déplacement")]
    public float speed = 10f;

    // 2. Variables privées
    private bool isMoving = false;

    // 3. Méthodes Unity (Awake, Start, Update)
    void Start()
    {
        Initialize();
    }

    void Update()
    {
        HandleMovement();
    }

    // 4. Méthodes personnalisées
    void Initialize()
    {
        // Code d'initialisation
    }

    void HandleMovement()
    {
        // Code de mouvement
    }

    // 5. Méthodes publiques (appelables de l'extérieur)
    public void StopMovement()
    {
        isMoving = false;
    }
}
```

---

## 🎯 Pattern Singleton (GameManager)

### Quand l'utiliser ?
- Manager unique dans la scène (GameManager, AudioManager, etc.)
- Accès global : `GameManager.Instance.AddScore(10);`

### Comment l'implémenter ?
```csharp
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        // Vérifier si une instance existe déjà
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        // DontDestroyOnLoad(gameObject); // Si vous voulez garder entre les scènes
    }
}
```

---

## 🏷️ Utilisation des Tags

### Tags Essentiels
- `Player` : pour le joueur
- `Obstacle` : pour les obstacles
- `Collectible` : pour les objets à ramasser
- `Ground` : pour le sol

### Créer un Tag
1. Inspector (en haut) → Tag → Add Tag
2. Créer le tag
3. Réassigner à l'objet

### Utiliser un Tag
```csharp
if (other.CompareTag("Player"))
{
    // Code...
}
```

⚠️ **Éviter** : `if (other.tag == "Player")` (moins performant)

---

## ⚡ Optimisation et Performance

### 1. Object Pooling
**Pourquoi ?** Éviter les `Instantiate()` et `Destroy()` répétés (coûteux)

```csharp
// Au lieu de :
Instantiate(bulletPrefab, position, rotation);
Destroy(bullet, 2f);

// Utiliser un pool :
GameObject bullet = bulletPool.Dequeue();
bullet.SetActive(true);
// Plus tard :
bullet.SetActive(false);
bulletPool.Enqueue(bullet);
```

### 2. Cache des Composants
```csharp
// ❌ MAUVAIS (recherche à chaque frame)
void Update()
{
    GetComponent<Rigidbody>().AddForce(...);
}

// ✅ BON (cache dans Start)
private Rigidbody rb;

void Start()
{
    rb = GetComponent<Rigidbody>();
}

void Update()
{
    rb.AddForce(...);
}
```

### 3. Éviter Find dans Update
```csharp
// ❌ MAUVAIS
void Update()
{
    GameObject player = GameObject.Find("Player");
}

// ✅ BON
private GameObject player;

void Start()
{
    player = GameObject.FindGameObjectWithTag("Player");
}
```

---

## 🎮 Input System

### Méthode Classique (Input Manager)
```csharp
void Update()
{
    // Touche appuyée
    if (Input.GetKeyDown(KeyCode.Space))
    {
        Jump();
    }

    // Touche maintenue
    if (Input.GetKey(KeyCode.W))
    {
        MoveForward();
    }

    // Axes (-1 à 1)
    float horizontal = Input.GetAxis("Horizontal");
}
```

### Différence GetKey vs GetKeyDown
- `GetKeyDown` : Une fois au moment de l'appui
- `GetKey` : Tant que la touche est maintenue
- `GetKeyUp` : Une fois au moment du relâchement

---

## 🔄 Cycle de Vie Unity

### Ordre d'exécution :
```csharp
void Awake()     // Avant Start, pour initialiser des références
void Start()     // Au premier frame, après tous les Awake
void Update()    // Chaque frame (input, logique de jeu)
void FixedUpdate() // Physique (à intervalle fixe, ~50fps)
void LateUpdate()  // Après Update (caméra qui suit)
```

---

## 🧩 Prefabs

### Qu'est-ce qu'un Prefab ?
Un modèle réutilisable d'objet. Modification du prefab = modification de toutes les instances.

### Créer un Prefab
1. Configurer l'objet dans la scène
2. Glisser dans le dossier Prefabs
3. Supprimer l'original de la scène (optionnel)

### Instantier un Prefab
```csharp
public GameObject enemyPrefab;

void SpawnEnemy()
{
    Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
}
```

---

## 🎨 Materials et Shaders

### Créer un Material
1. Clic-droit dans Assets/Materials
2. Create → Material
3. Choisir une couleur ou texture
4. Glisser sur un objet 3D

### URP (Universal Render Pipeline)
- Utilisé dans votre projet
- Matériaux spécifiques URP
- Post-processing disponible

---

## 🐛 Debugging

### Debug.Log
```csharp
Debug.Log("Message normal");
Debug.LogWarning("Attention !");
Debug.LogError("Erreur !");
Debug.Log($"Score actuel : {currentScore}");
```

### Gizmos (visualisation dans l'éditeur)
```csharp
void OnDrawGizmos()
{
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, 5f);
}
```

### Breakpoints (Visual Studio)
- Cliquer à gauche d'une ligne pour poser un breakpoint
- Play en mode Debug dans Unity
- Le code s'arrête au breakpoint

---

## 📊 Inspector Tips

### [Header] et [Tooltip]
```csharp
[Header("Mouvement")]
[Tooltip("Vitesse de déplacement en m/s")]
public float speed = 10f;
```

### [Range] pour slider
```csharp
[Range(0f, 100f)]
public float health = 100f;
```

### [SerializeField] pour variables privées visibles
```csharp
[SerializeField]
private int secretValue = 42; // Visible dans Inspector mais reste privée
```

---

## 🎯 Collisions

### Collider vs Trigger
- **Collider normal** : physique réaliste, objets se bloquent
- **Trigger (Is Trigger coché)** : détection sans physique

### Méthodes de collision
```csharp
// Collider normal
void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Enemy"))
    {
        // Contact physique
    }
}

// Trigger
void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Collectible"))
    {
        // Zone de détection
    }
}
```

### CharacterController
```csharp
void OnControllerColliderHit(ControllerColliderHit hit)
{
    if (hit.gameObject.CompareTag("Obstacle"))
    {
        // Collision avec CharacterController
    }
}
```

---

## 💾 Sauvegarder des Données

### PlayerPrefs (simple)
```csharp
// Sauvegarder
PlayerPrefs.SetInt("HighScore", 1000);
PlayerPrefs.SetFloat("Volume", 0.8f);
PlayerPrefs.SetString("PlayerName", "John");
PlayerPrefs.Save();

// Charger
int highScore = PlayerPrefs.GetInt("HighScore", 0); // 0 = valeur par défaut
```

⚠️ **Limitation** : données simples seulement, pas de listes/objets complexes.

---

## 🔧 Build Settings

### Préparer un Build
1. File → Build Settings
2. Add Open Scenes (ajouter votre scène)
3. Choisir la plateforme (PC, Mac, Android, etc.)
4. Player Settings → configurer icône, nom, résolution
5. Build

---

## ✅ Checklist Avant de Coder

- [ ] Scripts bien organisés dans des sous-dossiers
- [ ] Noms clairs et descriptifs
- [ ] Commentaires sur les parties complexes
- [ ] Tags créés et assignés
- [ ] Références assignées dans l'Inspector
- [ ] Test fréquent (Play souvent !)

---

## 📚 Ressources Utiles

### Documentation
- [Unity Manual](https://docs.unity3d.com/Manual/index.html)
- [Unity Scripting API](https://docs.unity3d.com/ScriptReference/index.html)

### Communauté
- Unity Forums
- Stack Overflow (tag: unity3d)
- Reddit r/Unity3D

### Assets Gratuits
- Unity Asset Store (Free Assets)
- Kenney.nl (low poly assets)
- Mixamo (animations de personnages)

---

**Bon apprentissage ! 🚀**
