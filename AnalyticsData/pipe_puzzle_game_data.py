import matplotlib.pyplot as plt
import json

plt.rcParams['toolbar'] = 'None' 

# Specify the path to your JSON file
file_path = "Pipe-Puzzle-Game-Data.json"

# Open the file and load the JSON data
with open(file_path, "r") as json_file:
    data = json.load(json_file)

# Initialize counters for different outcomes
outcomes = {
    "Category 1": 0,
    "Category 2": 0,
    "Category 3": 0,
    "Category 4": 0
}

# Count the different outcomes
for item in data.values():
    outcome = item["category"]
    if outcome in outcomes:
        outcomes[outcome] += 1

# Create a bar chart to visualize the different outcomes
categories = list(outcomes.keys())
values = list(outcomes.values())

plt.bar(categories, values, color=['green', 'red', 'blue', 'purple'])
plt.xlabel("Categories")
plt.ylabel("Number of Games")
plt.title("Playing styles for Pipe Game")
plt.xticks(rotation=45)  # Rotate x-axis labels for better visibility
plt.tight_layout()
plt.show()