# ScoutSheet
 Scoutsheet of Team 2638 for the 2020 Infinite Recharge. This app is designed to streamline record robot activity data during matches (with graphical pictures to help with data inputting) and exporting of that data. Developed with the Xamarin Framework.
 
 # Codebase
 ## MainPage.xaml
 Houses the information for the structure of the tabbed page to contain the other three listed pages. Also sets the image icon source being set in the corresponding C# file.
 
## Scout.xaml
 This is the main page for users to input their data. The general gist of the UI is that the more bigger sections are stacked together in a StackLayout (sections like the autonomous, tele-op, endgame). For employing the buttons inside of the image (which is listed as a static resource), I used a grid to position the buttons on top of the image (sometimes with many rows and columns). Each button had its own id which can then be used by the C# file to compile all of the data into one source.
 
 ## Scout.xaml.cs
  This contains a lot of boiler plate code that I used to set and reset different properties (like lines 45-80 setting the default color for all buttons and the whole ResetData() function). It also contains functions for handling button presses for all the buttons (for changing color, adding values to buttons, etc.). Some highlighted functions are described below. 
  
### RecordAllData()

Manually gets all of the user input data from all the UI elements and returns all the data in a Matches object (can then be used to serialize and turn into a CSV)

### SaveData_clicked(object sender, EventArgs e)

This method uses a package called SQLite to initiate a database connection and compile all of the data necessary. It then either updates the record in the database if the match Id (the primary key) already exists. If not, then it inserts the data

### Export_Clicked(object sender, EventArgs e)

First calls SaveData_Clicked() to make sure that the data is saved before attempting to convert the data into a CSV. Calls seralizeCsv() which writes the data to the CSV then creates a prompt for which users can share the csv file via bluetooth or other means.

## Matches.cs
Holds the information for how the data is stored in the SQLite server. In addition, it also contains SerializeCsv(), which utilizes the CSVWriter package to write all the data to the csv file.

## PastMatches.xaml/PastMatches.xaml.cs
Uses the SQLite library to provide the functionality for the front end. At the start, it would get all the matches as a list and set the ItemSource of the ListView. Then depending on what feature is requested (delete one, delete all, select an item, export all), the code would write the accompanying SQLite command necessary to get this.

##Settings.xaml/Settings.xaml.cs
Shows some settings. Though one function also changes PressLength variable found in App.xaml.cs
