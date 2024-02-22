using HTTPUtils;
using System.Text.Json;
using AnsiTools;
using Colors = AnsiTools.ANSICodes.Colors;
using System;
using System.Globalization;
using System.Linq;

Console.Clear();
Console.WriteLine("Starting Assignment 2");

// SETUP 
const string myPersonalID = "d3bb1dd9b2120d42eb228404d5c2ba9686e59ed39e53510e486835fa41277aa1"; // GET YOUR PERSONAL ID FROM THE ASSIGNMENT PAGE https://mm-203-module-2-server.onrender.com/
const string baseURL = "https://mm-203-module-2-server.onrender.com/";
const string startEndpoint = "start/"; // baseURl + startEndpoint + myPersonalID
const string taskEndpoint = "task/";   // baseURl + taskEndpoint + myPersonalID + "/" + taskID

// Creating a variable for the HttpUtils so that we dont have to type HttpUtils.instance every time we want to use it
HttpUtils httpUtils = HttpUtils.instance;

//#### REGISTRATION
// We start by registering and getting the first task
Response startRespons = await httpUtils.Get(baseURL + startEndpoint + myPersonalID);
Console.WriteLine($"Start:\n{Colors.Magenta}{startRespons}{ANSICodes.Reset}\n\n"); // Print the response from the server to the console
string taskID = "KO1pD3"; // We get the taskID from the previous response and use it to get the task (look at the console output to find the taskID)

//#### FIRST TASK 
Response task1Response = await httpUtils.Get(baseURL + taskEndpoint + myPersonalID + "/" + taskID); 
Console.WriteLine(task1Response);

var taskDetails = JsonSerializer.Deserialize<TaskDetails>(task1Response.content);
var numbers = taskDetails.parameters.Split(',').Select(int.Parse).ToArray();

var difference = numbers[1] - numbers[0];
var nextNumber = numbers.Last() + difference;

Console.WriteLine($"The next number in the series is: {nextNumber}");

Response task1AnswerResponse = await httpUtils.Post(baseURL + taskEndpoint + myPersonalID + "/" + taskID, nextNumber.ToString());
Console.WriteLine($"Answer: {Colors.Green}{task1AnswerResponse}{ANSICodes.Reset}");

Console.WriteLine("\n-----------------------------------\n");

//#### SECOND TASK 

var taskID2 = JsonSerializer.Deserialize<Task>(task1AnswerResponse.content);
Console.WriteLine($"Task ID {taskID2.taskID}");

Response task2Response = await httpUtils.Get(baseURL + taskEndpoint + myPersonalID + "/" + taskID2.taskID); 
Console.WriteLine(task2Response);

var task2Details = JsonSerializer.Deserialize<TaskDetails>(task2Response.content);
var number = double.Parse(task2Details.parameters);

var fahrenheit = number;
var celsius = Math.Round((fahrenheit - 32) * 5 / 9, 2).ToString("0.00", CultureInfo.InvariantCulture);

Console.WriteLine($"The number in celsius is: {celsius}");

Response task2AnswerResponse = await httpUtils.Post(baseURL + taskEndpoint + myPersonalID + "/" + taskID2.taskID, celsius.ToString());
Console.WriteLine($"Answer: {Colors.Green}{task2AnswerResponse}{ANSICodes.Reset}");


Console.WriteLine("\n-----------------------------------\n");

//#### THIRD TASK 

var taskID3 = JsonSerializer.Deserialize<Task>(task2AnswerResponse.content);
Console.WriteLine($"Task ID {taskID3.taskID}");

Response task3Response = await httpUtils.Get(baseURL + taskEndpoint + myPersonalID + "/" + taskID3.taskID); 
Console.WriteLine(task3Response);

var task3Details = JsonSerializer.Deserialize<TaskDetails>(task3Response.content);
var primeNumbers = task3Details.parameters.Split(',').Select(int.Parse).OrderBy(x => x).ToArray();

bool IsPrime(int n)
{
    if (n <= 1)
        return false;
    if (n <= 3)
        return true;
    if (n % 2 == 0 || n % 3 == 0)
        return false;
    for (int i = 5; i * i <= n; i += 6)
    {
        if (n % i == 0 || n % (i + 2) == 0)
            return false;
    }
    return true;
}

var thePrimeNumbers = primeNumbers.Where(IsPrime).ToArray();

string primeNumbersString = string.Join(",", thePrimeNumbers);

Console.WriteLine("The prime numbers:");
foreach (var prime in thePrimeNumbers)
{
    Console.WriteLine(prime);
}

Response task3AnswerResponse = await httpUtils.Post(baseURL + taskEndpoint + myPersonalID + "/" + taskID3.taskID, primeNumbersString);
Console.WriteLine($"Answer: {Colors.Green}{task3AnswerResponse}{ANSICodes.Reset}");

Console.WriteLine("\n-----------------------------------\n");

//#### FOUR TASK 

var taskID4 = JsonSerializer.Deserialize<Task>(task3AnswerResponse.content);
Console.WriteLine($"Task ID {taskID4.taskID}");

Response task4Response = await httpUtils.Get(baseURL + taskEndpoint + myPersonalID + "/" + taskID4.taskID); 
Console.WriteLine(task4Response);

var task4Details = JsonSerializer.Deserialize<TaskDetails>(task4Response.content);
var theSumOfNumbers = task4Details.parameters.Split(',').Select(int.Parse).ToArray();

int theSum = 0;
foreach (int numbersAndStuff in theSumOfNumbers)
{
    theSum += numbersAndStuff;
}
Console.WriteLine($"Sum of numbers: {theSum}");

Response task4AnswerResponse = await httpUtils.Post(baseURL + taskEndpoint + myPersonalID + "/" + taskID4.taskID, theSum.ToString());
Console.WriteLine($"Answer: {Colors.Green}{task4AnswerResponse}{ANSICodes.Reset}");

class Task
{
    public string taskID { get; set; }

}

class TaskDetails
{
    public string title { get; set; }
    public string description { get; set; }
    public string taskID { get; set; }
    public string usierID { get; set; }
    public string parameters { get; set; }
}