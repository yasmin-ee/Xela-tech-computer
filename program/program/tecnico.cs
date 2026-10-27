using System;

namespace XelaTech
{
    public class Tecnico
    {
        // Límite de trabajos activos por técnico
        public const int LIMITE_ACTIVOS = 3;

        private string codigo;
        public string Codigo
        {
            get { return codigo; }
            set
            {
                if (value == null || value.Trim() == "")
                {
                    throw new ArgumentException("El código del técnico no puede quedar vacío.");
                }
                if (value.Length > 10)
                {
                    throw new ArgumentException("El código del técnico no puede exceder los 10 caracteres.");
                }
                codigo = value.Trim();
            }
        }

        private string nombreCompleto;
        public string NombreCompleto
        {
            get { return nombreCompleto; }
            set
            {
                if (value == null || value.Trim() == "")
                {
                    throw new ArgumentException("El nombre del técnico no puede quedar vacío.");
                }
                if (value.Length > 50)
                {
                    throw new ArgumentException("El nombre del técnico no puede exceder los 50 caracteres.");
                }
                nombreCompleto = value.Trim();
            }
        }

        private Especialidad especialidad;
        public Especialidad Especialidad
        {
            get { return especialidad; }
            set { especialidad = value; }
        }

        private bool disponible;
        public bool Disponible
        {
            get { return disponible; }
            set { disponible = value; }
        }

        private int trabajosActivos;
        public int TrabajosActivos
        {
            get { return trabajosActivos; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Los trabajos activos no pueden ser negativos.");
                }
                if (value > LIMITE_ACTIVOS)
                {
                    throw new ArgumentException("Los trabajos activos no pueden superar el límite de " + LIMITE_ACTIVOS + ".");
                }
                trabajosActivos = value;
            }
        }

        private int serviciosFinalizados;
        public int ServiciosFinalizados
        {
            get { return serviciosFinalizados; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Los servicios finalizados no pueden ser negativos.");
                }
                serviciosFinalizados = value;
            }
        }

       
        private int[] conteoCalificaciones;
        public int[] ConteoCalificaciones
        {
            get { return conteoCalificaciones; }
            set
            {
                if (value == null || value.Length != 5)
                {
                    throw new ArgumentException("El arreglo de calificaciones debe tener 5 posiciones (de 1 a 5).");
                }
                for (int i = 0; i < value.Length; i++)
                {
                    if (value[i] < 0)
                    {
                        throw new ArgumentException("El conteo de calificaciones no puede ser negativo.");
                    }
                }
                conteoCalificaciones = value;
            }
        }

        // Constructores

        // Constructor vacío
        public Tecnico()
        {
            disponible = true;
            trabajosActivos = 0;
            serviciosFinalizados = 0;
            conteoCalificaciones = new int[5];
        }

        // Sobrecarga del constructor
        public Tecnico(string codigo, string nombreCompleto, Especialidad especialidad)
        {
            Codigo = codigo;
            NombreCompleto = nombreCompleto;
            Especialidad = especialidad;
            disponible = true;
            trabajosActivos = 0;
            serviciosFinalizados = 0;
            conteoCalificaciones = new int[5];
        }

        // +TieneCupo() : bool
        public bool TieneCupo()
        {
            return trabajosActivos < LIMITE_ACTIVOS;
        }

        // +PuedeAtender(Especialidad) : bool
        public bool PuedeAtender(Especialidad requerida)
        {
            return especialidad == requerida;
        }

        // +AsignarTrabajo()
        public void AsignarTrabajo()
        {
            if (!disponible)
            {
                throw new InvalidOperationException("El técnico no está disponible.");
            }
            if (!TieneCupo())
            {
                throw new InvalidOperationException("El técnico alcanzó el límite de " + LIMITE_ACTIVOS + " trabajos activos.");
            }
            trabajosActivos = trabajosActivos + 1;
        }

        // +FinalizarTrabajo()
        public void FinalizarTrabajo()
        {
            if (trabajosActivos == 0)
            {
                throw new InvalidOperationException("El técnico no tiene trabajos activos para finalizar.");
            }
            trabajosActivos = trabajosActivos - 1;
            serviciosFinalizados = serviciosFinalizados + 1;
        }

        // +RegistrarCalificacion(int)
        public void RegistrarCalificacion(int calificacion)
        {
            if (calificacion < 1 || calificacion > 5)
            {
                throw new ArgumentException("La calificación debe estar entre 1 y 5.");
            }
            conteoCalificaciones[calificacion - 1] = conteoCalificaciones[calificacion - 1] + 1;
        }

        // CalcularPromedio
        public double CalcularPromedio()
        {
            int total = 0;
            int suma = 0;

            for (int i = 0; i < conteoCalificaciones.Length; i++)
            {
                total = total + conteoCalificaciones[i];
                suma = suma + conteoCalificaciones[i] * (i + 1);
            }

            if (total == 0)
            {
                return 0;
            }
            return (double)suma / total;
        }

        public override string ToString()
        {
            return Codigo + " | " + NombreCompleto + " | " + Especialidad +
                   " | Disponible: " + (Disponible ? "Sí" : "No") +
                   " | Activos: " + TrabajosActivos + "/" + LIMITE_ACTIVOS +
                   " | Finalizados: " + ServiciosFinalizados +
                   " | Promedio: " + CalcularPromedio().ToString("F2");
        }
    }
}