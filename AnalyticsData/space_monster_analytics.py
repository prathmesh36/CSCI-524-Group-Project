import matplotlib.pyplot as plt
from matplotlib.ticker import MaxNLocator
import json

# Specify the path to your JSON file
file_path = "space_monster_game_data.json"

# Open the file and load the JSON data
with open(file_path, "r") as json_file:
    data = json.load(json_file)

# create an array of values 
totalMonstersKilled = []
totalMonstersEscaped = []
number_games = ["Game1","Game2"]


for item in data.values():
    #print("Item: ",item["monstersCount"])
    totalMonstersKilled.append(item["monstersKilled"])
    totalMonstersEscaped.append(item["monstersCount"] - item["monstersKilled"])
    


# Data for the bottom of each bar (starts at 0)
bottom = [0, 0]

# Create a figure and axis
fig, ax = plt.subplots()

# Width of the bars
bar_width = 0.6
spacing = 0.01

# Plot the first set of values
ax.bar(number_games, totalMonstersKilled, label='Monsters Killed', color='r', bottom=bottom, width = bar_width)
bottom = [bottom[i] + totalMonstersKilled[i] for i in range(len(number_games))]

# Plot the second set of values on top of the first
ax.bar(number_games, [x + spacing for x in totalMonstersEscaped], label='Monsters Escaped', color='g', bottom=bottom, width = bar_width)
bottom = [bottom[i] + totalMonstersEscaped[i] for i in range(len(number_games))]

ax.set_xlabel('Game')
ax.set_ylabel('Count')

ax.set_title('Analytic #2: Space Monsters')
#ax.legend()
ax.legend(loc='best',bbox_to_anchor=(1, 1))  

# Set the y-axis limits
plt.ylim(0, 25)
#ax.yaxis.set_major_locator(MaxNLocator(integer=True))

plt.show()