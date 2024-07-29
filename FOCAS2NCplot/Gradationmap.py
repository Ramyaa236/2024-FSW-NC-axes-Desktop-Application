
import matplotlib.pyplot as plt
from mpl_toolkits.mplot3d import Axes3D
from matplotlib import cm
from matplotlib import colors
import numpy as np
import sqlite3
import mplcursors


# Step 1: Connect to the SQLite Database
# conn = sqlite3.connect("U:\\hbsgi13C_AF制御技術部\\501工機標準\\標準不具合、変更\\20230711 FOCAS2活用\\ラムヤ\\NCaxisappli\\mydata.db")   #real location
conn = sqlite3.connect("U:\\hbsgi13C_AF制御技術部\\501工機標準\標準不具合、変更\\20230711 FOCAS2活用\\2024.02.7 TXT,SQlite NCaxisappli\\mydata.db")      #10.1.24
# conn = sqlite3.connect("C:\\Users\\2701\\Desktop\\20kdata2.db") 
cursor = conn.cursor()

# #######################################################################################1111

# # Retrieve/ Read Data from the latest data table name
# ##cursor.execute("SELECT name FROM sqlite_master WHERE type='table' AND name LIKE 'Data_%' ORDER BY name DESC LIMIT 1")
# cursor.execute("SELECT name FROM sqlite_master WHERE type='table' AND name LIKE 'IP_%' ORDER BY name DESC LIMIT 1")

# latest_table = cursor.fetchone()

# if latest_table:
#     latest_table_name = latest_table[0]

#     # Retrieve specific columns from the latest table
#     cursor.execute(f"SELECT XAxis, YAxis, ZAxis, ZLoad FROM {latest_table_name}")
#     latest_data = cursor.fetchall()

#     #Separate data into lists
#     x = []
#     y = []
#     z = []
#     z_load = []

#     if latest_data:
#         for row in latest_data:
#             x.append(row[0])
#             y.append(row[1])
#             z.append(row[2])
#             z_load.append(row[3])
#             print("XAxis:", row[0], "YAxis:", row[1], "ZAxis:", row[2], "ZLoad:", row[3])

#         ##Calculate Z load percentage(Newton N --> Percentage %) if N -> just comment the below line # for Newton cbar graph
#         #z_load_percent = (np.array(z_load) - min(z_load)) / (max(z_load) - min(z_load)) * 100
#         ##Calculate Z load percentage(Newton N --> Percentage %) if N -> just comment the below line # for Newton cbar graph


#         ## Calculate the range of z_load values  --> For both NC GUIDE, Acutual machine Test run
#         z_load_range = max(z_load) - min(z_load)

#         # Check if the range is zero
#         if z_load_range == 0:
#             # Handle the case when the range is zero (avoid division by zero)
#             z_load_percent = np.zeros_like(z_load)
#         else:
#             # Perform the calculation
#             z_load_percent = (np.array(z_load) - min(z_load)) / z_load_range * 100
#         ## Calculate the range of z_load values  --> For both NC GUIDE, Acutual machine Test run       


#     else:
#         print("No data found in the latest table.")

# else:
#     print("No tables found with the 'Data_' prefix.")

#######################################################################################1111


#######################################################################################2222

# Step 2: Retrieve/ Read specific Data from any table name in the Database
cursor.execute("SELECT XAxis, YAxis, ZAxis, ZLoad FROM IP_192_168_1_1_20240716104043")  #  IP_192_168_1_1_20240716103346 20240716104043   IP_127_0_0_1_20240418143000  Data_20240325143001 3000 _20k  20231109105425 20231109110724 20231109104122  20231109104426  20231109104719  20231208110259,20231208111049,20231208112116,...Data_20231214144003
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

