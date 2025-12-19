# 🎬 GUIDE : Animator Transitions Rapides

## Problème : Animation de Slide qui met du temps à démarrer

### Cause
Par défaut, Unity ajoute des transitions avec **Has Exit Time** coché et **Transition Duration** élevé (0.25 sec).
Résultat : délai visible entre l'input et l'animation.

---

## Solution : Transitions Instantanées

### Étape 1 : Ouvrir l'Animator Controller

1. **Project** → `Assets/Animations/PlayerAnimator`
2. **Double-clic** → Fenêtre Animator s'ouvre
3. Tu vois les états : **Run**, **Roll**, **Death**

### Étape 2 : Transition Run → Roll (Slide)

#### Sélectionner la transition
1. **Clique** sur la **flèche blanche** allant de **Run** vers **Roll**
2. L'Inspector montre les paramètres de cette transition

#### Réglages pour slide instantané
```
┌─────────────────────────────────────┐
│ Settings                            │
├─────────────────────────────────────┤
│ Has Exit Time        ❌ DÉCOCHER    │
│ Fixed Duration       ✅ COCHER      │
│ Transition Duration  0.05           │ ← Presque instantané
│ Transition Offset    0              │
│ Interruption Source  None           │
└─────────────────────────────────────┘

┌─────────────────────────────────────┐
│ Conditions                          │
├─────────────────────────────────────┤
│ Slide                               │ ← Doit être présent
└─────────────────────────────────────┘
```

**Points critiques :**
- **Has Exit Time** = ❌ **DÉCOCHÉ** → Pas d'attente de fin d'animation Run
- **Transition Duration** = `0.05` → Transition quasi-instantanée (5 centisecondes)

### Étape 3 : Transition Roll → Run (Retour)

#### Sélectionner la transition
1. **Clique** sur la **flèche** allant de **Roll** vers **Run**

#### Réglages pour retour fluide
```
┌─────────────────────────────────────┐
│ Settings                            │
├─────────────────────────────────────┤
│ Has Exit Time        ✅ COCHER      │ ← On veut finir l'animation Roll
│ Exit Time            0.85           │ ← À 85% de l'animation Roll
│ Fixed Duration       ✅ COCHER      │
│ Transition Duration  0.1            │ ← Retour fluide
│ Transition Offset    0              │
└─────────────────────────────────────┘

┌─────────────────────────────────────┐
│ Conditions                          │
├─────────────────────────────────────┤
│ (vide)                              │ ← Pas de condition, juste exit time
└─────────────────────────────────────┘
```

### Étape 4 : Même chose pour Jump

Si le saut est aussi lent, applique les mêmes réglages :

**Run → Roll (Jump)** :
- Has Exit Time : ❌
- Transition Duration : 0.05
- Condition : Jump

**Roll → Run (Retour du Jump)** :
- Has Exit Time : ✅
- Exit Time : 0.85
- Transition Duration : 0.1

---

## Paramètres de l'Animation Roll

Si l'animation Roll elle-même est trop lente :

1. Dans **Animator**, **clique sur l'état "Roll"** (le rectangle)
2. Dans **Inspector** :
```
┌─────────────────────────────────────┐
│ Motion : Roll (Animation Clip)      │
├─────────────────────────────────────┤
│ Speed : 1.5                         │ ← Accélère l'animation
│ Multiplier : Parameter (vide)       │
│ Mirror : ❌                          │
│ Foot IK : ❌                         │
└─────────────────────────────────────┘
```

**Speed = 1.5** → L'animation joue 50% plus vite
**Speed = 2** → L'animation joue 2x plus vite

---

## Paramètres de l'Animator (Global)

Dans la fenêtre **Animator** :

1. **Clique** sur l'onglet **Parameters** (à gauche)
2. Tu devrais voir :
   ```
   Jump  (Trigger)
   Slide (Trigger)
   Die   (Trigger)
   ```

3. Vérifie qu'ils sont bien de type **Trigger** (icône éclair)
   - Si c'est des **Bool** → Supprimer et recréer comme Trigger

---

## Schéma de l'Animator Idéal

```
         ┌──────────┐
    ┌───▶│   Run    │◀─────┐
    │    │ (défaut) │      │
    │    └──────────┘      │
    │                      │
    │                      │ Has Exit Time ✅
    │                      │ Exit Time 0.85
    │                      │ Duration 0.1
    │                      │
    │                      │
    │    ┌──────────┐      │
    │    │   Roll   │──────┘
    │    │(Slide/J.)│
    └────│  Speed:  │
         │   1.5    │
         └──────────┘
              ▲
              │
              │ Has Exit Time ❌
              │ Duration 0.05
              │ Trigger: Slide
              │
         (Input S)
```

---

## Test dans Play Mode

### Avant réglages (problème)
- Appuie **S**
- ⏱️ Délai de ~0.3 sec
- Animation Roll démarre enfin

### Après réglages (solution)
- Appuie **S**
- ⚡ Animation Roll démarre IMMÉDIATEMENT
- Aucun délai visible

---

## Checklist de Vérification

Dans l'Animator Controller, pour chaque transition déclenchée par input (Slide, Jump) :

- [ ] Has Exit Time = ❌ Décoché
- [ ] Transition Duration = 0.05 ou moins
- [ ] Condition = Le bon Trigger (Slide/Jump)
- [ ] Fixed Duration = ✅ Coché

Pour les transitions de retour automatique :

- [ ] Has Exit Time = ✅ Coché
- [ ] Exit Time = 0.8 - 0.9 (80-90% de l'animation)
- [ ] Transition Duration = 0.1
- [ ] Conditions = (vide)

---

## Problèmes Fréquents

### "L'animation ne joue toujours pas immédiatement"
→ Vérifie que **Has Exit Time** est bien **DÉCOCHÉ**

### "L'animation se répète en boucle"
→ Dans l'état Roll, décoche **Loop Time** dans l'Animation Clip

### "L'animation revient trop tôt à Run"
→ Augmente **Exit Time** de la transition Roll → Run (ex: 0.9 au lieu de 0.8)

### "Le joueur reste bloqué en Roll"
→ Vérifie qu'il y a bien une transition **Roll → Run**

---

## Bonus : Animation Death Instantanée

Si tu veux que la mort soit immédiate aussi :

**Run → Death** :
- Has Exit Time : ❌
- Transition Duration : 0
- Condition : Die

**Death → (aucun retour)** : Pas de transition, c'est la fin !

---

## Prochaine Étape

Une fois les transitions réglées :
- Teste en Play Mode
- Le slide devrait être instantané
- Si satisfait → Passe aux modèles 3D ou autres améliorations
