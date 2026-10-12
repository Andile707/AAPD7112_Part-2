MUNICIPAL SERVICES APPLICATION
==============================

1. PROJECT OVERVIEW
-------------------

The Municipal Services Application is a C# Windows Forms application developed
using the .NET Framework.

The application is designed to provide South African residents with an
accessible platform for interacting with municipal services.

The current version of the application provides the following functionality:

- Report municipal issues.
- View local events and announcements.
- Search for local events by category and date.
- Receive event recommendations based on previous searches.

The Service Request Status feature will be implemented in a later part of
the project.


2. DEVELOPMENT TECHNOLOGIES
---------------------------

Application Name: MunicipalServicesApp
Application Type: Windows Forms Application
Programming Language: C#
Framework: .NET Framework 4.7.2
Development Environment: Microsoft Visual Studio


3. SYSTEM REQUIREMENTS
----------------------

The following software is required to compile and run the application:

- Microsoft Windows
- Microsoft Visual Studio
- .NET Framework 4.7.2
- C# Windows Forms development support


4. HOW TO OPEN THE PROJECT
--------------------------

1. Open Microsoft Visual Studio.

2. Select "Open a project or solution".

3. Navigate to the MunicipalServicesApp project folder.

4. Open the MunicipalServicesApp solution (.sln) file.

5. Wait for Visual Studio to load the solution and project files.


5. HOW TO COMPILE THE APPLICATION
---------------------------------

1. Open MunicipalServicesApp in Visual Studio.

2. Select:

   Build > Build Solution

   Alternatively, press Ctrl + Shift + B.

3. Confirm that the application builds successfully without errors.

The application should not be run if compilation errors are present.


6. HOW TO RUN THE APPLICATION
-----------------------------

1. Open the solution in Visual Studio.

2. Build the solution.

3. Press F5.

   Alternatively, select:

   Debug > Start Debugging

4. The Municipal Services Main Menu will appear.


7. MAIN MENU
------------

The Main Menu provides access to the municipal services implemented by the
application.

The available options are:

- Report an Issue
- Local Events and Announcements
- Service Request Status

Report an Issue was implemented in Part 1.

Local Events and Announcements is implemented in Part 2.

Service Request Status is reserved for future implementation.


8. REPORT AN ISSUE
------------------

The Report an Issue feature allows residents to submit municipal
service-delivery problems.

To report an issue:

1. Select "Report an Issue" from the Main Menu.

2. Enter the location where the problem occurred.

3. Select the appropriate issue category.

4. Enter a detailed description of the problem.

5. If required, click Browse to attach an image or document.

6. Monitor the progress indicator while completing the report.

7. Click Submit.

The application validates the required fields before accepting the report.


9. REPORT VALIDATION
--------------------

The following fields are required:

- Location
- Category
- Description

If required information is missing, the application displays an appropriate
warning message.

An attachment may also be selected to provide supporting information about
the reported issue.


10. ISSUE REFERENCE NUMBERS
---------------------------

Each successfully submitted municipal issue receives a unique reference
number.

Example:

MS-0001
MS-0002
MS-0003

Newly submitted reports receive an initial status of:

Submitted

These reference numbers provide a foundation for the Service Request Status
feature that will be implemented later.


11. ISSUE DATA STORAGE
----------------------

Reported issues are represented using the ReportedIssue class.

Submitted reports are stored in a List<ReportedIssue> through the
IssueRepository class.

Each reported issue contains information including:

- Reference number
- Location
- Category
- Description
- Attachment path
- Date reported
- Status

The current implementation stores this information in memory while the
application is running.


12. REPORTING PROGRESS INDICATOR
--------------------------------

The Report Issue form contains a dynamic progress indicator.

Progress increases when the resident provides information such as:

- Location
- Category
- Description
- Attachment

Encouraging messages are displayed as the user progresses through the report.

This feature is intended to improve user engagement and encourage residents
to complete their reports.


13. LOCAL EVENTS AND ANNOUNCEMENTS
----------------------------------

Part 2 introduces the Local Events and Announcements feature.

To access the feature:

1. Open the Main Menu.

2. Select "Local Events and Announcements".

3. Open the Local Events and Announcements page.

The page displays upcoming municipal events and announcements.

Information displayed for events may include:

- Event date
- Event title
- Category
- Location
- Description


14. SEARCHING FOR EVENTS
------------------------

Residents can search for events using:

- Category
- Date

To search:

1. Select a category from the Category dropdown.

2. Select the required date.

3. Click Search.

4. Matching events are displayed under Upcoming Events.

If no matching event exists, the application informs the user that no events
were found.

The "All Categories" option allows a search to be performed without limiting
the search to one particular event category.


15. SHOW ALL EVENTS
-------------------

The Show All button displays all upcoming events.

Events are displayed in chronological order, allowing residents to easily
identify which municipal events will occur next.


