import matplotlib.pyplot as plt
import numpy as np

from matplotlib import cm
from matplotlib.ticker import LinearLocator

# Create a figure and a 3D subplot
fig = plt.figure()
ax = fig.add_subplot(111, projection='3d')

# Create data ranges
x = np.arange(-19782, 19783, 100)
y = np.arange(-72041, 72042, 100)
X, Y = np.meshgrid(x, y)

# Set z_load and sp_load values
z_load = 30.99 * np.ones(X.shape)
sp_load = 105.01419 * np.ones(X.shape)

# Plot the surface for z_load
surf_z_load = ax.plot_surface(X, Y, z_load, cmap=cm.coolwarm,
                             linewidth=0, antialiased=False)

# Plot the surface for sp_load
surf_sp_load = ax.plot_surface(X, Y, sp_load, cmap=cm.coolwarm,
                             linewidth=0, antialiased=False)

# Customize the z axis.
ax.set_zlim(0, 120)  # Adjust z-axis limits as needed
ax.zaxis.set_major_locator(LinearLocator(10))
ax.zaxis.set_major_formatter('{x:.02f}')

# Add a color bar for z_load
fig.colorbar(surf_z_load, shrink=0.5, aspect=5, label='z_load')

# Add a color bar for sp_load
fig.colorbar(surf_sp_load, shrink=0.5, aspect=5, label='sp_load')

plt.show()
