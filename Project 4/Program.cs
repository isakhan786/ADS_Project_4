using System;
using System.Diagnostics;

// Class containing sorting algorithms
public class SortingAlgorithm
{
    // Method for Bubble Sort
    public static void BubbleSort(int[] data)
    {
        int n = data.Length;
        bool noswap;
        do
        {
            noswap = true;
            for (int i = 0; i < n - 1; i++)
            {
                if (data[i] > data[i + 1])
                {
                    // Swap the elements if the current element is greater than the next element
                    int temp = data[i];
                    data[i] = data[i + 1];
                    data[i + 1] = temp;
                    noswap = false;
                }
            }
        } while (!noswap); // Repeat until no swaps are made
    }

    // Method to swap two elements in an array
    public static void Swap(int[] data, int i, int j)
    {
        int temp = data[i];
        data[i] = data[j];
        data[j] = temp;
    }

    // Partition method used in Quick Sort
    public static int Partition(int[] data, int low, int high)
    {
        int pivot = data[high];
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            // If current element is smaller than the pivot then swap the elements
            if (data[j] < pivot)
            {
                i++;
                Swap(data, i, j);
            }
        }

        Swap(data, i + 1, high); // Swap the pivot element with the element at i+1
        return i + 1; // Return the index of the pivot element
    }

    // Method for Quick Sort
    public static void QuickSort(int[] data, int low, int high)
    { 
        // Base case: If low is greater than or equal to high then return
        if (low < high)
        {
            int pi = Partition(data, low, high); // pi is the partitioning index

            QuickSort(data, low, pi - 1); // Sort elements before partition
            QuickSort(data, pi + 1, high); // Sort elements after partition
        }
    }
}

public class Program
{
    // Method to run the experiment for a given size
    private static void RunExperiment(int size, StreamWriter fileWriter)
    {
        int[] data = GenerateRandomArray(size); // Generate random data

        // Bubble Sort
        int[] bubbleSortData = (int[])data.Clone(); // Clone the data for Bubble Sort
        Stopwatch stopwatch = Stopwatch.StartNew(); // Start the stopwatch
        SortingAlgorithm.BubbleSort(bubbleSortData); // Sort the data using Bubble Sort
        stopwatch.Stop(); // Stop the stopwatch
        long bubbleSortTime = stopwatch.ElapsedMilliseconds; // Get the elapsed time in milliseconds
        Console.WriteLine($"Bubble Sort for {size} elements took {bubbleSortTime} ms");

        // Quick Sort
        int[] quickSortData = (int[])data.Clone(); // Clone the data for Quick Sort
        stopwatch.Restart(); // Restart the stopwatch
        SortingAlgorithm.QuickSort(quickSortData, 0, quickSortData.Length - 1); // Sort the data using Quick Sort
        stopwatch.Stop(); // Stop the stopwatch
        long quickSortTime = stopwatch.ElapsedMilliseconds; // Get the elapsed time in milliseconds
        Console.WriteLine($"Quick Sort for {size} elements took {quickSortTime} ms");

        // Record the result in CSV format: Size, BubbleSortTime, QuickSortTime
        fileWriter.WriteLine($"{size},{bubbleSortTime},{quickSortTime}");
    }

    public static void Main()
    {
        int[] dataSizes = { 500, 1000, 2000, 5000, 10000}; // Data sizes to run the experiment for
        // Open a file to write the results to in CSV format
        using (StreamWriter fileWriter = new StreamWriter("SortingResults.csv"))
        {
            fileWriter.WriteLine("Size(n),BubbleSortTime(ms),QuickSortTime(ms)"); // Header for the CSV file
            // Run the experiment for each size
            foreach (int size in dataSizes)
            {
                RunExperiment(size, fileWriter);
            }
        }
        Console.WriteLine("Experiments completed and results saved to SortingResults.csv");
    }


    private static int[] GenerateRandomArray(int size)
    {
        Random random = new Random(); // Random number generator
        int[] data = new int[size]; // Array to store the random data
        for (int i = 0; i < size; i++)
        {
            data[i] = random.Next(); // Generates a random integer
        }
        return data;
    }
}
