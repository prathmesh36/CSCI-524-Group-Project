import matplotlib.pyplot as plt
from matplotlib.ticker import MaxNLocator
import json

# Specify the path to your JSON file
file_path = "checkpoint_game_data.json"

# Open the file and load the JSON data
with open(file_path, "r") as json_file:
    data = json.load(json_file)

# create an array of values 
totalBlackhole = []
totalInfiniteSpace = []
totalPlanets = []
number_games = ["Game1","Game2"]


for item in data.values():
    
    totalBlackhole.append(item["countBlackhole"]);
    totalInfiniteSpace.append(item["countInfinity"]);
    totalPlanets.append(item["countPlanets"]);
    
# Data for the bottom of each bar (starts at 0)
bottom = [0, 0]

# Create a figure and axis
fig, ax = plt.subplots()

# Width of the bars
bar_width = 0.4
spacing = 0

# Plot the first set of values
ax.bar(number_games, totalBlackhole, label='Blackholes', color='r', bottom=bottom, width = bar_width)
bottom = [bottom[i] + totalBlackhole[i] for i in range(len(number_games))]

# Plot the second set of values on top of the first
ax.bar(number_games, [x + spacing for x in totalInfiniteSpace], label='Infinite Space', color='g', bottom=bottom, width = bar_width)
bottom = [bottom[i] + totalInfiniteSpace[i] for i in range(len(number_games))]

# Plot the third set of values on top of the second with spacing
ax.bar(number_games,[x + 2 * spacing for x in totalPlanets], label='Planets', color='b', bottom=bottom, width=bar_width)
#bottom = [bottom[i] + totalPlanets[i] for i in range(len(number_games))]

ax.set_xlabel('Game')
ax.set_ylabel('Total Number of Spacebar Clicks')

ax.set_title('Analytic #1: First Checkpoint Behaviour')
#ax.legend()
ax.legend(loc='best',bbox_to_anchor=(1, 1))  

# Set the y-axis limits
plt.ylim(0, 10)
#ax.yaxis.set_major_locator(MaxNLocator(integer=True))

plt.show()