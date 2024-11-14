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
            List<string>[] logs = new List<string>[n]; // Array de logs

            for (int i = 0; i < n; i++)
            {
                tenedores[i] = new Tenedor();
                logs[i] = new List<string>(); // Inicializar la lista de logs para cada filósofo
            }
            for (int i = 0; i < n; i++)
            {
                int cantComida = new Random().Next(1, 11);
                filosofos[i] = new Filosofo(i + 1, $"Filósofo {i + 1}", cantComida, tenedores[i], tenedores[(i + 1) % n], logs[i]);

                logs[i].Add($"El hilo del filósofo {i + 1} se acaba de crear");
                Console.WriteLine($"Filósofo {i + 1} tiene {cantComida} de comida en su plato.");
                logs[i].Add($"Filósofo {i + 1} tiene {cantComida} de comida en su plato.");
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

            // Imprimir los logs de cada filósofo
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\nTraza de {filosofos[i].Nombre}:");
                foreach (var log in logs[i])
                {
                    Console.WriteLine(log);
                }
            }
        }
    }

    class Tenedor
    {
        private readonly Semaphore semaphore = new Semaphore(1, 1);
        private bool disponible = true;

        public void Tomar()
        {
            semaphore.WaitOne();
            disponible = false; // Marca el tenedor como no disponible
        }

        public void Liberar()
        {
            disponible = true; // Marca el tenedor como disponible
            semaphore.Release(); // Libera el tenedor para que otro filósofo pueda tomarlo
        }

        public bool EstaDisponible()
        {
            return disponible;
        }
    }

    class Filosofo
    {
        public int Numero { get; }
        public string Nombre { get; }
        public int Comida { get; private set; }
        private Tenedor Izquierdo { get; }
        private Tenedor Derecho { get; }
        private List<string> Logs { get; } // Lista de logs específica para este filósofo

        public Filosofo(int numero, string nombre, int comida, Tenedor izquierdo, Tenedor derecho, List<string> logs)
        {
            Numero = numero;
            Nombre = nombre;
            Comida = comida;
            Izquierdo = izquierdo;
            Derecho = derecho;
            Logs = logs;
        }

        public void Comer()
        {
            Log($"{Nombre} está pensando.");
            while (Comida > 0)
            {
                // Intentar tomar el tenedor izquierdo
                Izquierdo.Tomar();
                Log($"{Nombre} ha tomado su tenedor izquierdo.");

                // Verificar si el tenedor derecho está disponible antes de tomarlo
                if (!Derecho.EstaDisponible())
                {
                    Log($"{Nombre} está esperando a que el tenedor derecho esté disponible.");
                    Izquierdo.Liberar(); // Libera el tenedor izquierdo para evitar interbloqueo
                    Log($"{Nombre} ha liberado su tenedor izquierdo para evitar interbloqueo.");
                    Thread.Sleep(4000); // Espera aleatoria antes de reintentar
                    continue;
                }

                // Intentar tomar el tenedor derecho
                Derecho.Tomar();
                Log($"{Nombre} ha tomado ambos tenedores y va a comer.");

                // Comer mientras tenga comida
                while (Comida > 0)
                {
                    Thread.Sleep(1); // Simula el tiempo de comer
                    Comida--;
                    Log($"{Nombre} comió un bocado. Comida restante: {Comida}");
                }

                // Liberar los tenedores
                Derecho.Liberar();
                Log($"{Nombre} ha liberado el tenedor derecho.");

                Izquierdo.Liberar();
                Log($"{Nombre} ha liberado el tenedor izquierdo.");

                Log($"{Nombre} terminó su plato");
                Thread.Sleep(4000); // Espera aleatoria para evitar inanición
            }
            Log($"{Nombre} ha terminado de comer.");
        }

        // Método para agregar un mensaje a la lista de logs
        private void Log(string mensaje)
        {
            Logs.Add(mensaje);
            Console.WriteLine(mensaje); // Imprimir en la consola al mismo tiempo
        }
    }
}





