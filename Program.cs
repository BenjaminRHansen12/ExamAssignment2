using HTTPUtils;
using System.Text.Json;
using AnsiTools;
using Colors = AnsiTools.ANSICodes.Colors;
using System;
using System.Globalization;
using System.Linq;

Console.Clear();
Console.WriteLine("Starting Assignment 2");

const string myPersonalID = "d3bb1dd9b2120d42eb228404d5c2ba9686e59ed39e53510e486835fa41277aa1";
const string baseURL = "https://mm-203-module-2-server.onrender.com/";
const string startEndpoint = "start/";
const string taskEndpoint = "task/";   

HttpUtils httpUtils = HttpUtils.instance;

//#### REGISTRATION
Response startRespons = await httpUtils.Get(baseURL + startEndpoint + myPersonalID);
Console.WriteLine($"Start:\n{Colors.Green}{startRespons}{ANSICodes.Reset}\n\n"); 
string taskID = "KO1pD3"; 

//#### FIRST TASK 
Console.WriteLine($"{Colors.Magenta}Task ID 1: {taskID}{ANSICodes.Reset}");

Response task1Response = await httpUtils.Get(baseURL + taskEndpoint + myPersonalID + "/" + taskID); 
Console.WriteLine(task1Response);

var taskDetails = JsonSerializer.Deserialize<TaskDetails>(task1Response.content);
var numbersTask1 = taskDetails.parameters.Split(',').Select(int.Parse).ToArray();

var difference = numbersTask1[1] - numbersTask1[0];
var nextNumber = numbersTask1.Last() + difference;

Console.WriteLine($"{Colors.Blue}The next number in the series is: {nextNumber}{ANSICodes.Reset}");

Response task1AnswerResponse = await httpUtils.Post(baseURL + taskEndpoint + myPersonalID + "/" + taskID, nextNumber.ToString());
Console.WriteLine($"Answer: {Colors.Green}{task1AnswerResponse}{ANSICodes.Reset}");

MakeLine();

//#### SECOND TASK 

var taskID2 = JsonSerializer.Deserialize<Task>(task1AnswerResponse.content);
Console.WriteLine($"{Colors.Magenta}Task ID 2: {taskID2.taskID}{ANSICodes.Reset}");

Response task2Response = await httpUtils.Get(baseURL + taskEndpoint + myPersonalID + "/" + taskID2.taskID); 
Console.WriteLine(task2Response);

var task2Details = JsonSerializer.Deserialize<TaskDetails>(task2Response.content);
var numbersTask2 = double.Parse(task2Details.parameters);

var fahrenheit = numbersTask2;
var celsius = Math.Round((fahrenheit - 32) * 5 / 9, 2).ToString("0.00", CultureInfo.InvariantCulture);

Console.WriteLine($"{Colors.Blue}The number in celsius is: {celsius}{ANSICodes.Reset}");

Response task2AnswerResponse = await httpUtils.Post(baseURL + taskEndpoint + myPersonalID + "/" + taskID2.taskID, celsius.ToString());
Console.WriteLine($"Answer: {Colors.Green}{task2AnswerResponse}{ANSICodes.Reset}");

MakeLine();

//#### THIRD TASK 

var taskID3 = JsonSerializer.Deserialize<Task>(task2AnswerResponse.content);
Console.WriteLine($"{Colors.Magenta}Task ID 3: {taskID3.taskID}{ANSICodes.Reset}");

Response task3Response = await httpUtils.Get(baseURL + taskEndpoint + myPersonalID + "/" + taskID3.taskID); 
Console.WriteLine(task3Response);

var task3Details = JsonSerializer.Deserialize<TaskDetails>(task3Response.content);
var numbersTask3 = task3Details.parameters.Split(',').Select(int.Parse).OrderBy(x => x).ToArray();

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

var thePrimeNumbers = numbersTask3.Where(IsPrime).ToArray();

string primeNumbersString = string.Join(",", thePrimeNumbers);

Console.WriteLine($"{Colors.Blue}The prime numbers:{ANSICodes.Reset}");
foreach (var prime in thePrimeNumbers)
{
    Console.WriteLine($"{Colors.Blue}{prime}{ANSICodes.Reset}");
}

Response task3AnswerResponse = await httpUtils.Post(baseURL + taskEndpoint + myPersonalID + "/" + taskID3.taskID, primeNumbersString);
Console.WriteLine($"Answer: {Colors.Green}{task3AnswerResponse}{ANSICodes.Reset}");

MakeLine();

//#### FOUR TASK 

var taskID4 = JsonSerializer.Deserialize<Task>(task3AnswerResponse.content);
Console.WriteLine($"{Colors.Magenta}Task ID 4: {taskID4.taskID}{ANSICodes.Reset}");

Response task4Response = await httpUtils.Get(baseURL + taskEndpoint + myPersonalID + "/" + taskID4.taskID); 
Console.WriteLine(task4Response);

var task4Details = JsonSerializer.Deserialize<TaskDetails>(task4Response.content);
var numbersTask4 = task4Details.parameters.Split(',').Select(int.Parse).ToArray();

int theSum = 0;
foreach (int numbersAndStuff in numbersTask4)
{
    theSum += numbersAndStuff;
}
Console.WriteLine($"{Colors.Blue}Sum of numbers: {theSum}{ANSICodes.Reset}");

Response task4AnswerResponse = await httpUtils.Post(baseURL + taskEndpoint + myPersonalID + "/" + taskID4.taskID, theSum.ToString());
Console.WriteLine($"Answer: {Colors.Green}{task4AnswerResponse}{ANSICodes.Reset}");

//#### Functions

static void MakeLine()
{
    int width = Console.WindowWidth;
    string line = new string('-', width);

    Console.WriteLine("\n" + line + "\n");
}