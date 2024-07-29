import sqlite3
import matplotlib.pyplot as plt
from mpl_toolkits.mplot3d import Axes3D
import numpy as np
from matplotlib import cm

# Step 1: Connect to the SQLite Database
conn = sqlite3.connect("U:\\hbsgi13C_AF制御技術部\\501工機標準\\標準不具合、変更\\20230711 FOCAS2活用\\ラムヤ\\NCaxisappli\\mydata.db")
cursor = conn.cursor()

# Step 2: Retrieve/ Read Data from the Database
cursor.execute("SELECT XAxis, YAxis, ZLoad FROM Data_20231109104719")
data = cursor.fetchall()

# Print the first few rows of the data to verify it
print(data[:12])  # Display the first 5 rows

# Step 3: Create the 3D Plot
XAxis = [int(row[0]) for row in data]
YAxis = [int(row[0]) for row in data]
ZLoad = [[float(row[0]) for row in data]]

fig = plt.figure()
ax = fig.add_subplot(111, projection='3d')

# Meshgrid of X and Y values
X, Y = np.meshgrid(XAxis, YAxis)

# Create a 2D array of Z values corresponding to the X and Y values
Z = np.array(ZLoad)
# Z = np.array(ZLoad).reshape(len(YAxis), len(XAxis) )  # Reshape Z to match X and Y dimensions

# 3D surface plot
surf = ax.plot_surface(X, Y, Z, cmap=cm.coolwarm)

# Labels for the x, y, and z axes
ax.set_xlabel('X Axis')
ax.set_ylabel('Y Axis')
ax.set_zlabel('Z Axis')

# Title to the plot
ax.set_title('3D Surface Plot')

# Add color bar
fig.colorbar(surf, ax=ax)

# Display the plot
plt.show()

# Add color bar
fig.colorbar(surf, ax=ax)

# Display the plot
plt.show()
