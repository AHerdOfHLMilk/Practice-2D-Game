# Practice 2D Game — Mini War Base

A small strategy-base prototype built in Unity 6 (Universal 2D / URP) as a hands-on learning project, targeting Android.

What's in it so far:

- Pannable, pinch-zoomable top-down map camera (world-space pan, clamped to the map)
- Data-driven buildings: `ScriptableObject` definitions + prefab variants (Gold Mine, Farm, Barracks)
- Tick-based economy behind a `ResourceManager` singleton that publishes a C# `OnGoldChanged` event
- Timed building upgrades with a quadratic cost curve

Planned: HUD and building panel, tap-to-select, pooled troop marches, save/load with offline catch-up, a performance pass and an Android build.

Art: [Kenney Tower Defense (Top-Down)](https://kenney.nl/assets/tower-defense-top-down), CC0.
