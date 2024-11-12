//Problema de los filosofos comensales
using System;
using System.Collections.Specialized;
using System.Threading;
class Program
{
    private static Semaphore _semaphore;

    static void Main(string[] args)
    {

        Console.BackgroundColor = ConsoleColor.Black;
        Console.WriteLine("Problema de los filosofos comensales");
        Console.WriteLine("===================================");
        Console.Write("Introduce el numero de filosofos: ");
        int numFilosofos = int.Parse(Console.ReadLine());

        Thread[] threads = new Thread[numFilosofos];
        _semaphore = new Semaphore(0, 4);

        for (int i = 0; i < numFilosofos; i++)
        {
            threads[i] = new Thread(Filosofo);
            threads[i].Start(i);
        }

        _semaphore.Release(4);
        foreach (Thread thread in threads)
        {
            thread.Join();
        }
        Console.WriteLine("Esto se ejecuta??");
    }

    static void Filosofo(object id)
    {
        while (true)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            int threadId = (int)id;
            Console.WriteLine($"El filosofo {threadId} esta pensando.");
            _semaphore.WaitOne();
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"El filosofo {threadId} esta comiendo.");
            Thread.Sleep(1000);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine($"El filosofo {threadId} ha terminado de comer.");
            Console.BackgroundColor = ConsoleColor.Black;
            _semaphore.Release(1);

        }
        
    }
    
}

 