# #Calculate Z load percentage --> For Actual Machine FSWNS70 Test run (Newton N --> Percentage %) if N -> just comment the below line # for Newton cbar graph
# z_load_percent = (np.array(z_load) - min(z_load)) / (max(z_load) - min(z_load)) * 100
# #Calculate Z load percentage --> For Actual Machine FSWNS70 Test run (Newton N --> Percentage %) if N -> just comment the below line # for Newton cbar graph


## Calculate the range of z_load values  --> For both NC GUIDE, Acutual machine Test run
z_load_range = max(z_load) - min(z_load)

# Check if the range is zero
if z_load_range == 0:
    # Handle the case when the range is zero (avoid division by zero)
    z_load_percent = np.zeros_like(z_load)
else:
    # Perform the calculation
    z_load_percent = (np.array(z_load) - min(z_load)) / z_load_range * 100
## Calculate the range of z_load values  --> For both NC GUIDE, Acutual machine Test run

#######################################################################################2222
# Create a 3D plot
fig = plt.figure(facecolor='black', figsize=(12, 10))   
# fig = plt.figure(figsize=(12, 10))
ax = fig.add_subplot(111, facecolor='black', projection='3d')
#ax.set_facecolor('black')

# Create a ScalarMappable to apply the colormap #
#norm = colors.Normalize(min(z_load), max(z_load))  # for Newton cbar graph
norm = colors.Normalize(0, 100)  # for % cbar graph
cmap = plt.cm.coolwarm  #viridis Spectral  bwr  seismic  rainbow
sm = plt.cm.ScalarMappable(cmap=cmap, norm=norm)
sm.set_array([])

# Plot the 3D line with colormap gradation
for i in range(len(x) - 1):
    ax.plot(x[i:i+2], y[i:i+2], z[i:i+2], color=cmap(norm(z_load_percent[i])), linewidth=2) # for % cbar graph
    #ax.plot(x[i:i+2], y[i:i+2], z[i:i+2], color=cmap(norm(z_load[i])), linewidth=2) # for Newton cbar graph


# Add a colorbar to the plot
cbar = fig.colorbar(sm, ax=ax)
cbar.ax.tick_params(axis='y', labelcolor='white', labelsize=10)  # Set color of tick labels to white
cbar.set_label('Z Load(%)', color='white')  # Set color of colorbar label to white # for Newton cbar graph-> change % -> N

# Add Min and Max labels to the colorbar
cbar.ax.text(0.15, -0.03, 'Min', color='white', va='center', ha='left', transform=cbar.ax.transAxes)
cbar.ax.text(0.15, 1.03, 'Max', color='white', va='center', ha='left', transform=cbar.ax.transAxes)

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

##Use mplcursors to display position values on hover
#cursor = mplcursors.cursor(ax, hover=False)

plt.show()




# #Dropdown button
# import sys
# import matplotlib.pyplot as plt
# from mpl_toolkits.mplot3d import Axes3D
# from matplotlib import cm
# from matplotlib import colors
# import numpy as np
# import sqlite3
# import mplcursors

# def display_graph(table_name):
#     # Step 1: Connect to the SQLite Database
#     conn = sqlite3.connect("C:\\Users\\2701\\Desktop\\Ramyaa ラムヤ\\NC APPLI\\2024.02.7 TXT,SQlite NCaxisappli\\mydata.db")
#     cursor = conn.cursor()

#     # Retrieve specific columns from the selected table
#     cursor.execute(f"SELECT XAxis, YAxis, ZAxis, ZLoad FROM {table_name}")
#     data = cursor.fetchall()

#     # Separate data into lists
#     x = []
#     y = []
#     z = []
#     z_load = []

#     for row in data:
#         x.append(row[0])
#         y.append(row[1])
#         z.append(row[2])
#         z_load.append(row[3])

#     # Calculate Z load percentage
#     z_load_range = max(z_load) - min(z_load)
#     if z_load_range == 0:
#         z_load_percent = np.zeros_like(z_load)
#     else:
#         z_load_percent = (np.array(z_load) - min(z_load)) / z_load_range * 100

