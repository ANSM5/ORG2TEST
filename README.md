# ORG2TEST
Read me file for ANSM5

The console application can be run by opening a command prompt and running the executable in the Application folder.
It accepts two command line arguments: the first is the JSON file and the second is the target file to be created.

Example from the Application folder: Run_ME C:\Temp\projects.json C:\Temp\report.txt

The application parses the JSON file, creates the report, and saves it as a text file to the target provided.

The source code is provided in the Source Code folder. It is written in Visual Studio 2019 using C# on the .net core 3.0
It should work on Windows, Linux or MAC due to the Core framework, however I've only tested it on Windows.

There were assumptions made on two of the questions as they were unclear:

Question 2:

This has been intepreted as three separate questions -

	a) Successful deployments by project group
	b) Successful deployments by environment
	c) Successful deployments by year
	
	It was interpreted that way as a collation of all the data would have led to a massive report with lots of redundancy
	that would have had to have been further interpreted to make it useful

Question 5: 

	This has been intepreted as: "Please provide a break down by project group of success and unsuccessful RELEASES (successful being
	releases that are deployed to live)", as the deployments have their own state for success.
