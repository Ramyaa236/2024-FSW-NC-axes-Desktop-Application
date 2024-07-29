import matplotlib.pyplot as plt
from matplotlib.animation import FuncAnimation
import numpy as np

# Create initial data
x = np.linspace(0, 2 * np.pi, 100)
y = np.sin(x)

# Create a figure and axis
fig, ax = plt.subplots()
line, = ax.plot(x, y)

# Function to update the plot data
def update(frame):
    # Update data for animation
    line.set_ydata(np.sin(x + frame *0.01))
    return line,

# Create an animation
animation = FuncAnimation(fig, update, frames=100, interval=50, blit=True)

# Show the plot
plt.show()
