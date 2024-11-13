//Problema de los filosofos comensales
using System;
using System.Collections.Generic;
using System.Threading;

namespace FilosofosComensales
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese el número de filósofos (máximo 10): ");
            if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0 || n > 10)
            {
                Console.WriteLine("Número inválido. El programa finalizará.");
                return;
            }

            // Crear tenedores y filósofos
            Tenedor[] tenedores = new Tenedor[n];
            Filosofo[] filosofos = new Filosofo[n];
            for (int i = 0; i < n; i++)
            {
                tenedores[i] = new Tenedor();
            }
            for (int i = 0; i < n; i++)
            {
                filosofos[i] = new Filosofo(i + 1, $"Filósofo {i + 1}", new Random().Next(1, 11), tenedores[i], tenedores[(i + 1) % n]);
            }

            // Crear y ejecutar los hilos
            List<Thread> hilos = new List<Thread>();
            foreach (var filosofo in filosofos)
            {
                Thread hilo = new Thread(filosofo.Comer);
                hilos.Add(hilo);
                hilo.Start();
            }

            // Esperar a que todos los hilos terminen
            foreach (var hilo in hilos)
            {
                hilo.Join();
            }

            Console.WriteLine("Todos los filósofos han terminado de comer.");
        }
    }

    class Tenedor
    {
        private SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
        public bool Disponible { get; private set; } = true;

        public bool Tomar()
        {
            if (semaphore.Wait(0))
            {
                Disponible = false; // Cambia el estado a no disponible
                return true;
            }
            return false;
        }

        public void Liberar()
        {
            Disponible = true; // Cambia el estado a disponible
            semaphore.Release();
        }
    }

    class Filosofo
    {
        public int Numero { get; }
        public string Nombre { get; }
        public int Comida { get; private set; }
        private Tenedor Izquierdo { get; }
        private Tenedor Derecho { get; }

        public Filosofo(int numero, string nombre, int comida, Tenedor izquierdo, Tenedor derecho)
        {
            Numero = numero;
            Nombre = nombre;
            Comida = comida;
            Izquierdo = izquierdo;
            Derecho = derecho;
        }

        public void Comer()
        {
            Console.WriteLine($"{Nombre} está pensando.");
            while (Comida > 0)
            {
                if (Izquierdo.Disponible)
                {
                    if (Derecho.Disponible)
                    {
                        Console.WriteLine($"{Nombre} va a comer.");
                        while (Comida > 0)
                        {
                            Thread.Sleep(4000);
                            Comida--;
                            Console.WriteLine($"{Nombre} comió un bocado. Comida restante: {Comida}");
                        }
                        Derecho.Liberar();
                        Izquierdo.Liberar();
                        Console.WriteLine($"{Nombre} terminó su plato.");
                    }
                    else
                    {
                        Console.WriteLine($"{Nombre} no puede comer porque el tenedor derecho no está disponible.");
                        Izquierdo.Liberar();
                    }
                }
                else
                {
                    Console.WriteLine($"{Nombre} no puede comer porque el tenedor izquierdo no está disponible.");
                }

                Thread.Sleep(new Random().Next(100, 500)); // Tiempo aleatorio de espera para evitar inanición
            }
        }
    }
}




