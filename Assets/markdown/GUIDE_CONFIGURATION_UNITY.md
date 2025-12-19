# 🎮 Guide de Configuration Unity - Leaf Runner

## 📋 Configuration de la Scène

### 1️⃣ Créer le Joueur

1. **Créer un GameObject vide** : `GameObject > Create Empty` → Nommer "Player"
2. **Ajouter un CharacterController** :
   - Sélectionner le Player
   - `Add Component` → Rechercher "Character Controller"
   - Régler les paramètres :
     - Center: (0, 1, 0)
     - Radius: 0.5
     - Height: 2
     - Skin Width: 0.08
3. **Ajouter le script PlayerController** :
   - Glisser-déposer `PlayerController.cs` sur le GameObject Player
   - Dans l'Inspector, assigner :
     - Character Controller → glisser le composant depuis le Player
     - Player Model → glisser votre modèle FBX ici (créer un child si nécessaire)
4. **Importer votre modèle FBX** :
   - Glisser votre personnage dans `Assets/Models/`
   - Créer un child dans Player : clic-droit sur Player > `Create Empty` → "PlayerModel"
   - Glisser votre FBX dans ce child
   - Ajuster la position/échelle si nécessaire
5. **Ajouter le Tag "Player"** :
   - Sélectionner le GameObject Player
   - En haut de l'Inspector → Tag → Add Tag → Créer "Player"
   - Réassigner le tag "Player" au GameObject

---

### 2️⃣ Créer la Caméra

1. **Sélectionner la Main Camera** (déjà dans la scène)
2. **Ajouter le script CameraFollow** :
   - Glisser-déposer `CameraFollow.cs` sur la Main Camera
   - Dans l'Inspector :
     - Target → glisser le GameObject Player
     - Offset → (0, 5, -7) (déjà défini par défaut)
     - Smooth Speed → 0.125
     - Rotation → (30, 0, 0)
3. **Positionner la caméra** :
   - Position : (0, 5, -7)
   - Rotation : (30, 0, 0)

---

### 3️⃣ Créer le GameManager

1. **Créer un GameObject vide** : `GameObject > Create Empty` → Nommer "GameManager"
2. **Ajouter le script GameManager** :
   - Glisser-déposer `GameManager.cs` sur le GameObject
3. **Assigner les références dans l'Inspector** :
   - Player Controller → glisser le GameObject Player
   - UI Manager → on va le créer juste après

---

### 4️⃣ Créer le SegmentSpawner

1. **Créer un GameObject vide** : `GameObject > Create Empty` → Nommer "SegmentSpawner"
2. **Ajouter le script SegmentSpawner** :
   - Glisser-déposer `SegmentSpawner.cs` sur le GameObject
3. **Configurer dans l'Inspector** :
   - Player Transform → glisser le GameObject Player
   - Segment Length → 10 (ajuster selon vos segments)
   - Visible Segments → 5
   - Segment Prefabs → on les créera plus tard

---

### 5️⃣ Créer l'UI (Canvas)

#### A. Créer le Canvas
1. **Créer un Canvas** : `GameObject > UI > Canvas`
2. **Configurer le Canvas** :
   - Canvas Scaler → UI Scale Mode : "Scale With Screen Size"
   - Reference Resolution : 1920x1080
   - Match : 0.5

#### B. Créer le Start Menu Panel
1. **Clic-droit sur Canvas** → `UI > Panel` → Nommer "StartMenuPanel"
2. **Ajouter un titre** :
   - Clic-droit sur StartMenuPanel → `UI > Text - TextMeshPro` → "TitleText"
   - Texte : "FOREST DASH"
   - Font Size : 72
   - Alignment : Center/Middle
   - Positionner en haut du panel
3. **Ajouter le bouton Start** :
   - Clic-droit sur StartMenuPanel → `UI > Button - TextMeshPro` → "StartButton"
   - Texte du bouton : "JOUER"
   - Positionner au centre
4. **Ajouter le texte High Score** :
   - Clic-droit sur StartMenuPanel → `UI > Text - TextMeshPro` → "HighScoreText"
   - Texte : "Meilleur Score: 0"
   - Positionner en bas

#### C. Créer le Game HUD Panel
1. **Clic-droit sur Canvas** → `UI > Panel` → Nommer "GameHUDPanel"
2. **Rendre transparent** :
   - Sélectionner GameHUDPanel → Image Component → Alpha = 0
