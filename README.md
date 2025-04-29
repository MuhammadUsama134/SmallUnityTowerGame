Procedural Tower Builder – Touch to Create & Collapse
===================

Description:
------------
This is a simple tower stacking game made in Unity.
Players tap or click to spawn colored blocks and build a tower.
The camera moves up slightly as the tower grows taller.
If the tower tilts too much, it collapses and the game resets.

Controls:
---------
- Click (Mouse) or Tap (Touchscreen) to spawn a new block.

Implementation Details:
-----------------------
- Main Script: TowerManager.cs
  - Spawns a block at the clicked/tapped position.
  - Each block spawns slightly higher (spawnOffset = 1.1f).
  - Blocks are given random X and Z scales between 0.8 and 1.2.
  - Blocks get a random color using Random.ColorHSV().
  - A small rightward force (swayForce) is applied to all blocks to add instability.
  - The tilt of the tower is checked by measuring the angle between the bottom and top blocks.
  - If the tilt angle exceeds 30 degrees (maxTiltAngle), the tower is reset instantly.
  - The camera's Y-position updates dynamically based on the number of blocks (0.4 units per block).
  - Score is displayed using a TextMeshProUGUI element.

Platform:
---------
- Built for Android using Unity Hub 3.12.0 and Unity Editor 6(6000.0.36f1).
- Requires Android Build Support (SDK/NDK & OpenJDK).
