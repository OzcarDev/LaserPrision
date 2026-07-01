\# Technical Documentation



\## Architecture Overview



The project is organized into independent gameplay systems with clearly defined responsibilities.



The main design goals were:



\* Readability

\* Modularity

\* Maintainability

\* Low coupling

\* Reusability



Communication between systems is handled primarily through C# events.



\---



\# Core



\## GameManager



Responsible for:



\* Managing game states

\* Starting sessions

\* Ending sessions

\* Broadcasting session reset events



\### Events



```csharp

Action<GameState> GameStateChanged;

Action GameSessionReset;

```



\---



\# Gameplay



\## ScoreManager



Tracks:



\* Current Score

\* Survival Time



Broadcasts:



```csharp

Action<int, float> ScoreChanged;

Action<float> TimeChanged;

```



Used by:



\* HUDController

\* DifficultyManager



\---



\## DifficultyManager



Evaluates progression based on elapsed survival time.



Uses:



```text

DifficultySettings (ScriptableObject)

```



Broadcasts:



```csharp

Action<DifficultyLevel> DifficultyChanged;

```



Used by:



\* LaserSpawner



\---



\## GameArea



Provides:



```csharp

ClampPosition()

GetRandomPosition()

```



Responsibilities:



\* Restrict player movement

\* Provide valid random spawn positions



\---



\# Player



\## PlayerInputReader



Responsible only for reading input.



Broadcasts:



```csharp

InvulnerabilityPressed

```



Provides:



```csharp

MoveInput

```



\---



\## PlayerMovement



Responsibilities:



\* CharacterController movement

\* Velocity calculation

\* Area clamping



Depends on:



\* PlayerInputReader

\* GameArea



\---



\## PlayerHealth



Responsibilities:



\* Life management

\* Damage handling

\* Death detection



Implements:



```csharp

IDamageable

```



Broadcasts:



```csharp

LivesChanged

Damaged

Died

```



Supports:



\* Session Reset

\* Invulnerability Integration



\---



\## PlayerInvulnerability



Responsibilities:



\* Temporary immunity

\* Cooldown management

\* Ability state tracking



Broadcasts:



```csharp

InvulnerabilityStarted

InvulnerabilityEnded

CooldownStarted

CooldownFinished

```



\---



\## PlayerAnimationController



Responsibilities:



\* Animator parameter updates

\* Character visual rotation



Keeps presentation logic separated from gameplay logic.



\---



\# Hazards



\## Laser



Handles:



\* Warning phase

\* Active phase

\* Pool release



Implements:



```csharp

IPoolable

```



\---



\## HazardDamage



Generic damage component.



Responsibilities:



\* Detect trigger collisions

\* Apply damage through IDamageable



This component is reusable by any future hazard.



\---



\## LaserSpawner



Spawns:



\* Random lasers

\* Targeted lasers



Responds to:



```csharp

DifficultyChanged

GameStateChanged

```



\---



\## LaserPool



Generic pooling implementation used by laser hazards.



Benefits:



\* Reduced allocations

\* Reduced instantiations

\* Better runtime performance

