# 🔊 Guide d'Intégration Audio - LeafRunner

## 📋 Vue d'ensemble

Le système audio a été implémenté avec un **AudioManager** singleton qui gère tous les sons du jeu :
- 🎵 Musique de fond (en boucle)
- 🎤 Effets sonores (saut, course)

## ✅ Fichiers Créés/Modifiés

### Nouveaux fichiers :
- `Assets/Scripts/Managers/AudioManager.cs` - Gestionnaire central de tous les sons

### Fichiers modifiés :
- `Assets/Scripts/Player/PlayerController.cs` - Ajout des sons de saut et course
- `Assets/Scripts/Managers/GameManager.cs` - Ajout de la musique de fond

## 🎮 Configuration dans Unity

### Étape 1 : Créer l'AudioManager GameObject

1. Dans la **Hiérarchie**, clic droit → `Create Empty`
2. Renommer en **"AudioManager"**
3. Ajouter le script `AudioManager.cs` au GameObject
4. Assigner les clips audio :
   - **Background Music** : `Assets/Audio/music.mp3`
   - **Jump Sound** : `Assets/Audio/jump.mp3`
   - **Running Sound** : `Assets/Audio/running.mp3`
5. Ajuster les volumes :
   - **Music Volume** : 0.5 (par défaut)
   - **SFX Volume** : 0.7 (par défaut)

### Étape 2 : Vérifier les Configurations

#### Sur le GameObject Player :
- Le `PlayerController` est déjà configuré pour appeler l'AudioManager
- Aucune modification nécessaire

#### Sur le GameObject GameManager :
- Le `GameManager` est déjà configuré pour démarrer la musique
- Aucune modification nécessaire

## 🎵 Sons Implémentés

| Son | Déclencheur | Fichier | Type |
|-----|------------|---------|------|
| 🎵 Musique de fond | Au démarrage du jeu | `music.mp3` | Boucle continue |
| 🦘 Saut | Appui sur Espace/W/Z/Flèche Haut | `jump.mp3` | Effet ponctuel |
| 🏃 Course | Pendant le jeu (quand le joueur court) | `running.mp3` | Boucle continue |

## 📝 Utilisation de l'AudioManager

### Dans vos scripts :

```csharp
// Jouer un son de saut
if (AudioManager.Instance != null)
{
    AudioManager.Instance.PlayJumpSound();
}

// Démarrer la musique
AudioManager.Instance.PlayBackgroundMusic();

// Arrêter la musique
AudioManager.Instance.StopBackgroundMusic();

// Démarrer le son de course
AudioManager.Instance.PlayRunningSound();

// Arrêter le son de course
AudioManager.Instance.StopRunningSound();

// Changer le volume de la musique (0 à 1)
AudioManager.Instance.SetMusicVolume(0.5f);

// Changer le volume des effets sonores (0 à 1)
AudioManager.Instance.SetSFXVolume(0.7f);

// Couper/Réactiver tous les sons
AudioManager.Instance.ToggleMute(true); // mute
AudioManager.Instance.ToggleMute(false); // unmute
```

## 🎯 Fonctionnalités

### ✨ Pattern Singleton
- Une seule instance de l'AudioManager dans toute la scène
- Accès facile via `AudioManager.Instance`
- Persist entre les scènes avec `DontDestroyOnLoad`

### 🔊 Trois AudioSources
1. **musicSource** : Musique de fond en boucle
2. **sfxSource** : Effets sonores ponctuels (saut, collectibles)
3. **runningSource** : Son de course en boucle

### 🎚️ Contrôle du Volume
- Volume séparé pour la musique et les effets
- Ajustable via l'Inspector Unity
- Méthodes pour modifier en temps réel

## 🔧 Ajout de Nouveaux Sons

### Pour ajouter un son de collectible :

1. **Dans AudioManager.cs** :
```csharp
[Header("Effets Sonores")]
public AudioClip collectSound;

public void PlayCollectSound()
{
    if (collectSound != null && sfxSource != null)
    {
        sfxSource.PlayOneShot(collectSound);
    }
}
```

2. **Dans le script Collectible** :
```csharp
public void Collect()
{
    if (AudioManager.Instance != null)
    {
        AudioManager.Instance.PlayCollectSound();
    }
    // ... reste du code
}
```

3. **Dans Unity** :
   - Importer le fichier audio dans `Assets/Audio/`
   - Assigner dans l'Inspector de l'AudioManager

## 🐛 Dépannage

### Le son ne joue pas ?
- ✅ Vérifier que l'AudioManager existe dans la scène
- ✅ Vérifier que les clips audio sont assignés dans l'Inspector
- ✅ Vérifier que les volumes ne sont pas à 0
- ✅ Vérifier que le AudioListener est présent (généralement sur la Camera)

### Le son est trop fort/faible ?
- Ajuster les sliders **Music Volume** et **SFX Volume** dans l'Inspector
- Valeurs recommandées : 0.3 à 0.7

### Le son de course ne s'arrête pas ?
- Vérifier que `StopRunningSound()` est appelé dans `GameOver()`
- Vérifier les logs de debug dans la Console

## 🎮 Test

1. Lancer le jeu en mode Play
2. Cliquer sur "Démarrer" → La musique démarre
3. Le jeu commence → Le son de course démarre
4. Appuyer sur Espace → Le son de saut joue
5. Collision avec obstacle → Tous les sons s'arrêtent

## 📚 Ressources

### Sites pour télécharger des sons gratuits :
- [Freesound.org](https://freesound.org/) - Sons libres de droits
- [OpenGameArt.org](https://opengameart.org/) - Sons pour jeux vidéo
- [Incompetech](https://incompetech.com/) - Musiques libres
- [Zapsplat](https://www.zapsplat.com/) - Effets sonores

### Format recommandé :
- **Format** : MP3 ou OGG (compression)
- **Qualité** : 44100 Hz, Stereo
- **Durée** : 
  - Effets courts : < 1 seconde
  - Musique : 30-120 secondes (boucle)

## 💡 Bonnes Pratiques

1. **Nommer clairement** : `jump.mp3`, `collect.mp3`, etc.
2. **Organiser** : Tous les sons dans `Assets/Audio/`
3. **Volume équilibré** : Musique < Effets sonores
4. **Compression** : Utiliser MP3/OGG pour économiser l'espace
5. **Tests** : Vérifier dans différents environnements (casque, enceintes)

---

✅ **Le système audio est maintenant configuré et prêt à l'emploi !**
