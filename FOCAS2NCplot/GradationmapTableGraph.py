import sys
import sqlite3
import numpy as np
import matplotlib.pyplot as plt
from mpl_toolkits.mplot3d import Axes3D
from matplotlib import cm
from matplotlib import colors
import mplcursors

def main(selectedFileName):
    try:
        # Step 1: Connect to the SQLite Database
        conn = sqlite3.connect("U:\\hbsgi13C_AF制御技術部\\501工機標準\標準不具合、変更\\20230711 FOCAS2活用\\2024.02.7 TXT,SQlite NCaxisappli\\FOCAS2NCplot\\mydata.db")
        cursor = conn.cursor()

        # Step 2: Retrieve/ Read specific Data from the selected table
        cursor.execute(f"SELECT XAxis, YAxis, ZAxis, ZLoad FROM {selectedFileName}")
        data = cursor.fetchall()

        # Step 3: Separate data into lists
        x = []
        y = []
        z = []
        z_load = []

        for row in data:
            x.append(row[0])
            y.append(row[1])
            z.append(row[2])
            z_load.append(row[3])
            print("XAxis:", row[0], "YAxis:", row[1], "ZAxis:", row[2], "ZLoad:", row[3])

        ## Calculate the range of z_load values
        z_load_range = max(z_load) - min(z_load)

        # Check if the range is zero
        if z_load_range == 0:
            # Handle the case when the range is zero (avoid division by zero)
            z_load_percent = np.zeros_like(z_load)
        else:
            # Perform the calculation
            z_load_percent = (np.array(z_load) - min(z_load)) / z_load_range * 100

        # Create a 3D plot
        fig = plt.figure(facecolor='black', figsize=(12, 10))
        ax = fig.add_subplot(111, facecolor='black', projection='3d')

        # Create a ScalarMappable to apply the colormap
        norm = colors.Normalize(0, 100)
        cmap = plt.cm.coolwarm
        sm = plt.cm.ScalarMappable(cmap=cmap, norm=norm)
        sm.set_array([])

        # Plot the 3D line with colormap gradation
        for i in range(len(x) - 1):
            ax.plot(x[i:i+2], y[i:i+2], z[i:i+2], color=cmap(norm(z_load_percent[i])), linewidth=2)

        # Add a colorbar to the plot
        cbar = fig.colorbar(sm, ax=ax)
        cbar.ax.tick_params(axis='y', labelcolor='white', labelsize=10)
        cbar.set_label('Z Load(%)', color='white')

        # Set labels for each axis
        ax.set_xlabel('X-axis', color='white')
        ax.set_ylabel('Y-axis', color='white')
        ax.set_zlabel('Z-axis', color='white')

        # Set axis ticks color to white
        ax.tick_params(axis='x', colors='white')
        ax.tick_params(axis='y', colors='white')
        ax.tick_params(axis='z', colors='white')

        ax.set_title('FSW 3D Plot', color='white')

        # Hide grid
        ax.grid(True)
        mplcursors.cursor(hover=True)
        plt.show()

    except Exception as ex:
        print(f"Error: {ex}")

if __name__ == "__main__":
    # Accept the table name as a command-line argument
    selectedFileName = sys.argv[1]
    # Call the main function with the selected table name
    main(selectedFileName)
