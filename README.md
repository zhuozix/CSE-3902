CSE-3902
Team members: 

(Cotae) Adam Cote,  
(Kylek0)  Kyle Kauffman,  
(sethw216)  Seth Welch,  
(Leah16)  Shuangchen Zhou,  
(Adobely) Yao Lu,  
(zhuozix) Zhuozi Xie.

Project: Mario Replica

Sprint3

Controls:

Keyboard Controls:
WASD or arrow keys to control mario, move left and right, jump, and crouch.
N or Z to fire fireball (must be fire mario).
R to reset the game.
Q to quit the game.

Mouse Controls:
Left click left or right side of the game to move left or right.
Right click to jump.
Scroll mouse wheel to fire fireball (must be fire mario).
Press middle mouse or mouse button 1 (if your mouse has buttons) to crouch.

Special Notes:

There are some differences between the game and the original game, such as
1) The game camera can move backwards.
2) The items that come out after colliding with the question mark bricks are different from the original version.
3) The game's enemies may appear in different locations.
4) The distance that activated enemies move is not the same.

There are also some features that have not been added, such as
1) Hitting a brick on top will kill the enemy above the brick.
2) More death animations.
3) More animations during state transitions.


Known Bugs:

Collision detection can have some problems, such as
1) Getting stuck on the edge of a brick.
2) Not being hit by a monster.
3) No monster hit.
4) No successful detection of a brick below (Mario falling out of the screen).

There are some problems with Mario's state transitions, such as
1) The jump distance is too short.
2) The falling speed is too slow.
These two problems may be derived from problems with collisions.

Also, when Mario jumps on the turtle, Mario bounces up. But sometimes the bouncing up is further than expected.

