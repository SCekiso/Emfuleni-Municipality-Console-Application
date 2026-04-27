using System;
using System.Collections.Generic;
using System.Linq;

namespace Emfuleni_Municipality
{
    // This class represents a resident of the municipality, containing their name, address, account number, and monthly utility bill.
    public class Resident
    {
        // Properties to store resident information
        public string Name { get; set; }
        public string Address { get; set; }
        public int AccountNumber { get; set; }
        public double MonthlyUtility { get; set; }

        // Constructor to initialize a new resident with their details
        public Resident(string name, string address, int accountNumber, double monthlyUtility)
        {
            // Initialize the properties with the provided values
            Name = name;
            Address = address;
            AccountNumber = accountNumber;
            MonthlyUtility = monthlyUtility;
        }
    }
    // This class represents a service request made by a resident, containing details about the requester, type of request, priority, severity, estimated time for resolution and an urgency score.

    public class ServiceRequest
    {
        // Properties to store service request information
        public string Requester { get; set; }
        public string RequestType { get; set; }
        public int PriorityLevel { get; set; }
        public int SeverityLevel { get; set; }
        public double EstimatedTime { get; set; }
        public double UrgencyScore { get; set; }
        // Constructor to initialize a new service request with the provided details
        public ServiceRequest(string requester, string requestType, int priorityLevel, int severityLevel, double estimatedTime)
        {
            // Initialize the properties with the provided values
            Requester = requester;
            RequestType = requestType;
            PriorityLevel = priorityLevel;
            SeverityLevel = severityLevel;
            EstimatedTime = estimatedTime;
            UrgencyScore = 0.0;
        }
    }
    // This class contains methods to calculate the urgency of a service request and to generate a report for a service request.q
    public class MonthlyUtilitiesUsage
    {
        // This method calculates the urgency score of a service request based on its priority, severity, and estimated time for resolution.
        public void CalculateUrgency(ServiceRequest request)
        {
            // The urgency score is calculated using a weighted formula where priority contributes 50%, severity contributes 30% and estimated time contributes 20% to the final score.
            request.UrgencyScore = (request.PriorityLevel * 0.5) + (request.SeverityLevel * 0.3) + (request.EstimatedTime * 0.2);
        }
        // This method generates a report for a given service request, displaying its details and urgency score.

        public void ServiceReport(ServiceRequest request)
        {
            // Shows the details of the service request in a formatted manner, including the requester, type of request, urgency score, and estimated time for resolution.
            Console.WriteLine("\n--- ResidentService Report ---");
            Console.WriteLine($"Service Report for: {request.Requester}");
            Console.WriteLine($"Request Type: {request.RequestType}");
            Console.WriteLine($"Urgency Score: {request.UrgencyScore:F2}");
            Console.WriteLine($"Resolution Time: {request.EstimatedTime} hours");
        }
    }

    class Program
    {
        // The main entry point of the application where the user interacts with the console to input resident and service request details and processes the service requests based on their urgency.
        public static void Main(string[] args)
        {
            // Initialize the manager for handling service requests and lists to store residents, pending service requests and resolved service requests.
            MonthlyUtilitiesUsage manager = new MonthlyUtilitiesUsage();
            List<Resident> residents = new List<Resident>();
            List<ServiceRequest> pendingQueue = new List<ServiceRequest>();
            List<ServiceRequest> resolvedRequests = new List<ServiceRequest>();

            Console.WriteLine("Welcome to the Emfuleni Municipality Service Help Desk. Your complaints, our priority!");

            Console.Write("How many residents would you like to assist? ");
            int residentCount = int.Parse(Console.ReadLine());

            // Loop to gather details for each resident and store them in the residents list.
            for (int i = 0; i < residentCount; i++)
            {
                
                Console.WriteLine($"\nDetails for resident {i + 1}:");
                Console.Write("Name and Surname: "); string name = Console.ReadLine();
                Console.Write("Address: "); string address = Console.ReadLine();
                Console.Write("Account Number: "); int accountNumber = int.Parse(Console.ReadLine());
                Console.Write("Monthly Bill: "); double bill = double.Parse(Console.ReadLine());

                // Create a new Resident object with the provided details and add it to the residents list.
                residents.Add(new Resident(name, address, accountNumber, bill));
            }



            Console.Write("\nHow many service requests would you like to record? ");
            int requestCount = int.Parse(Console.ReadLine());
            // Loop to gather details for each service request, calculate its urgency and store it in the pendingQueue list.

            for (int i = 0; i < requestCount; i++)
            {
                Console.WriteLine($"\nService Request {i + 1}:");
                Console.Write("Service Requester Name and Surname: "); string requestorName = Console.ReadLine();
                Console.Write("Request Type: "); string requesttype = Console.ReadLine();
                Console.Write("Priority (1-5): "); int priority = int.Parse(Console.ReadLine());
                Console.Write("Severity (1-10): "); int severity = int.Parse(Console.ReadLine());
                Console.Write("Estimated Time (Hours): "); double time = double.Parse(Console.ReadLine());

                // Creates a new ServiceRequest object with the provided details, calculate its urgency using the manager and add it to the pendingQueue list.
                ServiceRequest req = new ServiceRequest(requestorName, requesttype, priority, severity, time);
                manager.CalculateUrgency(req);
                pendingQueue.Add(req);
            }

            // Loop to process service requests from the pendingQueue until it is empty, allowing the user to select which request to process based on its index in the list.
            while (pendingQueue.Count > 0)
            {
                // Shows the list of pending service requests with their details and urgency scores.
                Console.WriteLine("\n+++ Services in Queue +++");
                // Loop through the pendingQueue and display each service request with its index, type, requester and urgency score.
                for (int i = 0; i < pendingQueue.Count; i++)
                {
                    Console.WriteLine($"[{i}] {pendingQueue[i].RequestType} from {pendingQueue[i].Requester} (Score: {pendingQueue[i].UrgencyScore:F2})");
                }

                Console.Write("\nPick an index of the request to process: ");
                int choice = int.Parse(Console.ReadLine());

                
                if (choice >= 0 && choice < pendingQueue.Count)
                {
                    //this block processes the selected service request by generating a report for it, adding it to the resolvedRequests list and removing it from the pendingQueue.
                    ServiceRequest current = pendingQueue[choice];
                    manager.ServiceReport(current);

                    // Add the current request to the resolvedRequests list and remove it from the pendingQueue.
                    resolvedRequests.Add(current);
                    pendingQueue.RemoveAt(choice);

                    // Added the pause here so you can read the report
                    Console.WriteLine("\nRequest processed successfully. Click enter to continue");
                    
                }
                else
                {
                    Console.WriteLine("Not correct index. Please try again.");
                }
            }

            Console.WriteLine("\n+++ The Final Summary +++");
            Console.WriteLine($"Total requests we have resolved: {resolvedRequests.Count}");

            // If there are any resolved requests, find the one with the highest urgency score and display its details.
            if (resolvedRequests.Count > 0)
            {
                // Use LINQ to order the resolved requests by urgency score in descending order and select the first one (the highest).
                var highest = resolvedRequests.OrderByDescending(r => r.UrgencyScore).First();
                Console.WriteLine($"The highest urgency request was '{highest.RequestType}' for {highest.Requester} with a score of {highest.UrgencyScore:F2}");
            }

            Console.WriteLine("\nThank you for using the Emfuleni Municipality Service Help Desk. Until next time!");
           
        }
    }
}