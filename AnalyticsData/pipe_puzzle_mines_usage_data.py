import matplotlib.pyplot as plt

data = {
    "-Nh-jZ_Bcdq7xTzBc6dF": {"mines_collected": 3, "mines_used": 3},
    "A1b2C3d4E5": {"mines_collected": 2, "mines_used": 1},
    "X9Y8Z7W6": {"mines_collected": 4, "mines_used": 2},
    "K0L1M2N3": {"mines_collected": 1, "mines_used": 0},
    "P4Q5R6S7": {"mines_collected": 3, "mines_used": 2}
}

mines_collected_values = [entry["mines_collected"] for entry in data.values()]
mines_used_values = [entry["mines_used"] for entry in data.values()]

# Calculate the mean values
mean_mines_collected = sum(mines_collected_values) / len(mines_collected_values)
mean_mines_used = sum(mines_used_values) / len(mines_used_values)

# Plot histogram for mines_collected
plt.figure(1)
plt.hist(mines_collected_values, bins=range(6), alpha=0.5, color='b', label="Mines Collected")
plt.axvline(mean_mines_collected, color='b', linestyle='dashed', linewidth=1, label=f"Mean Mines Collected: {mean_mines_collected:.2f}")
plt.xlabel("Mines Collected")
plt.ylabel("Frequency")
plt.legend(loc="upper right")
plt.title("Histogram of Mines Collected")

# Plot histogram for mines_used
plt.figure(2)
plt.hist(mines_used_values, bins=range(6), alpha=0.5, color='r', label="Mines Used")
plt.axvline(mean_mines_used, color='r', linestyle='dashed', linewidth=1, label=f"Mean Mines Used: {mean_mines_used:.2f}")
plt.xlabel("Mines Used")
plt.ylabel("Frequency")
plt.legend(loc="upper right")
plt.title("Histogram of Mines Used")

plt.show()