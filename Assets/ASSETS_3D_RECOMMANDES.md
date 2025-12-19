# 🎨 Assets 3D Recommandés pour Leaf Runner

## Pack Complet Recommandé

### **Quaternius - Ultimate Nature Pack**
🔗 http://quaternius.com/packs.html

**Contient :**
- Rochers (parfait pour obstacles)
- Arbres variés
- Buissons
- Plateformes en bois
- Troncs, champignons
- **Format : .FBX** ✅
- **100% Gratuit** ✅
- **Licence : CC0** (utilisation libre)

---

## Assets par Catégorie

### 🪨 Obstacles (Low - sauter par-dessus)
- **Rochers** → Quaternius Nature Pack
- **Troncs couchés** → Kenney Nature Kit
- **Buissons épais** → Quaternius

### 🌲 Obstacles (High - roll en dessous)
- **Branches horizontales** → Créer avec troncs
- **Lianes** → Unity Asset Store "Jungle Pack"
- **Portiques en bois** → Quaternius

### 🚧 Obstacles (Wide - 3 lanes bloquées)
- **Barrières en bois** → Kenney
- **Gros rochers** → Quaternius (scale x3)
- **Arbres tombés** → Combiner plusieurs troncs

### 💰 Collectibles
- **Pièces d'or** → Kenney "Coins"
- **Gemmes** → Sketchfab (chercher "low poly gem")
- **Feuilles** → Utiliser les feuilles de Quaternius rotations

### 🏔️ Plateformes
- **Plateformes en bois** → Quaternius
- **Rochers plats** → Quaternius

---

## Téléchargement Étape par Étape

### Quaternius Ultimate Nature Pack

1. **Va sur** : http://quaternius.com/packs.html
2. **Scroll** jusqu'à "Ultimate Nature Pack"
3. **Clique** "Download" (pas de compte nécessaire)
4. **Extrais** le fichier .zip téléchargé
5. **Tu trouveras** :
   ```
   UltimateNaturePack/
   ├── FBX/           ← Utilise ce dossier !
   │   ├── Rock_01.fbx
   │   ├── Rock_02.fbx
   │   ├── Tree_01.fbx
   │   └── ...
   ├── OBJ/           ← Ignore
   └── Textures/      ← Unity les détecte auto avec .FBX
   ```

6. **Copie le dossier FBX entier** dans :
   ```
   C:\Users\brads\LeafRunner\Assets\Models\Quaternius\
   ```

7. **Dans Unity**, ils apparaîtront automatiquement dans :
   ```
   Project → Assets/Models/Quaternius/
   ```

---

## Configuration Import Unity

Quand tu ajoutes des .FBX dans ton projet :

### Réglages par défaut (généralement OK)
1. **Sélectionne** le .FBX dans Project
2. **Inspector** → Onglet **Model** :
   ```
   Scale Factor : 1
   Mesh Compression : Off
   Read/Write : ✅ (important pour colliders)
   Optimize Mesh : ✅
   Generate Colliders : ❌ (on les fera manuellement)
   ```
3. **Inspector** → Onglet **Rig** :
   ```
   Animation Type : None (obstacles ne bougent pas)
   ```
4. **Inspector** → Onglet **Materials** :
   ```
   Location : Use Embedded Materials (par défaut)
   ```
5. **Clique Apply**

### Si le modèle est trop grand/petit
- Change **Scale Factor** : 
  - `0.1` = divise par 10
  - `10` = multiplie par 10
  - Teste en glissant le modèle dans la scène

---

## Checklist Téléchargement

Avant d'importer dans Unity :

- [ ] Format : .FBX ou .OBJ ✅
- [ ] Style : Low poly (meilleure performance) ✅
- [ ] Licence : Vérifier utilisation commerciale OK
- [ ] Fichier extrait du .zip
- [ ] Textures incluses (souvent dans dossier séparé)

---

## Alternative : Unity Asset Store

### Méthode directe (plus facile)

1. **Dans Unity** → Menu **Window** → **Asset Store**
2. **Cherche** : "free low poly nature"
3. **Filtre** : Prix → Free
4. **Clique** sur un pack qui te plaît
5. **Clique** "Add to My Assets" (nécessite compte Unity gratuit)
6. **Clique** "Import" → Sélectionne tout → **Import**
7. Les assets vont directement dans `Assets/`

### Packs gratuits recommandés :
- **"Low Poly Style - Trees"** par 3D Props
- **"Free Rocks Pack"** par BITGEM
- **"Simple Wooden Props"** par Kristian Studios

---

## 🎨 Bonus : Créer des Variantes de Couleur

Si tu télécharges un seul rocher mais veux plusieurs obstacles :

1. Crée plusieurs matériaux :
   - `Material_Rock_Grey` (gris)
   - `Material_Rock_Brown` (marron)
   - `Material_Rock_Green` (vert mousse)

2. Utilise le même modèle 3D avec différents matériaux

3. Résultat : 1 modèle = 3 obstacles visuellement différents !

---

## Prochaine Étape

Une fois téléchargés :
→ Dis-moi "J'ai téléchargé Quaternius, comment je l'importe ?"
→ Ou "J'ai des .FBX dans mon dossier téléchargements"

Et je t'aiderai à les intégrer dans le jeu étape par étape !
