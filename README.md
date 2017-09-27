# SharpViz-ColorBar
A Visual Studio C# project for viewing and exploring numeric range and distribtuion in a single data series.

Imports up to 10,000 records from a plain text file and displays a color bar showing the range of values by color intensity.  
Each value is mapped to a color intensity based on lightest colors for minimum values to darkest colors for the maximum values.

![ColorBar screenshot example](/images/sharpviz-colorbar-screenshot-1.png "ColorBar with car prices data series")

Value order can be shown as-is or sorted min to max.  

![ColorBar sort example](/images/sharpviz-colorbar-screenshot-2.png "Car prices sorted in ascending order")

Here's an example of using the Iris data set where you can see the clustering of petal width values for the three different classes (Setosa, Versicolor and Virginica).  

![ColorBar Iris data set example](/images/sharpviz-colorbar-screenshot-3.png "Iris petal width values and clustering")

## Running the Project
This project was created in Microsoft Visual Studio 2010 C#.  
To open and run the project locally, download the **code** folder and double-click the solution file "**ColorBar.sln**".