#     # Create a 3D plot
#     fig = plt.figure(facecolor='black', figsize=(12, 10))
#     ax = fig.add_subplot(111, facecolor='black', projection='3d')

#     # Create a ScalarMappable to apply the colormap
#     norm = colors.Normalize(0, 100)
#     cmap = plt.cm.coolwarm
#     sm = plt.cm.ScalarMappable(cmap=cmap, norm=norm)
#     sm.set_array([])

#     # Plot the 3D line with colormap gradation
#     for i in range(len(x) - 1):
#         ax.plot(x[i:i+2], y[i:i+2], z[i:i+2], color=cmap(norm(z_load_percent[i])), linewidth=2)

#     # Add a colorbar to the plot
#     cbar = fig.colorbar(sm, ax=ax)
#     cbar.ax.tick_params(axis='y', labelcolor='white', labelsize=10)
#     cbar.set_label('Z Load(%)', color='white')

#     # Set labels for each axis
#     ax.set_xlabel('X-axis', color='white')
#     ax.set_ylabel('Y-axis', color='white')
#     ax.set_zlabel('Z-axis', color='white')

#     # Set axis ticks color to white
#     ax.tick_params(axis='x', colors='white')
#     ax.tick_params(axis='y', colors='white')
#     ax.tick_params(axis='z', colors='white')

#     ax.set_title(f'3D Plot for Table: {table_name}', color='white')
#     ax.grid(True)

#     mplcursors.cursor(hover=True)

#     plt.show()

# if __name__ == "__main__":
#     # Accept the table name as command-line argument
#     table_name = sys.argv[1]
#     display_graph(table_name)



###################################################################################
###################################################################################

#REAL VERSION
# import matplotlib.pyplot as plt
# from mpl_toolkits.mplot3d import Axes3D
# from matplotlib import cm
# from matplotlib import colors
# import numpy as np
# import sqlite3
# import mplcursors
# #######################################################################################1111

# # # Data: X axis data --> -100000(neg) to 100000(pos)   Y axis data --> 100000(pos) to -100000(neg)   Z axis data --> 100000(pos) to -100000(neg)  ZLoad data --> from 60 to 22
# # x = [-100000,-100000,-100000,-100000,-100000,-100000,-100000,-100000,-100000,0,0,0,0,0,0,0,0,0,0,41920,90560,100000,100000,100000,100000,100000,100000,100000,100000,100000]
# # y = [100000,100000,100000,100000,100000,100000,100000,100000,80560,0,0,0,0,0,0,0,-51200,-100000,-100000,-100000,-100000,-100000,-100000,-100000,-100000,-100000,-100000,-100000,-100000,-100000]
# # z = [100000,100000,100000,100000,100000,100000,100000,100000,80560,0,0,0,0,0,0,0,0,0,0,0,0,-30400,-75520,-100000,-100000,-100000,-100000,-100000,-100000,-100000]
# # z_load = [60,59,58,58,55,55.2,55.2,55.2,51,32.7,33.38,32.78,33.16,32.89,32.89,32.89,33,33.06,33.1,33.2,32.95,20.69,21.13,22.6,22.7,22.87,22.87,22.76,22.76,23.14]

# #######################################################################################1111

# # Step 1: Connect to the SQLite Database
# # conn = sqlite3.connect("U:\\hbsgi13C_AF制御技術部\\501工機標準\\標準不具合、変更\\20230711 FOCAS2活用\\ラムヤ\\NCaxisappli\\mydata.db")   #real location
# conn = sqlite3.connect("C:\\Users\\2701\\Desktop\\Ramyaa ラムヤ\\NC APPLI\\2024.02.7 TXT,SQlite NCaxisappli\\mydata.db")      #10.1.24
# # conn = sqlite3.connect("C:\\Users\\2701\\Desktop\\20kdata2.db") 
# cursor = conn.cursor()

# # #######################################################################################2222

