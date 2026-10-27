using System;

namespace XelaTech
{
    public class MatrizCarga
    {
        private int[,] carga = new int[0, 5];
        public int[,] Carga
        {
            get { return carga; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("La matriz de carga no puede ser nula.");
                }
                if (value.GetLength(1) != dias.Length)
                {
                    throw new ArgumentException("La matriz debe tener una columna por cada día de atención.");
                }
                carga = value;
            }
        }

        private string[] dias = new string[] { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes" };
        public string[] Dias
        {
            get { return dias; }
            set
            {
                if (value == null || value.Length != carga.GetLength(1))
                {
                    throw new ArgumentException("Debe haber un nombre por cada columna de la matriz.");
                }
                dias = value;
            }
        }

        public void Registrar(int indiceTecnico, int dia)
        {
            if (indiceTecnico < 0)
            {
                throw new ArgumentException("El índice del técnico no puede ser negativo.");
            }
            if (dia < 0 || dia >= dias.Length)
            {
                throw new ArgumentException("El día debe estar entre 0 y " + (dias.Length - 1) + ".");
            }

            if (indiceTecnico >= carga.GetLength(0))
            {
                AgrandarFilas(indiceTecnico + 1);
            }
            carga[indiceTecnico, dia] = carga[indiceTecnico, dia] + 1;
        }

        public void Mostrar()
        {
            Console.WriteLine("=== MATRIZ SEMANAL DE CARGA DE TRABAJO ===");

            bool hayCarga = false;
            for (int i = 0; i < carga.GetLength(0); i++)
            {
                for (int j = 0; j < carga.GetLength(1); j++)
                {
                    if (carga[i, j] > 0)
                    {
                        hayCarga = true;
                    }
                }
            }
            if (!hayCarga)
            {
                Console.WriteLine("No hay carga registrada.");
                return;
            }

            Console.Write("Técnico".PadRight(10));
            for (int j = 0; j < dias.Length; j++)
            {
                Console.Write(dias[j].PadRight(11));
            }
            Console.WriteLine("Total");

            for (int i = 0; i < carga.GetLength(0); i++)
            {
                int totalFila = 0;
                Console.Write(EtiquetaTecnico(i).PadRight(10));
                for (int j = 0; j < carga.GetLength(1); j++)
                {
                    Console.Write(carga[i, j].ToString().PadRight(11));
                    totalFila = totalFila + carga[i, j];
                }
                Console.WriteLine(totalFila);
            }

            int tecnico = TecnicoMayorCarga();
            int dia = DiaMayorCarga();
            Console.WriteLine("Técnico con mayor carga: " + EtiquetaTecnico(tecnico) + " (" + TotalTecnico(tecnico) + " solicitudes)");
            Console.WriteLine("Día con mayor carga: " + dias[dia] + " (" + TotalDia(dia) + " solicitudes)");
        }

        public int TecnicoMayorCarga()
        {
            int mejor = -1;
            int mayorTotal = 0;

            for (int i = 0; i < carga.GetLength(0); i++)
            {
                int total = TotalTecnico(i);
                if (total > mayorTotal)
                {
                    mayorTotal = total;
                    mejor = i;
                }
            }

            if (mejor == -1)
            {
                throw new InvalidOperationException("No hay carga registrada.");
            }
            return mejor;
        }

        public int DiaMayorCarga()
        {
            int mejor = -1;
            int mayorTotal = 0;

            for (int j = 0; j < carga.GetLength(1); j++)
            {
                int total = TotalDia(j);
                if (total > mayorTotal)
                {
                    mayorTotal = total;
                    mejor = j;
                }
            }

            if (mejor == -1)
            {
                throw new InvalidOperationException("No hay carga registrada.");
            }
            return mejor;
        }


        private int TotalTecnico(int fila)
        {
            int total = 0;
            for (int j = 0; j < carga.GetLength(1); j++)
            {
                total = total + carga[fila, j];
            }
            return total;
        }

        private int TotalDia(int columna)
        {
            int total = 0;
            for (int i = 0; i < carga.GetLength(0); i++)
            {
                total = total + carga[i, columna];
            }
            return total;
        }

        private string EtiquetaTecnico(int fila)
        {
            return "T" + (fila + 1).ToString("000");
        }

        private void AgrandarFilas(int filasNuevas)
        {
            int[,] ampliada = new int[filasNuevas, carga.GetLength(1)];

            for (int i = 0; i < carga.GetLength(0); i++)
            {
                for (int j = 0; j < carga.GetLength(1); j++)
                {
                    ampliada[i, j] = carga[i, j];
                }
            }
            carga = ampliada;
        }
    }
}
