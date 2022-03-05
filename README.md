# GAME3011_A1_Idris
Game Portfolio Development Assignment 1

For this assignment, you will have to create much of the functionality for a 2D resource-gathering game where the goal is to try to find the highest concentration of mineral as possible and then dig to gather that mineral in a grid. Include the following criteria:

1)	You will be using Unity for this assignment.
2)	You can work in pairs, but it is recommended that you work alone to make this assignment a more effective portfolio piece.
3)	This ‘game’ must be treated like a 2D minigame pop-up that would be part of a larger game, such as a 3D, open-world one. You will need to create a Unity scene that can be empty, but you must have some event that brings up this resource gathering interface – a button or key press will do.
4)	The game grid can be from 32x32 to 64x64 tiles in size. Applicants will often have some freedom when it comes to UI and placement of such elements. Ensure that your interface has room for the following UI elements:
a.	A mode toggle button. This functionality will be explained below.
b.	A resource counter. A text field that keeps a tally of your collected resource amount.
c.	A message bar. A text field that displays a short message to the user.
d.	Any optional elements you like. Companies will be looking for creativity from the applicant, but remember to keep your interface clean. Don’t assault the player with unnecessary information.
5)	The goal of the game is to collect as much of a special resource, i.e. Tiberium, Unobtanium, generic ore, gold... etc, from the grid. You will place a random amount (e.g. 2000-5000) of this element in positions around the grid.
a.	To clarify, these tiles will contain the maximum amount of the resource. Then, each tile around this “central” maximum amount will contain half, then moving outward again, the amount will be ¼. Refer to the diagram below.
b.	The number of resource tiles should depend on your grid size and it will be up to you to find a good balance.
c.	You are free to use any color scheme you like but there should be four colors: max resource, half resource, quarter resource and minimal resource.
d.	For tiles outside the quarter resource, you can choose to have the tiles empty or fill them with a minimal amount maybe 1/8th or 1/16th of the maximum amount.


MAX RESOURCE:
Yellow Squares
 
HALF RESOURCE:
Light Orange Squares

QUARTER RESOURCE:
Dark Orange Squares


6)	The amount of resource, i.e. the colored tiles will be hidden from the user until they click on the toggle button to select Scan Mode that will allow them to click tiles on the grid. The maximum number of scans in total should be around 6.
7)	When the player clicks a tile in Scan Mode, it will display the resource underneath as well as those tiles that immediately surround it, for a total of 9 tiles.
8)	From these scans, the player may find a maximum amount of resource or at lease close.
9)	The player can toggle to Extract Mode to collect the resource. Then, the player will click a tile and gather all resources from that tile and it will become either become zero or a minimal resource tile.s
a.	The two rings of tiles around the tile clicked will be degraded to one level below. There is no level below the minimal resource, however.
b.	The player can only extract three times.
10)	When the player has extracted three times, ensure they cannot click anymore and display a final message with their total in the manner of your choice.
11)	You are only required to create the interface and no title/help or end screen for the ‘main’ game is required, however you may want to communicate the tile colors and their values to the player with a legend of some sort. Again, any aesthetics are up to you.
