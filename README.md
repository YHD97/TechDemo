# TechDemo
# Map Generate:
## 1. Random Roguelike Room Generate
It uses a randomised way of surviving the room. After he generates the first room, it is randomly extended up, down, left and right, in four ways. A box collider in the centre detects whether a room already exists in the chosen direction. Once the room has been generated, a door is added or not depending on the chosen direction.

## 2. Cellular Automata Generator：
This generation method first divides the specified location into a grid area of 0 and 1, then generates rocks at the 1's and open space at the 0's. Then, according to the rules, if a cell has less than two neighbours around it then it dies. If there are two or three neighbours around it then it does not quadruple. If a living cell has three neighbours around it then it dies. If a dead cell is surrounded by three living neighbours then it will live. Finally a cave is created. This map generation method will generate random rocks, and enemies in a given room. It has been optimised to remove the wall generation and keep the rocks in the middle. The enemies will then be randomly generated in the open space.

# Agent:
## 1. enemy1：
Enemy 1 is a fixed-point cruise implemented using the waypoint algorithm, which will patrol four points and wait two seconds to stop at each point. And it will track the player when the distance between the player and it is less than 30.

## 2. enemy2：
Enemy 2 is written using a behavioural tree algorithm, which patrols randomly at a fixed location and then tracks the player when the player's location and it are less than 30.


# Player:
Players need to dodge against enemies and then reach the final room.

# Cameras:
The camera's view moves more with the room, and if the player goes to another room, the camera's view changes to the next room.

# Scene:
The room is automatically generated when you enter the scene. If you want to change the map, you can press the space key.

# Video URL：
https://youtu.be/sNpvMoLyEsE

# Reference:
1. https://gamedevelopment.tutsplus.com/tutorials/generate-random-cave-levels-using-cellular-automata--gamedev-9664
2. https://www.youtube.com/watch?v=aVf3awPrVPE




