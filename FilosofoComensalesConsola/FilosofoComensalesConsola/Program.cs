//Problema de los filosofos comensales
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;

namespace FilosofosComensales
{
    class Program
    {
        static void Main(string[] args)
        {
            bool esNoValido = true;
            int n = 0;
            List<string> nombresFilosofos = new List<string> { "Aristoteles", "Platón", "Sócrates", "Descartes", "Kant", "Nietzsche", "Hume", "Locke", "Wittgenstein", "Russell" };
            while (esNoValido) {
                Console.WriteLine("Ingrese el número de filósofos (máximo 10): ");
                int.TryParse(Console.ReadLine(), out n);
                if (n <= 0 || n > 10)
                {
                    Console.Clear();
                    Console.WriteLine("Número inválido. Intente de nuevo");
                    continue;
                }
                esNoValido = false;
            }


            // Crear tenedores y filósofos
            Tenedor[] tenedores = new Tenedor[n==1?n+1:n];
            Filosofo[] filosofos = new Filosofo[n];
            List<string>[] logs = new List<string>[n]; // Array de logs

            

            if (n == 1)
            {
                tenedores[0] = new Tenedor();
                tenedores[1] = new Tenedor();
                int cantComida = new Random().Next(1, 11);
                logs[0] = new List<string>(); // Inicializar la lista de logs para cada filósofo
                filosofos[0] = new Filosofo(1, nombresFilosofos[0], cantComida, tenedores[0], tenedores[1], logs[0]);

                Console.WriteLine($"El hilo de {filosofos[0].Nombre} se acaba de crear");
                logs[0].Add($"El hilo de {filosofos[0].Nombre} se acaba de crear");
                Console.WriteLine($"{filosofos[0].Nombre} tiene {cantComida} de comida en su plato.");
                logs[0].Add($"{filosofos[0].Nombre} tiene {cantComida} de comida en su plato.");
            }
            else {
                for (int i = 0; i < n; i++)
                {
                    tenedores[i] = new Tenedor();
                }
                for (int i = 0; i < n; i++)
                {
                    int cantComida = new Random().Next(1, 11);
                    logs[i] = new List<string>(); // Inicializar la lista de logs para cada filósofo
                    filosofos[i] = new Filosofo(i + 1, nombresFilosofos[i], cantComida, tenedores[i], tenedores[(i + 1) % n], logs[i]);

                    Console.WriteLine($"El hilo de {filosofos[i].Nombre} se acaba de crear");
                    logs[i].Add($"El hilo de {filosofos[i].Nombre} se acaba de crear");
                    Console.WriteLine($"{filosofos[i].Nombre} tiene {cantComida} de comida en su plato.");
                    logs[i].Add($"{filosofos[i].Nombre} tiene {cantComida} de comida en su plato.");
                }
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

            Console.WriteLine("Presiona cualquier tecla para cerrar el programa");
            Console.ReadKey();
        }
    }

    class Tenedor
    {
        private readonly Semaphore semaphore = new Semaphore(1, 1);
        //0 = disponible, 1 = no disponible
        private int disponible = 0;

        public void Tomar()
        {

            semaphore.WaitOne();
            disponible = 1; // Marca el tenedor como no disponible            

        }

        public void Liberar()
        {
            disponible = 0; // Marca el tenedor como disponible
            semaphore.Release(); // Libera el tenedor para que otro filósofo pueda tomarlo
        }

        public bool EstaDisponible()
        {
            return disponible == 0;
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
        public void Pensar() {
            Log($"{Nombre} está pensando.");
        }
        public void Comer()
        {
            Pensar();
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
                    Log($"{Nombre} no puede comer.");
                    Log($"{Nombre} ha liberado su tenedor izquierdo para evitar interbloqueo.");
                    Thread.Sleep(1); // Espera antes de reintentar
                    Pensar();
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
                
            }
            Log($"{Nombre} ha terminado de comer.");
            Pensar();
        }

        // Método para agregar un mensaje a la lista de logs
        private void Log(string mensaje)
        {
            Logs.Add(mensaje);
            Console.WriteLine(mensaje); // Imprimir en la consola al mismo tiempo
        }
    }
}





