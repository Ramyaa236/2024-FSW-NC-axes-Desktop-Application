import numpy as np
import matplotlib.pyplot as plt
from mpl_toolkits.mplot3d import Axes3D

X_pos = np.array([0,0,0,0,0,0,0,0,0,0,41920,90560,100000,100000,100000,100000,100000,100000,100000,100000,100000])
Y_pos = np.array([0,0,0,0,0,0,0,-51200,-100000,-100000,-100000,-100000,-100000,-100000,-100000,-100000,-100000,-100000,-100000,-100000,-100000])

# Create a meshgrid for X and Y
X, Y = np.meshgrid(X_pos, Y_pos)

# Create a 3D figure
fig = plt.figure()
ax = fig.add_subplot(111, projection='3d')

frequency = 0.2  # frequency of the waves
amplitude_z_load = np.array([32.7886710239651,33.3877995642702,32.7886710239651,33.1699346405229,32.8976034858388,32.8976034858388,32.8976034858388,33.0065359477124,33.0610021786492,33.1699346405229,33.2244008714597,32.9520697167756,20.6971677559913,21.1328976034858,22.60348583878,22.7668845315904,22.875816993464,22.875816993464,22.7668845315904,22.7668845315904,23.1481481481481])  # Maximum value for Z_load
Z_load = amplitude_z_load[:, np.newaxis] * np.sin(frequency * X) * np.cos(frequency * Y)

# Define the color map
cmap = plt.get_cmap('coolwarm')

# Normalize Z_load to [0, 1] for colormap mapping
norm = plt.Normalize(Z_load.min(), Z_load.max())
print(Z_load.min(), Z_load.max())

# Color the surface based on Z_load values
facecolors = cmap(norm(Z_load))
surf = ax.plot_surface(X, Y, Z_load, facecolors=facecolors, cmap=cmap, label='Z Load')

colorbar = fig.colorbar(surf, shrink=0.5, aspect=5, label='Z Load')

# Set the range for the axes
ax.set_xlim([0, 100000])
ax.set_ylim([-100000, 0])
ax.set_zlim([-50, 50])

# Customize the plot
ax.set_xlabel('X Axis')
ax.set_ylabel('Y Axis')
ax.set_zlabel('Z Axis')
ax.set_title('3D Surface Plot')

# Show the plot
plt.show()





# import numpy as np
# import matplotlib.pyplot as plt
# from mpl_toolkits.mplot3d import Axes3D

# X_pos = np.array([0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20])
# Y_pos = np.array([0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20])

# # Create a meshgrid for X and Y
# X, Y = np.meshgrid(X_pos, Y_pos)

# # Create a 3D figure
# fig = plt.figure()
# ax = fig.add_subplot(111, projection='3d')

# frequency = 0.2  # frequency of the waves
# amplitude_z_load = np.array([0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 90, 80, 70, 60, 50, 40, 30, 20, 10, 0])  # Maximum value for Z_load
# Z_load = amplitude_z_load * np.sin(frequency * X) * np.cos(frequency * Y)

# # Define the color map
# cmap = plt.get_cmap('coolwarm')

# # Normalize Z_load to [0, 1] for colormap mapping
# norm = plt.Normalize(Z_load.min(), Z_load.max())

# # Color the surface based on Z_load values
# facecolors = cmap(norm(Z_load))
# surf = ax.plot_surface(X, Y, Z_load, facecolors=facecolors, cmap=cmap, label='Z Load')

# # Set the range for the axes
# ax.set_xlim([-100, 100])
# ax.set_ylim([-100, 100])
# ax.set_zlim([-100, 100])

# # Customize the plot
# ax.set_xlabel('X Axis')
# ax.set_ylabel('Y Axis')
# ax.set_zlabel('Z Axis')
# ax.set_title('3D Surface Plot')

# # Mark specific points on the graph
# ax.scatter(0, 0, 0, color='blue', s=100, label='Z Axis: 0, Z Load: 0')
# ax.text(0, 0, 0, 'Z Axis: 0, Z Load: 0', color='blue', fontsize=12, zorder=1)

# ax.scatter(0, 100, 0, color='red', s=100, label='Z Axis: 0, Z Load: 100')
# ax.text(0, 100, 0, 'Z Axis: 0, Z Load: 100', color='red', fontsize=12, zorder=1)

# ax.scatter(100, 0, 0, color='blue', s=100, label='Z Axis: 100, Z Load: 0')
# ax.text(100, 0, 0, 'Z Axis: 100, Z Load: 0', color='green', fontsize=12, zorder=1)

# ax.scatter(100, 100, 0, color='red', s=100, label='Z Axis: 100, Z Load: 100')
# ax.text(100, 100, 0, 'Z Axis: 100, Z Load: 100', color='purple', fontsize=12, zorder=1)

# # Show the plot
# plt.show()
