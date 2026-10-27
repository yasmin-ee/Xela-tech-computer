using System;
using System.Collections.Generic;

namespace XelaTech
{
    public class Reportes
    {
        private GestorSolicitudes gestorSolicitudes;
        public GestorSolicitudes GestorSolicitudes
        {
            get { return gestorSolicitudes; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("El gestor de solicitudes no puede ser nulo.");
                }
                gestorSolicitudes = value;
            }
        }

        private GestorTecnicos gestorTecnicos;
        public GestorTecnicos GestorTecnicos
        {
            get { return gestorTecnicos; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("El gestor de técnicos no puede ser nulo.");
                }
                gestorTecnicos = value;
            }
        }

        private GestorRepuestos gestorRepuestos;
        public GestorRepuestos GestorRepuestos
        {
            get { return gestorRepuestos; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("El gestor de repuestos no puede ser nulo.");
                }
                gestorRepuestos = value;
            }
        }

        public Reportes(GestorSolicitudes gestorSolicitudes, GestorTecnicos gestorTecnicos, GestorRepuestos gestorRepuestos)
        {
            GestorSolicitudes = gestorSolicitudes;
            GestorTecnicos = gestorTecnicos;
            GestorRepuestos = gestorRepuestos;
        }
}
        public void SolicitudesEnEspera()
        {
            Console.WriteLine("=== SOLICITUDES EN ESPERA ===");

            Console.WriteLine("--- Urgentes (" + gestorSolicitudes.ColaUrgente.Count + ") ---");
            if (gestorSolicitudes.ColaUrgente.Count == 0)
            {
                Console.WriteLine("No hay solicitudes urgentes en espera.");
            }
            else
            {
                foreach (Solicitud s in gestorSolicitudes.ColaUrgente)
                {
                    Console.WriteLine(s);
                }
            }

            Console.WriteLine("--- Normales (" + gestorSolicitudes.ColaNormal.Count + ") ---");
            if (gestorSolicitudes.ColaNormal.Count == 0)
            {
                Console.WriteLine("No hay solicitudes normales en espera.");
            }
            else
            {
                foreach (Solicitud s in gestorSolicitudes.ColaNormal)
                {
                    Console.WriteLine(s);
                }
            }
        }

        public void SolicitudesPorEstado()
        {
            List<Solicitud> todas = gestorSolicitudes.Solicitudes;

            Console.WriteLine("=== SOLICITUDES POR ESTADO ===");
            if (todas.Count == 0)
            {
                Console.WriteLine("No hay solicitudes registradas.");
                return;
            }

            List<Solicitud> activas = new List<Solicitud>();
            for (int i = 0; i < todas.Count; i++)
            {
                if (todas[i].Estado != EstadoSolicitud.Entregada && todas[i].Estado != EstadoSolicitud.Cancelada)
                {
                    activas.Add(todas[i]);
                }
            }

            List<Solicitud> finalizadas = Buscador.BusquedaSecuencial(todas, EstadoSolicitud.Entregada.ToString());
            List<Solicitud> canceladas = Buscador.BusquedaSecuencial(todas, EstadoSolicitud.Cancelada.ToString());

            MostrarSolicitudes("Activas", activas);
            MostrarSolicitudes("Finalizadas", finalizadas);
            MostrarSolicitudes("Canceladas", canceladas);
        }

        public void TecnicosDisponibles()
        {
            List<Tecnico> tecnicos = gestorTecnicos.Tecnicos;

            Console.WriteLine("=== TÉCNICOS DISPONIBLES Y CON CARGA COMPLETA ===");
            if (tecnicos.Count == 0)
            {
                Console.WriteLine("No hay técnicos registrados.");
                return;
            }

            Console.WriteLine("--- Disponibles (con cupo) ---");
            int disponibles = 0;
            for (int i = 0; i < tecnicos.Count; i++)
            {
                if (tecnicos[i].Disponible && tecnicos[i].TieneCupo())
                {
                    Console.WriteLine(tecnicos[i]);
                    disponibles++;
                }
            }
            if (disponibles == 0)
            {
                Console.WriteLine("No hay técnicos disponibles.");
            }

            Console.WriteLine("--- Con carga completa ---");
            int completos = 0;
            for (int i = 0; i < tecnicos.Count; i++)
            {
                if (!tecnicos[i].TieneCupo())
                {
                    Console.WriteLine(tecnicos[i]);
                    completos++;
                }
            }
            if (completos == 0)
            {
                Console.WriteLine("No hay técnicos con carga completa.");
            }
        }

        public void TecnicoMasServicios()
        {
            List<Tecnico> tecnicos = gestorTecnicos.Tecnicos;

            Console.WriteLine("=== TÉCNICO CON MÁS SERVICIOS FINALIZADOS ===");
            if (tecnicos.Count == 0)
            {
                Console.WriteLine("No hay técnicos registrados.");
                return;
            }

            Tecnico[] arreglo = new Tecnico[tecnicos.Count];
            for (int i = 0; i < tecnicos.Count; i++)
            {
                arreglo[i] = tecnicos[i];
            }

            if (arreglo.Length > 1)
            {
                Ordenador.SelectionSort(arreglo, false);
                Console.WriteLine("Algoritmo: Selection Sort | Criterio: servicios finalizados | Orden: descendente");
            }

            for (int i = 0; i < arreglo.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + arreglo[i]);
            }

            if (arreglo[0].ServiciosFinalizados == 0)
            {
                Console.WriteLine("Ningún técnico ha finalizado servicios todavía.");
            }
            else
            {
                Console.WriteLine("Técnico con más servicios: " + arreglo[0].NombreCompleto +
                                  " (" + arreglo[0].ServiciosFinalizados + ")");
            }
        }

        public void EquiposPorTipo()
        {
            List<Solicitud> todas = gestorSolicitudes.Solicitudes;

            int[] conteo = new int[3];

            for (int i = 0; i < todas.Count; i++)
            {
                if (todas[i].Estado == EstadoSolicitud.Entregada)
                {
                    Especialidad tipo = todas[i].Equipo.ObtenerEspecialidad();

                    if (tipo == Especialidad.Portatiles)
                    {
                        conteo[0] = conteo[0] + 1;
                    }
                    else if (tipo == Especialidad.Escritorio)
                    {
                        conteo[1] = conteo[1] + 1;
                    }
                    else if (tipo == Especialidad.Impresoras)
                    {
                        conteo[2] = conteo[2] + 1;
                    }
                }
            }

            Console.WriteLine("=== EQUIPOS ATENDIDOS POR TIPO ===");
            Console.WriteLine("Portátiles: " + conteo[0]);
            Console.WriteLine("Computadoras de escritorio: " + conteo[1]);
            Console.WriteLine("Impresoras: " + conteo[2]);
            Console.WriteLine("Total atendidos: " + (conteo[0] + conteo[1] + conteo[2]));
        }

        public void RepuestosBajoMinimo()
        {
            List<Repuesto> bajos = gestorRepuestos.ObtenerBajoMinimo();

            Console.WriteLine("=== REPUESTOS CON EXISTENCIA MÍNIMA O AGOTADOS ===");
            if (bajos.Count == 0)
            {
                Console.WriteLine("Ningún repuesto está en existencia mínima ni agotado.");
                return;
            }

            Repuesto[] arreglo = new Repuesto[bajos.Count];
            for (int i = 0; i < bajos.Count; i++)
            {
                arreglo[i] = bajos[i];
            }

            if (arreglo.Length > 1)
            {
                Ordenador.ShellSort(arreglo, true);
                Console.WriteLine("Algoritmo: Shell Sort | Criterio: existencia disponible | Orden: ascendente");
            }

            for (int i = 0; i < arreglo.Length; i++)
            {
                string estado = "";
                if (arreglo[i].Existencia == 0)
                {
                    estado = " | AGOTADO";
                }
                Console.WriteLine(arreglo[i] + estado);
            }
        }

        public void IngresosTotales()
        {
            List<Solicitud> todas = gestorSolicitudes.Solicitudes;
            decimal ingresos = 0m;
            int finalizados = 0;

            for (int i = 0; i < todas.Count; i++)
            {
                if (todas[i].Estado == EstadoSolicitud.Entregada)
                {
                    ingresos = ingresos + todas[i].Total;
                    finalizados++;
                }
            }

            Console.WriteLine("=== INGRESOS TOTALES POR SERVICIOS FINALIZADOS ===");
            Console.WriteLine("Servicios finalizados: " + finalizados);
            Console.WriteLine("Ingresos totales: Q" + ingresos.ToString("F2"));
        }

        private void MostrarSolicitudes(string titulo, List<Solicitud> lista)
        {
            Console.WriteLine("--- " + titulo + " (" + lista.Count + ") ---");
            if (lista.Count == 0)
            {
                Console.WriteLine("Sin solicitudes.");
            }
            else
            {
                for (int i = 0; i < lista.Count; i++)
                {
                    Console.WriteLine(lista[i]);
                }
            }
        }
    }
}