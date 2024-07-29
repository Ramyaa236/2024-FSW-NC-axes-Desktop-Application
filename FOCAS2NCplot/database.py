# import sqlite3
# import matplotlib.pyplot as plt
# from mpl_toolkits.mplot3d import Axes3D
# import numpy as np
# from matplotlib import cm

# # Step 1: Connect to the SQLite Database
# conn = sqlite3.connect("U:\\hbsgi13C_AF制御技術部\\501工機標準\\標準不具合、変更\\20230711 FOCAS2活用\\ラムヤ\\NCaxisappli\\mydata.db")
# cursor = conn.cursor()

# # Step 2: Retrieve/ Read Data from the Database
# cursor.execute("SELECT XAxis, YAxis, ZAxis, ZLoad FROM Data_20231109105425")
# data = cursor.fetchall()

# # Print the first few rows of the data to verify it
# print(data[:21])  # Display the first 5 rows

################################################################
import sqlite3
import matplotlib.pyplot as plt
from mpl_toolkits.mplot3d import Axes3D
import numpy as np
from matplotlib import cm

# Step 1: Connect to the SQLite Database
conn = sqlite3.connect("U:\\hbsgi13C_AF制御技術部\\501工機標準\\標準不具合、変更\\20230711 FOCAS2活用\\ラムヤ\\NCaxisappli\\mydata.db")
cursor = conn.cursor()

# Get the latest table name
cursor.execute("SELECT name FROM sqlite_master WHERE type='table' AND name LIKE 'Data_%' ORDER BY name DESC LIMIT 1")
latest_table = cursor.fetchone()

if latest_table:
    latest_table_name = latest_table[0]

    # Retrieve specific columns from the latest table
    cursor.execute(f"SELECT XAxis, YAxis, ZAxis, ZLoad FROM {latest_table_name}")
    latest_data = cursor.fetchall()

    if latest_data:
        for row in latest_data:
            print("XAxis:", row[0], "YAxis:", row[1], "ZAxis:", row[2], "ZLoad:", row[3])
    else:
        print("No data found in the latest table.")

else:
    print("No tables found with the 'Data_' prefix.")

# Close the cursor and connection
cursor.close()
conn.close()

################################################################

# # Step 2: Retrieve/ Read Data from the Database
# cursor.execute("SELECT XAxis, YAxis, ZAxis, ZLoad FROM Data_20231109105425")
# data = cursor.fetchall()

# # Step 3: Separate data into lists
# x = []
# y = []
# z = []
# z_load = []

# for row in data:
#     x.append(row[0])
#     y.append(row[1])
#     z.append(row[2])
#     z_load.append(row[3])

# # Step 4: Print the first few rows of the separated data to verify it
# print("x:", x[:21])      # Display the first 5 rows
# print("y:", y[:21])      # Display the first 5 rows
# print("z:", z[:21])      # Display the first 5 rows
# print("z_load:", z_load[:21])  # Display the first 5 rows



# # Step 1: Connect to the SQLite Database
# conn = sqlite3.connect("U:\\hbsgi13C_AF制御技術部\\501工機標準\\標準不具合、変更\\20230711 FOCAS2活用\\ラムヤ\\NCaxisappli\\mydata.db")
# cursor = conn.cursor()

# # Step 2: Retrieve/ Read Data from the Database
# cursor.execute("SELECT XAxis, YAxis, ZAxis, ZLoad FROM Data_20231109105425")
# data = cursor.fetchall()


# # Print the first few rows of the data to verify it
# print(data[:21])  # Display the first 5 rows

# # Step 3: Create the 3D Plot
# x = [float(row[0]) for row in data]
# y = [float(row[0]) for row in data]
# z = [float(row[0]) for row in data]
# z_load = [float(row[0]) for row in data]