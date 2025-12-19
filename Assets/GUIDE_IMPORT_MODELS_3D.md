# 🎨 GUIDE : Ajouter des Modèles 3D aux Obstacles

## Étape 1 : Obtenir des Modèles 3D

### Option A : Télécharger gratuitement
Sites recommandés :
- **Sketchfab** (https://sketchfab.com/feed) → Filtrer "Downloadable"
- **Quaternius** (http://quaternius.com) → Mega assets packs gratuits
- **Kenney.nl** (https://kenney.nl/assets) → Assets low poly gratuits
- **Unity Asset Store** → Section "Free"

### Option B : Utiliser des primitives Unity
- Pour débuter, tu peux combiner plusieurs cubes/sphères

## Étape 2 : Importer dans Unity

### 2.1 - Placer les fichiers
1. Dans l'**Explorateur Windows**, va dans ton dossier projet :
   ```
   C:\Users\brads\LeafRunner\Assets\Models\
   ```

2. Crée des sous-dossiers si besoin :
   ```
   Models/
   ├── Obstacles/     ← Mets tes rochers, arbres, etc. ici
   ├── Collectibles/  ← Mets tes pièces, feuilles ici
   └── Environment/   ← Mets tes plateformes, décors ici
   ```

3. **Copie-colle** tes fichiers .fbx ou .obj directement dans ces dossiers
   - Unity détectera automatiquement les nouveaux fichiers
   - Tu verras une barre de progression "Importing..."

### 2.2 - Vérifier l'import
1. Dans Unity, **Project** → `Assets/Models/Obstacles/`
2. **Clique sur ton modèle** (ex: Rock.fbx)
3. Dans l'**Inspector** :
   - Onglet **Model** :
     - Scale Factor : `1` (ajuste si trop grand/petit)
     - Mesh Compression : `Off`
   - Onglet **Rig** :
     - Animation Type : `None` (les obstacles ne bougent pas)
   - Clique **Apply**

## Étape 3 : Créer un Prefab avec le Modèle

### Méthode 1 : Remplacer un obstacle existant

#### A. Ouvrir le prefab existant
1. Dans **Project** → `Assets/Prefabs/Obstacles/`
2. **Double-clic** sur `Obstacle_Rock` → Mode édition prefab

#### B. Ajouter le modèle 3D
1. **Supprime** le cube primitif (Mesh Filter + Mesh Renderer)
2. Depuis **Project** → `Assets/Models/Obstacles/`, 
   **glisse-dépose** ton modèle 3D **sur** `Obstacle_Rock` dans la **Hierarchy**
3. Le modèle devient **enfant** de Obstacle_Rock

#### C. Ajuster la position
1. **Sélectionne** le modèle 3D dans la Hierarchy
2. Dans **Inspector** → **Transform** :
   ```
   Position : (0, 0, 0)
   Rotation : (0, 0, 0) ou ajuste selon ton modèle
   Scale : (1, 1, 1) ou ajuste la taille
   ```

#### D. Vérifier le Collider
1. **Sélectionne** `Obstacle_Rock` (le parent)
2. Dans **Inspector**, vérifie le **Box Collider** :
   - **Is Trigger** : ✅ Coché
   - **Center/Size** : Ajuste pour correspondre au modèle visuel
   - Astuce : Active **Gizmos** (en haut de Scene) pour voir le collider en vert

#### E. Sauvegarder
1. En haut de la Hierarchy, clique **"< Prefabs"** pour sortir du mode édition
2. C'est fait ! Ton prefab utilise maintenant le modèle 3D

### Méthode 2 : Créer un nouveau prefab from scratch

#### A. Dans la scène
1. **Clic droit** dans Hierarchy → **Create Empty** → Renomme "Obstacle_Tree"
2. **Glisse** ton modèle 3D depuis Project → **sur** "Obstacle_Tree"
3. Le modèle devient enfant

#### B. Ajouter les composants
1. **Sélectionne** `Obstacle_Tree` (le parent)
2. **Add Component** → **Box Collider** :
   - **Is Trigger** : ✅ Coché
   - Ajuste Center/Size
3. **Add Component** → Cherche `ObstacleAdvanced` :
   - **Obstacle Type** : Choisis Low/High/Wide
4. Dans **Inspector** en haut :
   - **Tag** : `Obstacle`
   - **Layer** : `Default`

#### C. Créer le prefab
1. Dans **Project**, crée le dossier si besoin : `Assets/Prefabs/Obstacles/`
2. **Glisse** `Obstacle_Tree` depuis la **Hierarchy** → vers le dossier **Project**
3. Unity crée le prefab (icône bleue)
4. Supprime `Obstacle_Tree` de la Hierarchy (il existe maintenant comme prefab)

## Étape 4 : Ajuster la Taille et l'Apparence

### Problème : Modèle trop grand/petit
**Solution 1** - Scale du modèle importé :
1. **Sélectionne** le fichier .fbx dans Project
2. **Inspector** → Onglet **Model** → **Scale Factor** : `0.5` (divise par 2)
3. **Apply** → Tous les prefabs utilisant ce modèle seront mis à jour

**Solution 2** - Scale dans le prefab :
1. Ouvre le prefab en édition
2. Sélectionne le modèle 3D enfant
3. **Transform** → **Scale** : `(2, 2, 2)` pour doubler la taille

### Problème : Modèle mal orienté
1. Sélectionne le modèle 3D enfant
2. **Transform** → **Rotation** :
   - `(0, 90, 0)` pour tourner de 90° sur Y
   - Ajuste jusqu'à ce que ça soit droit

### Ajouter un matériau
1. **Sélectionne** le modèle 3D enfant
2. Dans **Inspector** → **Mesh Renderer** → **Materials** :
   - Glisse ton material depuis `Assets/Materials/`
   - Ou clique le cercle pour en choisir un

## Étape 5 : Tester dans le Jeu

### A. Utiliser le nouveau prefab
1. Va dans ta scène
2. **Sélectionne** un de tes segments (ex: Segment_Simple)
3. **Double-clic** pour éditer le segment
4. Supprime l'ancien Obstacle_Rock
5. Glisse le nouveau prefab depuis Project

### B. Tester en Play Mode
1. **Clique** Play ▶️
2. Vérifie :
   - ✅ Le modèle apparaît correctement
   - ✅ La collision fonctionne (game over quand on touche)
   - ✅ La taille est appropriée (pas trop grand/petit)
   - ✅ On peut l'esquiver en changeant de lane

## Astuces Pro

### Organiser plusieurs modèles dans un prefab
Tu peux avoir plusieurs modèles dans un obstacle :
```
Obstacle_RockPile (parent avec collider + script)
├── Rock_01 (modèle 3D)
├── Rock_02 (modèle 3D)
└── Rock_03 (modèle 3D)
```

### Créer des variantes
1. **Clic droit** sur un prefab → **Create** → **Prefab Variant**
2. Change juste le modèle 3D ou la couleur
3. Le script/collider est hérité automatiquement

### Collider automatique
Si le modèle a une forme complexe :
1. Au lieu de **Box Collider**, utilise **Mesh Collider**
2. ⚠️ **Is Trigger** : Coché
3. ⚠️ **Convex** : Coché (requis pour les triggers)

## Checklist Finale

Avant de considérer un obstacle "fini" :

- [ ] Le modèle 3D est visible dans la scène
- [ ] Le collider (vert) englobe bien le modèle visuel
- [ ] Tag = "Obstacle" sur le parent
- [ ] Script `ObstacleAdvanced.cs` attaché avec le bon type
- [ ] Is Trigger = Coché sur le collider
- [ ] Testé en Play mode : collision fonctionne
- [ ] Taille appropriée (pas trop grand pour bloquer 3 lanes)
- [ ] Prefab sauvegardé dans `Assets/Prefabs/Obstacles/`

## Prochaine Étape

Une fois que tu as quelques obstacles avec des modèles 3D :
1. Crée des **variantes** (Obstacle_Low, Obstacle_High, Obstacle_Wide)
2. Place-les dans différents **segments**
3. Ajoute les segments dans **SegmentSpawner** → `segmentPrefabs[]`

---

**Questions ?**
- "Comment ajuster le collider exactement ?"
- "Mon modèle est invisible"
- "La collision ne fonctionne pas"
- "Je veux ajouter plusieurs modèles au même obstacle"
