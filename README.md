# \# Laser Prison

# 

# Technical test developed in Unity.

# 

# Objective: Survive as long as possible avoiding lasers.

# 

# \## Controls

# | Action | Input |

# | :--- | :---: |

# | Move | WASD / Left Stick |

# | Invulnerability | Space/Button South |





# \## Gameplay

# 

# \* Survive as long as possible.

# \* Avoid laser hazards.

# \* Gain score over time.

# \* Use invulnerability strategically.

# \* Difficulty increases automatically as survival time grows.

# 

# \## Features

# 

# \### Core Systems

# 

# \* Game State Management

# \* Event-Driven Architecture

# \* Session Reset System

# \* Difficulty Progression

# \* Score System

# 

# \### Player Systems

# 

# \* Character Controller Movement

# \* Movement Clamped to Play Area

# \* Invulnerability Ability

# \* Health System

# \* Animation Controller

# 

# \### Hazard Systems

# 

# \* Laser Warnings

# \* Random Laser Spawning

# \* Targeted Laser Spawning

# \* Generic Damage System

# \* Object Pooling

# 

# \## Architecture

# 

# The project follows a modular architecture based on events and single-responsibility components.

# 

# Examples:

# 

# \* `GameManager` manages game flow.

# \* `PlayerHealth` manages player lives.

# \* `DifficultyManager` evaluates progression.

# \* `LaserSpawner` reacts to difficulty changes.

# \* `ScoreManager` tracks score and survival time.

# 

# Systems communicate primarily through events instead of direct references whenever possible.

# 

# \## Project Structure

# 

# ```text

# Assets

# ├── Core

# ├── Gameplay

# ├── Hazards

# ├── Interfaces

# ├── Player

# ├── ScriptableObjects

# └── UI

# ```

# 

# \## Technical Notes

# 

# \* Unity CharacterController movement.

# \* Generic object pool implementation.

# \* ScriptableObject-based difficulty configuration.

# \* Event-driven game flow.

# \* Inspector-assigned dependencies.

# \* Minimal use of runtime object lookups.

# 

# \## Author

# 

# Oscar Núñez Hernández

# Gameplay / Unity Developer



