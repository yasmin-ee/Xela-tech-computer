using System;

namespace XelaTech
{
    // Los cuatro algoritmos están programados a mano (sin Sort, OrderBy ni equivalentes)
    // asc = true ordena de menor a mayor; asc = false ordena de mayor a menor
    public static class Ordenador
    {
        // +BubbleSort(Solicitud[], criterio, asc)
        // criterio: "total" (costo total) o "tiempo" (tiempo estimado)
        public static void BubbleSort(Solicitud[] arreglo, string criterio, bool asc)
        {
            if (arreglo == null || arreglo.Length < 2)
            {
                throw new InvalidOperationException("No hay datos suficientes para ordenar (se necesitan al menos 2 solicitudes).");
            }

            int n = arreglo.Length;

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - 1 - i; j++)
                {
                    decimal actual = ObtenerValor(arreglo[j], criterio);
                    decimal siguiente = ObtenerValor(arreglo[j + 1], criterio);
                    bool intercambiar = false;

                    if (asc)
                    {
                        if (actual > siguiente)
                        {
                            intercambiar = true;
                        }
                    }
                    else
                    {
                        if (actual < siguiente)
                        {
                            intercambiar = true;
                        }
                    }

                    if (intercambiar)
                    {
                        Solicitud temporal = arreglo[j];
                        arreglo[j] = arreglo[j + 1];
                        arreglo[j + 1] = temporal;
                    }
                }
            }
        }

        // +SelectionSort(Tecnico[], asc)  (criterio: servicios finalizados)
        public static void SelectionSort(Tecnico[] arreglo, bool asc)
        {
            if (arreglo == null || arreglo.Length < 2)
            {
                throw new InvalidOperationException("No hay datos suficientes para ordenar (se necesitan al menos 2 técnicos).");
            }

            int n = arreglo.Length;

            for (int i = 0; i < n - 1; i++)
            {
                int indiceElegido = i;

                for (int j = i + 1; j < n; j++)
                {
                    if (asc)
                    {
                        if (arreglo[j].ServiciosFinalizados < arreglo[indiceElegido].ServiciosFinalizados)
                        {
                            indiceElegido = j;
                        }
                    }
                    else
                    {
                        if (arreglo[j].ServiciosFinalizados > arreglo[indiceElegido].ServiciosFinalizados)
                        {
                            indiceElegido = j;
                        }
                    }
                }

                if (indiceElegido != i)
                {
                    Tecnico temporal = arreglo[i];
                    arreglo[i] = arreglo[indiceElegido];
                    arreglo[indiceElegido] = temporal;
                }
            }
        }

        // +ShellSort(Repuesto[], asc)  (criterio: existencia disponible)
        public static void ShellSort(Repuesto[] arreglo, bool asc)
        {
            if (arreglo == null || arreglo.Length < 2)
            {
                throw new InvalidOperationException("No hay datos suficientes para ordenar (se necesitan al menos 2 repuestos).");
            }

            int n = arreglo.Length;
            int salto = n / 2;

            while (salto > 0)
            {
                for (int i = salto; i < n; i++)
                {
                    Repuesto temporal = arreglo[i];
                    int j = i;
                    bool seguir = true;

                    while (j >= salto && seguir)
                    {
                        bool fueraDeOrden;

                        if (asc)
                        {
                            fueraDeOrden = arreglo[j - salto].Existencia > temporal.Existencia;
                        }
                        else
                        {
                            fueraDeOrden = arreglo[j - salto].Existencia < temporal.Existencia;
                        }

                        if (fueraDeOrden)
                        {
                            arreglo[j] = arreglo[j - salto];
                            j = j - salto;
                        }
                        else
                        {
                            seguir = false;
                        }
                    }
                    arreglo[j] = temporal;
                }
                salto = salto / 2;
            }
        }

        // +QuickSort(Solicitud[], ini, fin, asc)  (criterio: fecha de recepción)
        // Se llama con ini = 0 y fin = arreglo.Length - 1
        public static void QuickSort(Solicitud[] arreglo, int ini, int fin, bool asc)
        {
            if (arreglo == null || arreglo.Length < 2)
            {
                throw new InvalidOperationException("No hay datos suficientes para ordenar (se necesitan al menos 2 solicitudes).");
            }
            if (ini < 0 || fin >= arreglo.Length)
            {
                throw new ArgumentException("Los límites del ordenamiento están fuera del arreglo.");
            }

            if (ini < fin)
            {
                int posicionPivote = Particionar(arreglo, ini, fin, asc);
                QuickSort(arreglo, ini, posicionPivote - 1, asc);
                QuickSort(arreglo, posicionPivote + 1, fin, asc);
            }
        }

        // Auxiliar privado (no está en el diagrama): parte el arreglo usando el último elemento como pivote
        private static int Particionar(Solicitud[] arreglo, int ini, int fin, bool asc)
        {
            DateTime pivote = arreglo[fin].FechaRecepcion;
            int i = ini - 1;

            for (int j = ini; j < fin; j++)
            {
                bool vaAntesDelPivote;

                if (asc)
                {
                    vaAntesDelPivote = arreglo[j].FechaRecepcion <= pivote;
                }
                else
                {
                    vaAntesDelPivote = arreglo[j].FechaRecepcion >= pivote;
                }

                if (vaAntesDelPivote)
                {
                    i = i + 1;
                    Solicitud temporal = arreglo[i];
                    arreglo[i] = arreglo[j];
                    arreglo[j] = temporal;
                }
            }

            Solicitud auxiliar = arreglo[i + 1];
            arreglo[i + 1] = arreglo[fin];
            arreglo[fin] = auxiliar;
            return i + 1;
        }

        // Auxiliar privado (no está en el diagrama): valor de la solicitud según el criterio de BubbleSort
        private static decimal ObtenerValor(Solicitud solicitud, string criterio)
        {
            if (criterio == null)
            {
                throw new ArgumentException("El criterio de ordenamiento no puede ser nulo.");
            }

            string clave = criterio.Trim().ToLower();

            if (clave == "total")
            {
                return solicitud.Total;
            }
            if (clave == "tiempo")
            {
                return solicitud.TiempoEstimado;
            }
            throw new ArgumentException("Criterio no válido. Use \"total\" o \"tiempo\".");
        }
    }
}
