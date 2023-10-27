import matplotlib.pyplot as plt
import json

plt.rcParams['toolbar'] = 'None' 

# Specify the path to your JSON file
file_path = "Wire-Puzzle-Game-Data.json"

# Open the file and load the JSON data
with open(file_path, "r") as json_file:
    data = json.load(json_file)

# Initialize counters for different outcomes
outcomes = {
    "Won": 0,
    "Lost": 0
}

# Count the different outcomes
for item in data.values():
    outcome = item["winLossStatus"]
    if outcome in outcomes:
        outcomes[outcome] += 1

# Create a bar chart to visualize the different outcomes
categories = list(outcomes.keys())
values = list(outcomes.values())

plt.bar(categories, values, color=['green', 'red'])
plt.xlabel("Game Outcome")
plt.ylabel("Number of Games")
plt.title("Number of Different Game Outcomes")
plt.xticks(rotation=45)  # Rotate x-axis labels for better visibility
plt.tight_layout()
plt.show()

# Extract timeTakenToFinishGame values
time_values = [entry["timeTakenToFinishGame"] for entry in data.values()]

# Create a histogram
plt.hist(time_values, bins=10, edgecolor='k')
plt.xlabel('Time Taken to Finish Game')
plt.ylabel('Frequency')
plt.title('Histogram Distribution of Time Taken to Finish Game')
plt.tight_layout()
plt.show()