using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
Console.WriteLine(" ");
Console.WriteLine(args[0]);
Console.WriteLine(args[1]);
Random rnd = new Random();
Stopwatch sw = new Stopwatch();
int[] tal = new int[Convert.ToInt32(args[0])];
string type = args[1];
for(int i = 0; i < tal.Length; i++)
{
    tal[i] = rnd.Next(0, tal.Length * 100);
}
Do(type);


void Do(string type)
{
    if(type == "bubble")
    {
        sw.Start();
        BubbleSort(tal);
        sw.Stop();
        Output(tal, sw);
    }
    else if(type == "insertion")
    {
        sw.Start();
        InsertionSort(tal);
        sw.Stop();
        Output(tal, sw);
    }
    else if(type == "selection")
    {
        sw.Start();
        SelectionSort(tal);
        sw.Stop();
        Output(tal, sw);
    }
    else if(type == "topdownmerge")
    {
        sw.Start();
        TopDownMergeSort(tal);
        sw.Stop();
        Output(tal, sw);
    }
    else if(type == "bottomupmerge")
    {
        sw.Start();
        BottomUpMergeSort(tal);
        sw.Stop();
        Output(tal, sw);
    }
    else if(type == "quick")
    {
        sw.Start();
        QuickSort(tal, 0, tal.Length-1);
        sw.Stop();
        Output(tal, sw);
    }
    else
    {
        Console.WriteLine("Välj mellan: bubble, insertion, selection, topdownmerge, bottomupmerge, quick");
    }
}

void BubbleSort(int[] arr)
{
    int n = arr.Length;
    for(int i = 0; i < n; i++)
    {
        for(int j = 0; j < arr.Length-1-i; j++)
        {
            if(arr[j] > arr[j+1])
                Swap(ref arr[j], ref arr[j+1]);
        }
    } 
}

void InsertionSort(int[] arr)
{
    int n = arr.Length;
    for(int i = 1; i < n; i++)
    {
        int key = arr[i];
        int j = i - 1;
        while(j >= 0 && arr[j] > key)
        {
            arr[j + 1] = arr[j];
            j = j - 1;
        }
        arr[j + 1] = key;
    }
}

void SelectionSort(int[] arr)
{
    int n = arr.Length;
    for(int i = 0; i < n - 1; i++)
    {
        int min_idx = i;
        for(int j = i + 1; j < n; j++)
        {
            if(arr[j] < arr[min_idx])
            {
                min_idx = j;
            }
        }
        Swap(ref arr[i], ref arr[min_idx]);
    }
}

void TopDownMergeSort(int[] A)
{
    int n = A.GetLength(0);
    int[] B = new int[n];
    CopyArray(A, B, n);
    TopDownSplitMerge(A, 0, n, B);

    void TopDownSplitMerge(int[] B, int iBegin, int iEnd, int[] A)
    {
        if(iEnd - iBegin <= 1)
        {
            return;
        }
        int iMiddle = (iEnd + iBegin) / 2;
        TopDownSplitMerge(A, iBegin, iMiddle, B);
        TopDownSplitMerge(A, iMiddle, iEnd, B);
        TopDownMerge(B, iBegin, iMiddle, iEnd, A);
    }
    
    void TopDownMerge(int[] B, int iBegin, int iMiddle, int iEnd, int[] A)
    {
        int i = iBegin;
        int j = iMiddle;
        for(int k = iBegin; k < iEnd; k++)
        {
            if(i < iMiddle && (j >= iEnd || A[i] <= A[j]))
            {
                B[k] = A[i];
                i++;
            }
            else
            {
                B[k] = A[j];
                j++;
            }
        }
    }
}

void BottomUpMergeSort(int[] A)
{
    int n = A.GetLength(0);
    int[] B = new int[n];
    for(int width = 1; width < n; width *= 2)
    {
        for(int i = 0; i < n; i = i + 2 * width)
        {
            BottomUpMerge(A, i, Math.Min(i + width, n), Math.Min(i + 2 * width, n), B);
        }
        CopyArray(B, A, n);
    }
    void BottomUpMerge(int[] A, int iLeft, int iRight, int iEnd, int[] B)
    {
        int i = iLeft;
        int j = iRight;
        for(int key = iLeft; key < iEnd; key++)
        {
            if(i < iRight && (j >= iEnd || A[i] <= A[j]))
            {
                B[key] = A[i];
                i++;
            }
            else
            {
                B[key] = A[j];
                j++;
            }
        }
    }
}

void QuickSort(int[] A, int lo, int hi)
{
    if (lo >= hi || lo < 0)
    {
        return;
    }
    int p = Partition(A, lo, hi);
    QuickSort(A, lo, p - 1);
    QuickSort(A, p + 1, hi);

    int Partition(int[] A, int lo, int hi)
    {
        int pivot = A[hi];
        int i = lo;
        for(int j = lo; j <  hi; j++)
        {
            if(A[j] <= pivot)
            {
                Swap(ref A[i], ref A[j]);
                i++;
            }
        }
        Swap(ref A[i], ref A[hi]);
        return i;
    }
}


void Output(int[] tal, Stopwatch sw)
{
    Console.WriteLine(" ");
    if(Convert.ToInt32(args[0]) <= 10)
    {
        foreach(int i in tal)
        {
            Console.WriteLine(i);
        }
        Console.WriteLine(" ");
    }
    TimeSpan elapsed = sw.Elapsed;
    Console.WriteLine($"Sorting time: {elapsed}");
    Console.WriteLine(" "); 
}


void CopyArray(int[] A, int[] B, int n)
{
    for(int k = 0; k < n; k++)
    {
        B[k] = A[k];
    }
}
void Swap(ref int a, ref int b)
{
    int temp = a;
    a = b;
    b = temp;
}
