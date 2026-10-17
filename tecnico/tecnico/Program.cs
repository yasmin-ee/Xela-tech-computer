using System.Runtime.CompilerServices;
//falta poner las validadciones de los datos que se ingresan
namespace XelaTech
{
    class tecnico
    {
        private string codigoTecnico;

        public string CodigoTecnico
        {
            get { return codigoTecnico; }
            set { codigoTecnico = value; }
        }
        private string nombreTecnico;

        public string NombreTecnico
        {
            get { return nombreTecnico; }
            set { nombreTecnico = value; }
        }

        //falta mejorar la especialidad del tecnico
        private string especialidad;
        public string Especialidad
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
            set { trabajosActivos = value; }
        }

        private int trabajosFinalizados;

        public int TrabajosFinalizados
        {
            get { return trabajosFinalizados; }
            set { trabajosFinalizados = value; }
        }

        private int[] conteoCalificaciones;

        public int[] ConteoCalificaciones
        {
            get { return conteoCalificaciones; }
            set { conteoCalificaciones = value; }
        }

        public void TieneCupo ()
        {
            if (trabajosActivos < 3)
            {
                disponible = true;
                Console.WriteLine("El técnico tiene cupo para más trabajos.");
            }
            else
            {
                disponible = false;
                Console.WriteLine("El técnico no tiene cupo para más trabajos");
            }
        }

        public void PuedeAtender()
        {
            if (disponible)
            {
                Console.WriteLine("El técnico puede atender más trabajos.");
            }
            else
            {
                Console.WriteLine("El técnico no puede atender más trabajos.");
            }
        }

        public void FinalizarTrabajo()
        {
            if (trabajosActivos > 0)
            {
                trabajosActivos--;
                trabajosFinalizados++;
                Console.WriteLine("El técnico ha finalizado un trabajo.");
            }
            else
            {
                Console.WriteLine("El técnico no tiene trabajos activos para finalizar.");
            }
        }


        public void registrarCalificaciones()
        {
            int totalCalificaciones = 0;
            int sumaCalificaciones = 0;
            for (int i = 0; i < conteoCalificaciones.Length; i++)
            {
                totalCalificaciones += conteoCalificaciones[i];
                sumaCalificaciones += conteoCalificaciones[i] * (i + 1);
            }
            if (totalCalificaciones > 0)
            {
                double calificacionPromedio = (double)sumaCalificaciones / totalCalificaciones;
                Console.WriteLine($"La calificación promedio del técnico es: {calificacionPromedio:F2}");
            }
            else
            {
                Console.WriteLine("El técnico no tiene calificaciones registradas.");
            }
        }

    }
}