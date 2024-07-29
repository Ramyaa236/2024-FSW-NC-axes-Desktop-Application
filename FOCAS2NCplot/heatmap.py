import sqlite3
import matplotlib.pyplot as plt
from mpl_toolkits.mplot3d import Axes3D
import numpy as np
from matplotlib import cm

# Step 1: Connect to the SQLite Database
conn = sqlite3.connect("U:\\hbsgi13C_AF制御技術部\\501工機標準\\標準不具合、変更\\20230711 FOCAS2活用\\ラムヤ\\NCaxisappli\\mydata.db")
cursor = conn.cursor()

# Step 2: Retrieve/ Read Data from the Database
cursor.execute("SELECT XAxis, YAxis, ZLoad FROM Data_20231109105425")
data = cursor.fetchall()

# Print the first few rows of the data to verify it
print(data[:21])  # Display the first 5 rows

# Step 3: Create the 3D Plot
Xpos = [int(row[0]) for row in data]
Ypos = [int(row[0]) for row in data]
#Zpos = [[float(row[0]) for row in data]]
Zload = [[float(row[0]) for row in data]]

# Create a meshgrid for X and Y #######################################################################################
X, Y = np.meshgrid(Xpos, Ypos)

# Create a 3D figure
fig = plt.figure()
ax = fig.add_subplot(111, projection='3d')

frequency = 0.5  # frequency of the waves
amplitude_ZLoad = np.array([float(row[0]) for row in data])  # Maximum value for Z_load
ZLoad = amplitude_ZLoad * np.sin(frequency * X) * np.cos(frequency * Y)

# Define the color map
cmap = plt.get_cmap('coolwarm')

# Normalize Z_load to [0, 1] for colormap mapping
norm = plt.Normalize(ZLoad.min(), ZLoad.max())
print(ZLoad.min(), ZLoad.max())

# Color the surface based on Z_load values
facecolors = cmap(norm(ZLoad))
surf = ax.plot_surface(X, Y, ZLoad, facecolors=facecolors, cmap=cmap, label='Z Load')

colorbar = fig.colorbar(surf, shrink=0.5, aspect=5, label='Z Load')

# Set the range for the axes
ax.set_xlim([0, 100000])
ax.set_ylim([-100000, 100000])
ax.set_zlim([-100000, 100000])

# Customize the plot
ax.set_xlabel('X Axis')
ax.set_ylabel('Y Axis')
ax.set_zlabel('Z Axis')
ax.set_title('3D Surface Plot')


# Show the plot
plt.show()






# fig = plt.figure()
# ax = fig.add_subplot(111, projection='3d')

# # Meshgrid of X and Y values
# X, Y = np.meshgrid(Xpos, Ypos)

# # Create a 2D array of Z values corresponding to the X and Y values 
# Z = np.array(Zload)
# # Z = np.array(ZLoad).reshape(len(YAxis), len(XAxis) )  # Reshape Z to match X and Y dimensions

# # 3D surface plot
# surf = ax.plot_surface(X, Y, Z, cmap=cm.coolwarm)

# # Labels for the x, y, and z axes
# ax.set_xlabel('X Axis')
# ax.set_ylabel('Y Axis')
# ax.set_zlabel('Z Axis')

# # Title to the plot
# ax.set_title('3D Surface Plot')

# # Add color bar
# fig.colorbar(surf, ax=ax)

# # Display the plot
# plt.show()

# # Add color bar
# fig.colorbar(surf, ax=ax)

# # Display the plot
# plt.show()
