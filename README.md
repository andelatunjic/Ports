## Ports
*January, 2023.*

The purpose of this task is to recognize and solve given problems with different design patterns.
The solution is console application with MVC arhitecture. Application loads data from csv files and stores it using singleton and composite. 

Design pattern | Purpuse 
-------------- | -------
Singleton | Stored data, errors, virtual time
Facade | Loding data
Visitor | Command execution, summation of occupied berths
Builder | Building table and it's parts: header, body, footer
Chain of responsibility | Statistics of different ship categories
Observer | Notify system
Composite | Port (composite) has piers and one pier (composite) has berths (leaf)
Iterator | Iterating through the composite
MVC | Arhitecture, VT100 terminal
Memento | Saving all berths statuses (free/occupied) in current virtual time
