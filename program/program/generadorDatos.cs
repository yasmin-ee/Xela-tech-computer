using System;

namespace XelaTech
{
    public class GeneradorDatos
    {
        private GestorClientes gestorClientes;
        private GestorTecnicos gestorTecnicos;
        private GestorEquipos gestorEquipos;
        private GestorRepuestos gestorRepuestos;
        private GestorSolicitudes gestorSolicitudes;
        private MatrizCarga matrizCarga;
        private Random azar = new Random();

        public GeneradorDatos(GestorClientes gestorClientes, GestorTecnicos gestorTecnicos,
                              GestorEquipos gestorEquipos, GestorRepuestos gestorRepuestos,
                              GestorSolicitudes gestorSolicitudes, MatrizCarga matrizCarga)
        {
            this.gestorClientes = gestorClientes;
            this.gestorTecnicos = gestorTecnicos;
            this.gestorEquipos = gestorEquipos;
            this.gestorRepuestos = gestorRepuestos;
            this.gestorSolicitudes = gestorSolicitudes;
            this.matrizCarga = matrizCarga;
        }

        public void Generar(int cantidad)
        {
            if (cantidad < 1 || cantidad > 100)
            {
                throw new ArgumentException("La cantidad debe estar entre 1 y 100.");
            }

            // Técnicos y repuestos base (solo si no existen)
            if (gestorTecnicos.Tecnicos.Count == 0)
            {
                gestorTecnicos.Registrar("Carlos Pérez", Especialidad.Portatiles);
                gestorTecnicos.Registrar("Ana López", Especialidad.Escritorio);
                gestorTecnicos.Registrar("Luis Morales", Especialidad.Impresoras);
                gestorTecnicos.Registrar("María Gómez", Especialidad.General);
            }
            if (gestorRepuestos.Repuestos.Count == 0)
            {
                gestorRepuestos.Registrar(new Repuesto("Disco SSD 480GB", "Almacenamiento", 20, 5, 250m, 350m));
                gestorRepuestos.Registrar(new Repuesto("Memoria RAM 8GB", "Memoria", 25, 5, 180m, 260m));
                gestorRepuestos.Registrar(new Repuesto("Fuente de poder", "Energía", 15, 3, 200m, 300m));
                gestorRepuestos.Registrar(new Repuesto("Cartucho de tinta", "Impresión", 30, 8, 90m, 140m));
                gestorRepuestos.Registrar(new Repuesto("Pantalla 15.6", "Pantallas", 10, 2, 600m, 850m));
            }

            string[] nombres = { "Juan Ramírez", "Sofía Castillo", "Pedro Díaz", "Lucía Herrera", "Mario Tzoc", "Elena Xicay" };
            string[] marcas = { "HP", "Dell", "Lenovo", "Epson", "Canon", "Asus" };
            string[] problemas = { "No enciende", "Funciona muy lento", "No imprime", "Pantalla dañada", "Se sobrecalienta" };

            for (int i = 0; i < cantidad; i++)
            {
                int n = gestorClientes.Clientes.Count + 1;
                gestorClientes.Registrar(nombres[azar.Next(nombres.Length)], "5555" + azar.Next(1000, 9999),
                                         "cliente" + n + "@correo.com", (TipoCliente)azar.Next(0, 4));
                Cliente cliente = gestorClientes.Clientes[gestorClientes.Clientes.Count - 1];

                string serie = "SN" + azar.Next(100000, 999999) + i;
                string marca = marcas[azar.Next(marcas.Length)];
                Equipo equipo;
                int tipo = azar.Next(1, 4);

                if (tipo == 1)
                {
                    equipo = new Portatil(serie, marca, "Modelo-" + n, cliente, "Buen estado", "Ninguno", true);
                }
                else if (tipo == 2)
                {
                    equipo = new Escritorio(serie, marca, "Modelo-" + n, cliente, "Buen estado", "Ninguno", "ATX");
                }
                else
                {
                    equipo = new Impresora(serie, marca, "Modelo-" + n, cliente, "Buen estado", "Ninguno", "Laser");
                }
                gestorEquipos.Registrar(equipo);

                Prioridad prioridad = (Prioridad)azar.Next(0, 2);
                gestorSolicitudes.Crear(cliente, equipo, problemas[azar.Next(problemas.Length)], prioridad);
                gestorSolicitudes.Encolar(gestorSolicitudes.Solicitudes[gestorSolicitudes.Solicitudes.Count - 1]);
            }
        }
    }
}
