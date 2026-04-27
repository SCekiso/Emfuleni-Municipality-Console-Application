Emfuleni Municipality Service Help Desk

Project Overview
A C# Console Application designed to manage and prioritize resident service requests for the Emfuleni Municipality. 
The system captures resident details and uses a weighted algorithm to prioritize service delivery based on urgency.

Key Features
Resident Database:Stores names, addresses and billing info using a custom `Resident` class.
Smart Prioritization: Calculates an "Urgency Score" for every request.
Interactive Queue: It has a Real-time management of pending requests where administrators can select and resolve issues by index.
Final Analytics:Uses "LINQ" to identify the most urgent task resolved during the session.

Tech Logic: (The Urgency Formula)
The system doesn't just treat every request the same. It uses a weighted formula to calculate the `UrgencyScore`:
$$UrgencyScore = (Priority \times 0.5) + (Severity \times 0.3) + (EstimatedTime \times 0.2)$$
Priority :The official rank of the task.
Severity :How bad the damage or situation is.
Time : How long the resolution is expected to take.

Two Fun Observations about my Code:

1. I used "James Bond" as a test in my output. It's always fun to use famous names as test data—it makes debugging much less boring. 
2. The Weighted Formula: The formula is very balanced. By giving "Priority" the highest weight (50%), I ensure that the municipality's official classification takes the lead,
but the "Severity"(30%) still allows a "Sewage Burst" to jump ahead of a "Pot Hole" if things get messy!
