import matplotlib.pyplot as plt
from matplotlib.ticker import MaxNLocator
import json

# Specify the path to your JSON file
file_path = "pipe_puzzle_game_data.json"

# Open the file and load the JSON data
with open(file_path, "r") as json_file:
    data = json.load(json_file)

# create an array of values 
totalMinesClicked = []
totalMouseClicks = []
number_games = ["Game1","Game2"]


for item in data.values():
    #print("Item: ",item["monstersCount"])
    totalMinesClicked.append(item["mineClicks"])
    totalMouseClicks.append(item["mouseClicks"] - item["mineClicks"])
    


# Data for the bottom of each bar (starts at 0)
bottom = [0, 0]

# Create a figure and axis
fig, ax = plt.subplots()

# Width of the bars
bar_width = 0.6
spacing = 0.01

# Plot the first set of values
ax.bar(number_games, totalMinesClicked, label='Clicks resulting in Mines', color='r', bottom=bottom, width = bar_width)
bottom = [bottom[i] + totalMinesClicked[i] for i in range(len(number_games))]

# Plot the second set of values on top of the first
ax.bar(number_games, [x + spacing for x in totalMouseClicks], label='Clicks for correct pipe move', color='g', bottom=bottom, width = bar_width)
bottom = [bottom[i] + totalMouseClicks[i] for i in range(len(number_games))]

ax.set_xlabel('Game')
ax.set_ylabel('Total Number of Mouse-Clicks')

ax.set_title('Analytic #3: Pipe Puzzle')
#ax.legend()
ax.legend(loc='best',bbox_to_anchor=(1, 1))  

# Set the y-axis limits
plt.ylim(0, 10)
#ax.yaxis.set_major_locator(MaxNLocator(integer=True))

plt.show()