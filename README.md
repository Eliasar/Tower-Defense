Intro

Your computer is being invaded by evil packets! Hackers are trying to send malicious code through your firewall, and you’re here to stop it. In this 2D, no-art tower defense, you will become the greatest System Administrator ever by blocking all virulent traffic heading to your open ports.

Gameplay Description

The player will be in a top-down (or bird’s-eye) view of each level. At the start of the level, enemies will spawn in a location designated for starting per level. These enemies will move toward their destination along a set path, also per level. The player will attempt to destroy the enemies by building towers that will target and shoot the enemies. The win-scenario is when all enemies have been eliminated.

Platform Information

We will be using Unity3D (http://unity3d.com/) as the game engine and C# as the scripting language.

Artistic Style Outline

Currently, the art style is one that is provided by the default objects available in the Unity3D Game Engine. Once an actual artist joins for the project, this can definitely be changed. <Pictures of current map, towers go here>

Systematic Breakdown of Components

Unity3D Game Engine

Includes these systems but is not limited to:
•	Collision
•	Particles
•	2D/3D Renderer
•	Graphical 3D Scene Designer
•	Game Object Hierarchy View

Save/Load System

The user is going to need a way to save information between sessions of play. For example, a list of levels they have beaten in order to determine what towers are available for building. This may end up being platform specific, because a web player is going to save information differently than Android, iOS, Mac, PC, and Linux.

UI System

A UI will be needed for the following things, but not limited to:
•	Menu
•	Level Picker
•	In-Level HUD

Tower Building System

In-game, the player will need to be able to place towers in certain areas. The Tower Building system will need to be created in order to give the player the ability to place towers in certain areas in the level.
In order to build these towers, the player will spend currency and select an available spot in designated areas. As the player destroys enemies, they are awarded a certain amount of currency that is used to purchase more towers. Only certain towers are usable per level.

Asset Breakdown


Art Assets

Assets will not need to be very detailed, as this is a 2D game built in a 3D game engine. We will be able to texture cubes and other primitive types, such as: spheres, capsules (spheres with a height), and planes. Dependent upon the art direction, it can range from pixel art to low-poly count models. Unity3D can import a wide array of file types, so we can reference their site for info on what programs can be used to create assets (http://unity3d.com/unity/workflow/asset-workflow).

Areas that will need art assets:
•	Enemies
•	Levels
•	UI/Menus
•	HUD
•	Particle effects

Text Assets

There will not need to be many text assets since there is no story, narration, or dialogue in game. Most of what is needed is: names for enemies, names for towers, menu option text, and credits. 
Minimal time is required for these.

Sound Assets

As per art assets, a variety of sounds can be imported into Unity3D. A quick explanation of music vs. sound: music is the long form sound that is looped; a sound is the short form that is usually only lasts about .25 to .5 seconds. A list of what might be needed:
•	Menu/Level Picker music
•	Menu/UI sounds
•	In-game sounds
o	Tower projectiles
o	Enemy explosion

Suggested Game Flow Diagram

The game will commence as thus: Menu -> Level Picker -> Level -> Results screen -> Level Picker, etc.

Suggested Project Timeline

Since this projected started out as a 1 Game A Month (http://www.onegameamonth.com/), the schedule will be scoped for only a month and will include only gameplay. If a month goes by and it is decided that the game will continue for an undetermined about of time, then the scope will change.

Additional Ideas and Possibilities

It’s possible that a story line could be thrown in here once the gameplay is polished enough or another person is on the project, but currently there is no story scoped for this. Another possibility would be art affecting gameplay and or design. It could be that the art style would drive level design and possibly change how the tower building system functions.
