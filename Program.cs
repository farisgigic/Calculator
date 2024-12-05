using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

class Program
{
    static List<Calculation> history = new List<Calculation>();
    static string historyFile = "history.json";

    static void Main(string[] args)
    {
        LoadHistory();

        while (true)
        {
            Console.WriteLine("1. Perform Basic Calculation");
            Console.WriteLine("2. Calculate Square Root");
            Console.WriteLine("3. View History");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    PerformBasicCalculation();
                    break;
                case "2":
                    CalculateSquareRoot();
                    break;
                case "3":
                    ViewHistory();
                    break;
                case "4":
                    SaveHistory();
                    return;
                default:
                    Console.WriteLine("Invalid Input. Please enter a number between 1 and 4.");
                    break;
            }
        }
    }

    static void PerformBasicCalculation()
    {
        try
        {
            Console.Write("Enter your calculation (e.g., 5 + 3): ");
            string input = Console.ReadLine();
            var parts = input.Split(' ');
            double num1 = Convert.ToDouble(parts[0]);
            string op = parts[1];
            double num2 = Convert.ToDouble(parts[2]);
            double result = 0;

            switch (op)
            {
                case "+":
                    result = num1 + num2;
                    break;
                case "-":
                    result = num1 - num2;
                    break;
                case "*":
                    result = num1 * num2;
                    break;
                case "/":
                    if (num2 == 0)
                    {
                        Console.WriteLine("Error: Division by zero is not allowed.");
                        return;
                    }
                    result = num1 / num2;
                    break;
                default:
                    Console.WriteLine("Invalid operator. Please use +, -, *, or /.");
                    return;
            }

            Console.WriteLine($"Result: {result}");
            SaveCalculation(input, result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void CalculateSquareRoot()
    {
        try
        {
            Console.Write("Enter number: ");
            double num = Convert.ToDouble(Console.ReadLine());

            if (num < 0)
            {
                Console.WriteLine("Error: Cannot calculate the square root of a negative number.");
                return;
            }

            double result = Math.Sqrt(num);
            Console.WriteLine($"Square Root: √{num} = {result}");
            SaveCalculation($"√{num}", result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void SaveCalculation(string expression, double result)
    {
        var calculation = new Calculation { Expression = expression, Result = result };
        history.Add(calculation);
    }

    static void ViewHistory()
    {
        if (history.Count == 0)
        {
            Console.WriteLine("No history available.");
            return;
        }

        for (int i = 0; i < history.Count; i++)
        {
            var record = history[i];
            Console.WriteLine($"{i + 1}: {record.Expression} = {record.Result}");
            Console.WriteLine("\n");
        }
        Console.WriteLine("-----------------------------");
    }

    static void SaveHistory()
    {
        var json = JsonConvert.SerializeObject(history, Formatting.Indented);
        File.WriteAllText(historyFile, json);
    }

    static void LoadHistory()
    {
        if (File.Exists(historyFile))
        {
            var json = File.ReadAllText(historyFile);
            history = JsonConvert.DeserializeObject<List<Calculation>>(json);
        }
    }
}

public class Calculation
{
    public string Expression { get; set; }
    public double Result { get; set; }
}