3. **Ajouter le texte Score** :
   - Clic-droit sur GameHUDPanel → `UI > Text - TextMeshPro` → "ScoreText"
   - Texte : "Score: 0"
   - Font Size : 48
   - Positionner en haut à gauche
   - Alignment : Left/Top

#### D. Créer le Game Over Panel
1. **Clic-droit sur Canvas** → `UI > Panel` → Nommer "GameOverPanel"
2. **Ajouter le titre** :
   - Clic-droit sur GameOverPanel → `UI > Text - TextMeshPro` → "GameOverText"
   - Texte : "GAME OVER"
   - Font Size : 72
   - Color : Rouge
   - Positionner en haut
3. **Ajouter le score final** :
   - Clic-droit sur GameOverPanel → `UI > Text - TextMeshPro` → "FinalScoreText"
   - Texte : "Score: 0"
   - Font Size : 48
   - Positionner au centre
4. **Ajouter le meilleur score** :
   - Clic-droit sur GameOverPanel → `UI > Text - TextMeshPro` → "BestScoreText"
   - Texte : "Meilleur Score: 0"
   - Font Size : 36
5. **Ajouter le bouton Restart** :
   - Clic-droit sur GameOverPanel → `UI > Button - TextMeshPro` → "RestartButton"
   - Texte : "REJOUER"
6. **Ajouter le bouton Quit** :
   - Clic-droit sur GameOverPanel → `UI > Button - TextMeshPro` → "QuitButton"
   - Texte : "QUITTER"

#### E. Créer le UIManager
1. **Créer un GameObject vide** sur le Canvas : Clic-droit sur Canvas → `Create Empty` → "UIManager"
2. **Ajouter le script UIManager** :
   - Glisser-déposer `UIManager.cs` sur le GameObject
3. **Assigner TOUTES les références dans l'Inspector** :
   - Start Menu Panel → glisser StartMenuPanel
   - Game HUD Panel → glisser GameHUDPanel
   - Game Over Panel → glisser GameOverPanel
   - Score Text → glisser le ScoreText du HUD
   - High Score Text → glisser le HighScoreText du StartMenu
   - Final Score Text → glisser le FinalScoreText du GameOver
   - Best Score Text → glisser le BestScoreText du GameOver
   - Start Button → glisser le StartButton
   - Restart Button → glisser le RestartButton
   - Quit Button → glisser le QuitButton

4. **Retourner au GameManager** et assigner :
   - UI Manager → glisser le GameObject UIManager

---

### 6️⃣ Créer un Segment de Test Simple

1. **Créer un GameObject vide** : `GameObject > Create Empty` → Nommer "Segment_Simple"
2. **Ajouter un sol** :
   - Clic-droit sur Segment_Simple → `3D Object > Cube`
   - Renommer "Ground"
   - Scale : (10, 0.2, 10)
   - Position : (0, 0, 5)
   - Ajouter un Material vert dans `Assets/Materials/` → "GroundMaterial"
3. **Créer des marqueurs de voies (optionnel)** :
   - 3 cubes fins pour visualiser les lanes
   - Positions X : -3, 0, 3
4. **Créer un Prefab** :
   - Glisser Segment_Simple dans `Assets/Prefabs/Segments/`
   - Supprimer l'original de la scène

---

### 7️⃣ Créer un Obstacle Simple

1. **Créer un Cube** : `GameObject > 3D Object > Cube` → Nommer "Obstacle_Rock"
2. **Configurer** :
   - Scale : (1, 1, 1)
   - Ajouter un Material gris/marron
3. **Ajouter le script Obstacle** :
   - Glisser-déposer `Obstacle.cs` sur le cube
4. **Ajouter le Tag "Obstacle"** :
   - Tag → Add Tag → Créer "Obstacle"
   - Réassigner le tag
5. **Créer un Prefab** :
   - Glisser dans `Assets/Prefabs/Obstacles/`
   - Supprimer de la scène

---

### 8️⃣ Créer un Collectible Simple

1. **Créer une Sphère** : `GameObject > 3D Object > Sphere` → Nommer "Collectible_Leaf"
2. **Configurer** :
   - Scale : (0.5, 0.5, 0.5)
   - Ajouter un Material vert/jaune