16. RECENT SEARCHES
-------------------

The application records recent event searches.

Recent searches are displayed on the Local Events and Announcements page so
that the user's search activity can be viewed.

A Stack<string> is used to manage search history.

A stack follows the Last-In, First-Out (LIFO) principle. This is appropriate
for search history because the user's most recent search is placed at the
top of the stack and can be displayed first.


17. EVENT RECOMMENDATIONS
-------------------------

The application provides a "Recommended For You" feature.

Recommendations are based on the categories that the user searches for.

Each time a specific category is searched, the application records the
search frequency for that category.

For example:

Sports       - 3 searches
Community    - 1 search
Utilities    - 1 search

In this example, Sports is identified as the user's preferred category.

The recommendation algorithm determines the category with the highest search
frequency and displays upcoming events belonging to that category.

The recommendations therefore change according to the user's search patterns
and preferences.


18. ADVANCED DATA STRUCTURES
----------------------------

Part 2 uses several advanced data structures to efficiently organise,
retrieve and manage municipal event information.


19. SORTED DICTIONARY
---------------------

A SortedDictionary<DateTime, List<LocalEvent>> is used to organise events
according to their dates.

Purpose:

- Groups events by date.
- Keeps event dates automatically sorted.
- Supports chronological display of upcoming events.
- Provides efficient retrieval of event information.

This allows upcoming municipal events to be presented in date order.


20. DICTIONARY
--------------

A Dictionary<string, int> is used to record event-category search
preferences.

The dictionary stores:

Category -> Number of searches

Example:

Sports -> 3
Community -> 2
Education -> 1

This information is analysed by the recommendation algorithm to determine
which type of event the user is most interested in.


21. HASHSET
-----------

A HashSet<string> is used to store unique event categories.

The HashSet prevents duplicate categories from being stored.

For example, if several events belong to the Sports category, "Sports" is
still stored only once in the category collection.

The unique categories are used to populate the Category dropdown on the
Local Events and Announcements page.


22. STACK
---------

A Stack<string> is used to store recent user searches.

The stack follows the Last-In, First-Out (LIFO) principle.

The most recent search is therefore stored at the top of the stack and can
be displayed before older searches.


23. QUEUE
---------

A Queue<LocalEvent> is used to manage upcoming municipal events.

A queue follows the First-In, First-Out (FIFO) principle.

Events are first organised chronologically and then placed into the upcoming
events queue.

This allows earlier upcoming events to be processed before later events.


24. DATA STRUCTURE SUMMARY
--------------------------

The following advanced data structures are used:

SortedDictionary<DateTime, List<LocalEvent>>
Purpose: Organises events chronologically.

Dictionary<string, int>
Purpose: Records category search frequencies for recommendations.

HashSet<string>
Purpose: Maintains unique event categories.

Stack<string>
Purpose: Maintains recent search history using LIFO behaviour.

Queue<LocalEvent>
Purpose: Manages upcoming events using FIFO behaviour.


25. RECOMMENDATION ALGORITHM
----------------------------

The recommendation process works as follows:

1. The user searches for an event category.

2. The category search is recorded.

3. The Dictionary stores or increases the search count for the category.

4. The recommendation algorithm examines the stored search frequencies.

5. The category with the highest number of searches is identified as the
   user's preferred category.

6. Upcoming events are searched for events belonging to that category.

7. Matching events are displayed under "Recommended For You".

Searching using "All Categories" does not count as a specific category
preference.


26. NAVIGATION
--------------

The Back to Main Menu button returns the user from the Local Events and
Announcements page to the Main Menu.

The Report Issue form also provides navigation back to the Main Menu.


27. EXITING THE APPLICATION
---------------------------

The application can be closed using the Exit option on the Main Menu.

A confirmation message is displayed before the application closes.


28. CURRENT FUNCTIONALITY
-------------------------

The current application includes:

- Municipal Services Main Menu
- Report Issue functionality
- Issue validation
- Image and document attachment
- Unique issue reference numbers
- Reporting progress indicator
- Local Events and Announcements page
- Upcoming event display
- Event category filtering
- Event date filtering
- Search functionality
- Show All functionality
- Recent search history
- User preference tracking
- Event recommendations
- Sorted event organisation
- Unique event categories
- Stack-based search history
- Queue-based upcoming event management
- Navigation between forms
- User feedback messages


29. FUTURE DEVELOPMENT
----------------------

The application will continue to be developed in later stages of the
Portfolio of Evidence.

Future functionality includes:

- Service Request Status
- Additional municipal service functionality
- Further improvements to citizen engagement and service delivery


30. IMPORTANT NOTE
------------------

The application must be successfully compiled before it is run.

Before submission, use:

Build > Rebuild Solution

and confirm that the project builds successfully without errors.

Keep a backup copy of the complete MunicipalServicesApp solution because the
application will be extended in later parts of the Portfolio of Evidence.