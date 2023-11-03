import matplotlib.pyplot as plt
from matplotlib.ticker import MaxNLocator
import json

# Specify the path to your JSON file
file_path = "FuelTrend_game_data.json"

# Open the file and load the JSON data
with open(file_path, "r") as json_file:
    data = json.load(json_file)


x = [1, 2, 3, 4, 5]
y1 = [100]
#plt.plot(x, y1, label='Line 1', color='blue', marker='o', linestyle='-', linewidth=2)

mainItem = dict()

k=0
for item in data.values():
    mainItem[k] = item
    k += 1


y1.append(mainItem[0]["asteroidImpact"])
y1.append(mainItem[0]["blackholeImpact"])
y1.append(mainItem[0]["infiniteSpaceImpact"])  
y1.append(mainItem[0]["planetImpact"])
plt.plot(x, y1,  marker='o',color = 'black' , linestyle='dotted', linewidth=2, alpha = 0.2)

y1 = [100]

y1.append(mainItem[1]["asteroidImpact"])
y1.append(mainItem[1]["blackholeImpact"])
y1.append(mainItem[1]["infiniteSpaceImpact"])  
y1.append(mainItem[1]["planetImpact"])
plt.plot(x, y1,  marker='s',color = 'black', linestyle='solid', linewidth=2, alpha = 0.2)

colors = ['red', 'black' ,'cyan', 'green', 'pink']
datapoints = ["Initial Fuel","Asteroid", "Blackhole", "Infinite Space","Planet Impact"]
# Highlight data points with different colors
for i in range(len(x)):
    plt.scatter(x[i], y1[i], color=colors[i], label= datapoints[i], marker='s')


## Now append rest of the values
# for item in data.values():
#     y1.append(item["asteroidImpact"])
#     y1.append(item["blackholeImpact"])
#     y1.append(item["infiniteSpaceImpact"])  
#     y1.append(item["planetImpact"])
#     plt.plot(x, y1, label='Line 1', color='blue', marker='o', linestyle='-', linewidth=2)
#     y1 = [100]


# Create line plots for y1 and y2



# Add labels and a title
plt.xlabel('Time')
plt.ylabel('Value of Fuel')
plt.title('Analytics #4: Impact of game on fuel over time')

# Display a legend
plt.legend()

# Display the plot
plt.grid(True)  # Add gridlines
plt.show()
   