# # Retrieve/ Read Data from the latest data table name
# cursor.execute("SELECT name FROM sqlite_master WHERE type='table' AND name LIKE 'Data_%' ORDER BY name DESC LIMIT 1")
# latest_table = cursor.fetchone()

# if latest_table:
#     latest_table_name = latest_table[0]

#     # Retrieve specific columns from the latest table
#     cursor.execute(f"SELECT XAxis, YAxis, ZAxis, ZLoad FROM {latest_table_name}")
#     latest_data = cursor.fetchall()

#     #Separate data into lists
#     x = []
#     y = []
#     z = []
#     z_load = []

#     if latest_data:
#         for row in latest_data:
#             x.append(row[0])
#             y.append(row[1])
#             z.append(row[2])
#             z_load.append(row[3])
#             print("XAxis:", row[0], "YAxis:", row[1], "ZAxis:", row[2], "ZLoad:", row[3])
#     else:
#         print("No data found in the latest table.")

# else:
#     print("No tables found with the 'Data_' prefix.")

# #######################################################################################2222

# #########################################
# # Close the cursor and connection
# # cursor.close()
# # conn.close()
# #########################################

# #######################################################################################3333

# # # Step 2: Retrieve/ Read specific Data from any table name in the Database
# # cursor.execute("SELECT XAxis, YAxis, ZAxis, ZLoad FROM Data_20240325143000")  # _20k  20231109105425 20231109110724 20231109104122  20231109104426  20231109104719  20231208110259,20231208111049,20231208112116,...Data_20231214144003
# # data = cursor.fetchall()

# # # Step 3: Separate data into lists
# # x = []
# # y = []
# # z = []
# # z_load = []

# # for row in data:
# #     x.append(row[0])
# #     y.append(row[1])
# #     z.append(row[2])
# #     z_load.append(row[3])
# #     print("XAxis:", row[0], "YAxis:", row[1], "ZAxis:", row[2], "ZLoad:", row[3])

# # # # Step 4: Print the first few rows of the separated data to verify it
# # # print("x:", x[:40])      
# # # print("y:", y[:40])      
# # # print("z:", z[:40])      
# # # print("z_load:", z_load[:40])  

# #######################################################################################3333
# # Create a 3D plot
# fig = plt.figure()
# ax = fig.add_subplot(111, projection='3d')

# # Create a ScalarMappable to apply the colormap #
# norm = colors.Normalize(min(z_load), max(z_load))
# cmap = plt.cm.coolwarm  #viridis Spectral  bwr  seismic  rainbow
# sm = plt.cm.ScalarMappable(cmap=cmap, norm=norm)
# sm.set_array([])

# # Plot the 3D line with colormap gradation
# for i in range(len(x) - 1):
#     ax.plot(x[i:i+2], y[i:i+2], z[i:i+2], color=cmap(norm(z_load[i])), linewidth=2)

# # # # Mark points with z_load labels and (x, y, z) values
# # for i, txt in enumerate(z_load):
# #     label = f'{txt}\n({x[i]}, {y[i]}, {z[i]})'
# #     ax.text(x[i], y[i], z[i], label)

# # Mark points with z_load labels
# # for i, txt in enumerate(z_load):
# #     label = f'{txt}'
# #     ax.text(x[i], y[i], z[i], label)

# # Add a colorbar to the plot
# cbar = fig.colorbar(sm, ax=ax, label='Z Load')

# # Set labels for each axis
# ax.set_xlabel('X-axis')
# ax.set_ylabel('Y-axis')
# ax.set_zlabel('Z-axis')

# # mplcursors.cursor(hover=True, highlight=False).connect("add", lambda sel: sel.annotation.set_text(
# #     f'Z Load: {z_load[int(sel.target.index)]}\n(X: {x[int(sel.target.index)]}, Y: {y[int(sel.target.index)]}, Z: {z[int(sel.target.index)]})'))

# # Show the plot
# plt.show()

# # cursor.close()
# # conn.close()