3. **Modifier le Collider** :
   - Sphere Collider → Is Trigger : ✓ (coché)
4. **Ajouter le script Collectible** :
   - Glisser-déposer `Collectible.cs` sur la sphère
   - Point Value : 10
   - Rotate : ✓
   - Levitate : ✓
5. **Ajouter le Tag "Collectible"** :
   - Tag → Add Tag → Créer "Collectible"
   - Réassigner le tag
6. **Créer un Prefab** :
   - Glisser dans `Assets/Prefabs/Collectibles/`
   - Supprimer de la scène

---

### 9️⃣ Créer un Segment avec Obstacles

1. **Dupliquer Segment_Simple** → "Segment_Obstacles"
2. **Ajouter des obstacles** :
   - Glisser le prefab Obstacle_Rock dans le segment
   - Positionner sur les lanes (X: -3, 0, ou 3)
   - Position Y : au-dessus du sol
   - Position Z : varier entre 0 et 10
3. **Ajouter des collectibles** :
   - Glisser le prefab Collectible_Leaf
   - Positionner entre les obstacles
4. **Créer un Prefab** :
   - Glisser dans `Assets/Prefabs/Segments/`

---

### 🔟 Assigner les Segments au Spawner

1. **Sélectionner SegmentSpawner**
2. **Dans l'Inspector** :
   - Segment Prefabs → Size : 2
   - Element 0 → glisser Segment_Simple
   - Element 1 → glisser Segment_Obstacles

---

### 1️⃣1️⃣ Lumière et Environnement

1. **Directional Light** (déjà dans la scène) :
   - Rotation : (50, -30, 0)
   - Intensity : 1
   - Color : légèrement jaune/chaud
2. **Skybox** (optionnel) :
   - Window → Rendering → Lighting
   - Environment → Skybox Material : Choisir un skybox simple

---

## ✅ Vérifications Finales

### Tags nécessaires :
- ✓ Player
- ✓ Obstacle
- ✓ Collectible

### Hiérarchie de la scène :
```
- Main Camera (avec CameraFollow.cs)
- Directional Light
- Player (avec CharacterController + PlayerController.cs)
  └─ PlayerModel (votre FBX)
- GameManager (avec GameManager.cs)
- SegmentSpawner (avec SegmentSpawner.cs)
- Canvas
  ├─ StartMenuPanel
  ├─ GameHUDPanel
  ├─ GameOverPanel
  └─ UIManager (avec UIManager.cs)
```

---

## 🎮 Test de la Scène

1. **Appuyer sur Play** ▶️
2. **Vérifier** :
   - Le menu de démarrage s'affiche
   - Clic sur "JOUER" démarre le jeu
   - Le joueur avance automatiquement
   - A/D ou Flèches changent de voie
   - Espace fait sauter
   - S fait glisser
   - Les segments apparaissent
   - Les obstacles causent un Game Over
   - Les collectibles donnent des points
   - L'écran Game Over s'affiche

---

## 🐛 Problèmes Courants

### Le joueur ne bouge pas :
- Vérifier que le GameManager a démarré (clic sur Play dans le menu)
- Vérifier que PlayerController.cs est attaché
- Vérifier que CharacterController est assigné dans l'Inspector

### Pas de segments :
- Vérifier que SegmentSpawner a au moins 1 prefab assigné
- Vérifier que Player Transform est assigné
- Vérifier la console pour les erreurs

### L'UI ne s'affiche pas :
- Vérifier que toutes les références sont assignées dans UIManager
- Vérifier que le Canvas est en mode "Screen Space - Overlay"

### Les collisions ne fonctionnent pas :
- Vérifier les Tags (Player, Obstacle, Collectible)
- Vérifier que les colliders sont présents
- Pour les collectibles : Is Trigger doit être coché

---

## 🚀 Prochaines Étapes

Maintenant que le prototype fonctionne, vous pouvez :

1. **Créer plus de segments variés**
2. **Importer des assets low poly** (arbres, rochers, etc.)
3. **Ajouter des sons** (musique, effets)
4. **Créer des power-ups** (aimant, invincibilité)
5. **Améliorer les visuels** (post-processing, particules)
6. **Ajouter des animations** au personnage
7. **Créer un système de missions** ou d'objectifs

---

**Bon développement ! 🌲🏃‍♂️**
