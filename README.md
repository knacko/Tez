# Tez
A tesselation puzzle game inspired by the classic 90's handheld game [Lights Out](https://en.wikipedia.org/wiki/Lights_Out_(game)). It is available on the Android Play store.

![Main screen](/Assets/img1.png)  
![Game screen](/Assets/img2.png)

The original game is [generally solved](https://mathworld.wolfram.com/LightsOutPuzzle.html). An adjacency matrix can be constructed into a linear equation, with the tile state as the constant, then solved to give the list of moves necessary to clear the puzzle. Optimal solutions are known for any size of square puzzle, but this has not been shown for other [Archimedean tessellations](https://mathstat.slu.edu/escher/index.php/Tessellations_by_Polygons#Archimedean_tessellations) that can be played in Tez. To this end, Tez has been [replicated in Python](https://github.com/knacko/lightsOutPy), as many more libraries are available to use.

