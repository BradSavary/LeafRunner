# ✅ Checklist de Configuration - Leaf Runner

## 📋 Configuration Initiale Unity

### Étape 1 : Importer le Personnage
- [ ] Placer le fichier FBX dans `Assets/Models/`
- [ ] Vérifier que le modèle s'importe correctement
- [ ] Ajuster l'échelle si nécessaire (Scale Factor)

### Étape 2 : Créer le Joueur
- [ ] Créer GameObject vide "Player" (position: 0, 0, 0)
- [ ] Ajouter component CharacterController
  - [ ] Center: (0, 1, 0)
  - [ ] Radius: 0.5
  - [ ] Height: 2
- [ ] Ajouter script PlayerController.cs
- [ ] Créer child "PlayerModel" et y placer le FBX
- [ ] Assigner dans Inspector :
  - [ ] Character Controller
  - [ ] Player Model (le child)
- [ ] Ajouter Tag "Player" au GameObject Player

### Étape 3 : Configurer la Caméra
- [ ] Sélectionner Main Camera
- [ ] Ajouter script CameraFollow.cs
- [ ] Assigner Target → Player
- [ ] Position caméra : (0, 5, -7)
- [ ] Rotation caméra : (30, 0, 0)

---

## 🎮 Configuration des Managers

### Étape 4 : GameManager
- [ ] Créer GameObject vide "GameManager"
- [ ] Ajouter script GameManager.cs
- [ ] Laisser vide pour l'instant (on assignera après l'UI)

### Étape 5 : SegmentSpawner
- [ ] Créer GameObject vide "SegmentSpawner"
- [ ] Ajouter script SegmentSpawner.cs
- [ ] Assigner Player Transform → Player
- [ ] Segment Length : 10
- [ ] Visible Segments : 5

---

## 🖼️ Configuration de l'Interface (UI)

### Étape 6 : Créer le Canvas
- [ ] `GameObject > UI > Canvas`
- [ ] Canvas Scaler :
  - [ ] UI Scale Mode: Scale With Screen Size
  - [ ] Reference Resolution: 1920x1080
  - [ ] Match: 0.5

### Étape 7 : Start Menu Panel
- [ ] Créer Panel "StartMenuPanel"
- [ ] Ajouter Text-TMP "TitleText" : "LEAF RUNNER" (Font Size: 72)
- [ ] Ajouter Button-TMP "StartButton" : "JOUER"
- [ ] Ajouter Text-TMP "HighScoreText" : "Meilleur Score: 0"

### Étape 8 : Game HUD Panel
- [ ] Créer Panel "GameHUDPanel"
- [ ] Rendre transparent (Image Alpha = 0)
- [ ] Ajouter Text-TMP "ScoreText" : "Score: 0" (coin haut-gauche)

### Étape 9 : Game Over Panel
- [ ] Créer Panel "GameOverPanel"
- [ ] Ajouter Text-TMP "GameOverText" : "GAME OVER" (rouge, 72)
- [ ] Ajouter Text-TMP "FinalScoreText" : "Score: 0" (48)
- [ ] Ajouter Text-TMP "BestScoreText" : "Meilleur Score: 0" (36)
- [ ] Ajouter Button-TMP "RestartButton" : "REJOUER"
- [ ] Ajouter Button-TMP "QuitButton" : "QUITTER"

### Étape 10 : UIManager
- [ ] Créer GameObject vide "UIManager" (child du Canvas)
- [ ] Ajouter script UIManager.cs
- [ ] Assigner TOUTES les références :
  - [ ] Start Menu Panel
  - [ ] Game HUD Panel
  - [ ] Game Over Panel
  - [ ] Score Text (du HUD)
  - [ ] High Score Text (du StartMenu)
  - [ ] Final Score Text (du GameOver)
  - [ ] Best Score Text (du GameOver)
  - [ ] Start Button
  - [ ] Restart Button
  - [ ] Quit Button

### Étape 11 : Retour au GameManager
- [ ] Sélectionner GameManager
- [ ] Assigner UI Manager → UIManager (du Canvas)

---

## 🧱 Création des Prefabs

### Étape 12 : Segment Simple
- [ ] Créer GameObject vide "Segment_Simple"
- [ ] Ajouter Cube child "Ground"
  - [ ] Scale : (10, 0.2, 10)
  - [ ] Position : (0, 0, 5)
- [ ] Créer Material vert dans `Assets/Materials/`
- [ ] Appliquer le material au Ground
- [ ] Glisser Segment_Simple dans `Assets/Prefabs/Segments/`
- [ ] Supprimer de la scène

