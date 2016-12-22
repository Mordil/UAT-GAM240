# UAT-GAM240

This was a 5-week long project for the [University of Advancing Technology's](http://www.uat.edu/) "Game Engine Programming I" (GAM240) course.

This game is a 3D fantasy "arena" combat, where 3 AI enemies are spawned that cast fireballs at the player, and the player has access to a sword pickup and fireballs to attack the enemies with. After all 3 have died, the player has won the match.

Each week added new features and focused on a specific system with unity.

## Course Description

> This course introduces the fundamentals of game engine programming by customizing and enhancing an existing framework. Students will learn the core concepts of engine programming. Symptoms of taking Game Engine Programming I may include a greater understanding of frame rates, synchronization, timing, 2D and/or 3D graphics rendering, timed animation, user input, multiplayer, physics, collision detection and the most common algorithms used in game development. Many of these fundamentals will be implemented into a working engine from which playable games may be developed.

*Note: The course description was misleading, at least to me, in that it would be engine __tool__ development, and not just game development using an engine and its built-in systems.*

## Course Outline

* Week 1
  * Root Motion
  * Character controller programming (forward/backward, strafing movement)
  * Camera rotation from Mouse input
  * Physics on character (gravity)
  * Basic level geometry for verifying/testing player physics & gravity
  * Extra feature: Jumping
* Week 2
  * Weapons & Item (health) pickups
  * Simple Health UI
  * Target dummy that can be killed with the health system implemented
  * Focus on OOP prinicples (inheritance) and good coding standards of having `<summary>` tags on at least all public methods, properites, and fields.
  * Focus on inspector structure of component items.
  * Bonus feature: Spellcasting & VFX
* Week 3
  * AI movement with NavMesh
  * AI units utilizing the same components as the player, aside from an `AIInputController`
  * AI attacking the player
  * Continued focus on OOP principles and good coding standards
* Week 4
  * UI for player
    * Health
    * Equipped weapon
  * UI for enemies
    * Health
  * Pausing the game, with an associated pause screen
  * A main menu that is loaded at launch
  * Scene transitions
  * Win/lose conditions with associated end-of-level screens
  * Continued focus on OOP principles and good coding standards
* Week 5
  * Audio
    * Music - background and playing continuously
    * SFX - minimum requirement of 5 different triggered SFX.
  * Settings menu for controlling video & audio settings
    * Must be available at main menu and pause menu
    * Master, Music, SFX, and Ambient sound track levels
    * Resolution, Fullscreen, and Quality fields
  * PlayerPrefs to save & load settings
  * VFX with particle systems
  * Performance tweaks
    * Must run at 60+ FPS
  * Bonus feature: Vsync video setting

## Features
* [Mixamo](https://www.mixamo.com/store/#/) animations & models
* Unity Sytems:
  * [Profiler](https://docs.unity3d.com/Manual/ProfilerWindow.html)
  * [AudioMixer](https://docs.unity3d.com/Manual/AudioMixer.html)
  * [NavMesh](https://docs.unity3d.com/Manual/nav-BuildingNavMesh.html)
  * [Animation Events](https://docs.unity3d.com/Manual/AnimationEventsOnImportedClips.html)
  * [PlayerPrefs](https://docs.unity3d.com/ScriptReference/PlayerPrefs.html)
  * [Particle Systems](https://docs.unity3d.com/ScriptReference/ParticleSystem.html)
  * [UI](https://docs.unity3d.com/Manual/UISystem.html)

## License

All sourced assets are under their respective licenses, and the following only applies to my code.

----

MIT License

Copyright (c) 2016 Nathan "Mordil" Harris

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
