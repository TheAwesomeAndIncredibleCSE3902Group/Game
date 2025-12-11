# Game

Hello! This is our game repo where we're making the video game 😎

AwesomeRPG is a Zelda clone that includes bonus turn-based RPG elements.

Interacting with an enemy sends the player to the turn-based realm, where they
    have to defeat them with a collection of moves. After this, the player
    can return to the overworld and continue their quest.

Equipment and allies must be found in the world as the player explores (or,
    if you're someone of weak morals, found in the debug room down the stairs
    from the starting room.)

Items like potions are picked up in the overworld and then used in battle.
    You can also see all the items you've collected and allies who have joined
    you by pressing Esc.

Overworld

|Control            | Keys          |
| -------------     | ------------- |
| Movement          | W, A, S, D, up, down, left, right |
| Quit Game         | Q             |
| Reset Game        | R             |
| Move to Game Over | Home          |
| Move to Win       | End           |
| Reset Game        | R             |
| Damage Player     | E             |
| Use Sword         | Z, N          |
| Use Bow           | 1             |
| Use Boomerang     | 2             |
| Use Super Sword   | 3             |
| Enter menu        | Esc           |

Combat

|Control            | Keys          |
| -------------     | ------------- |
| Move between Options | up, down, left, right |
| Select Option     | enter |

The UI is manipulated with the arrow keys for navigation and the enter button
    to make a selection.

Level can be swapped by clicking on the 4 quadrants on the screen.
- Bottom Right changes to the room on the top (second value decreases)
- Bottom Left changes to the room on the left (first value decreases)
- Top Right changes to the room on the bottom (second value increases)
- Top Left changes to the room on the right (first value increases)