### Étape 13 : Obstacle
- [ ] Créer Cube "Obstacle_Rock"
- [ ] Scale : (1, 1, 1)
- [ ] Créer Material gris/marron
- [ ] Ajouter script Obstacle.cs
- [ ] Créer Tag "Obstacle" et l'assigner
- [ ] Glisser dans `Assets/Prefabs/Obstacles/`
- [ ] Supprimer de la scène

### Étape 14 : Collectible
- [ ] Créer Sphere "Collectible_Leaf"
- [ ] Scale : (0.5, 0.5, 0.5)
- [ ] Créer Material vert/jaune
- [ ] Sphere Collider → Is Trigger : ✓
- [ ] Ajouter script Collectible.cs
- [ ] Créer Tag "Collectible" et l'assigner
- [ ] Glisser dans `Assets/Prefabs/Collectibles/`
- [ ] Supprimer de la scène

### Étape 15 : Segment avec Obstacles
- [ ] Dupliquer prefab Segment_Simple → "Segment_Obstacles"
- [ ] Ouvrir le prefab (double-clic)
- [ ] Glisser 2-3 Obstacle_Rock sur différentes lanes
  - [ ] X : -3 (gauche), 0 (centre), ou 3 (droite)
  - [ ] Y : 1 (au-dessus du sol)
- [ ] Glisser 2-3 Collectible_Leaf entre les obstacles
- [ ] Sauvegarder le prefab

### Étape 16 : Assigner au Spawner
- [ ] Sélectionner SegmentSpawner
- [ ] Segment Prefabs → Size: 2
  - [ ] Element 0 : Segment_Simple
  - [ ] Element 1 : Segment_Obstacles

---

## 🏷️ Vérification des Tags

### Étape 17 : Tags Nécessaires
- [ ] Tag "Player" existe et assigné au Player
- [ ] Tag "Obstacle" existe et assigné aux obstacles
- [ ] Tag "Collectible" existe et assigné aux collectibles

---

## 💡 Configuration de la Lumière

### Étape 18 : Éclairage
- [ ] Directional Light :
  - [ ] Rotation : (50, -30, 0)
  - [ ] Intensity : 1
  - [ ] Color : légèrement jaune/chaud
- [ ] (Optionnel) Window > Rendering > Lighting > Skybox

---

## 🎮 TEST FINAL !

### Étape 19 : Premier Test
- [ ] Appuyer sur ▶️ PLAY
- [ ] Vérifications :
  - [ ] Le menu Start s'affiche
  - [ ] Clic sur "JOUER" démarre le jeu
  - [ ] Le joueur avance automatiquement
  - [ ] A/D changent de voie
  - [ ] Espace fait sauter
  - [ ] S fait glisser
  - [ ] Les segments apparaissent
  - [ ] Les obstacles causent Game Over
  - [ ] Les collectibles donnent des points
  - [ ] L'écran Game Over s'affiche
  - [ ] "REJOUER" redémarre le jeu

---

## 🐛 Debugging

### Si quelque chose ne fonctionne pas :

#### Erreurs dans la Console ?
- [ ] Lire le message d'erreur
- [ ] Double-cliquer pour aller à la ligne du code
- [ ] Vérifier que toutes les références sont assignées

#### Le joueur ne bouge pas ?
- [ ] Vérifier que CharacterController est présent
- [ ] Vérifier que PlayerController est attaché
- [ ] Cliquer sur "JOUER" dans le menu

#### Pas de segments ?
- [ ] Vérifier qu'au moins 1 prefab est assigné au Spawner
- [ ] Vérifier que Player Transform est assigné

#### UI ne s'affiche pas ?
- [ ] Vérifier toutes les références dans UIManager
- [ ] Vérifier que GameManager a UIManager assigné

---

## 🎉 Félicitations !

Si tout fonctionne, vous avez :
✅ Un prototype jouable d'endless runner
✅ Un système de score fonctionnel
✅ Un système de segments infini
✅ Des obstacles et collectibles
✅ Une UI complète

### 🚀 Prochaines Étapes

Maintenant que le prototype fonctionne :

1. **Créer plus de segments** (variations)
2. **Importer des assets low poly** (voir RESSOURCES_GRATUITES.md)
3. **Ajouter des sons** (effets et musique)
4. **Améliorer les visuels** (materials, couleurs)
5. **Ajouter des animations** au personnage (Mixamo)

**Consultez le fichier `RECAPITULATIF_PROJET.md` pour plus d'idées !**

---

**Bon jeu ! 🎮🌲**
