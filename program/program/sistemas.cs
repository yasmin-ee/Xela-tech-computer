using System;

namespace XelaTech
{
    // Clase central: crea los gestores una sola vez y los conecta entre sí.
    // Menu usa un objeto Sistema para llegar a todos los módulos.
    public class Sistema
    {
        private GestorClientes gestorClientes;
        public GestorClientes GestorClientes
        {
            get { return gestorClientes; }
        }

        private GestorTecnicos gestorTecnicos;
        public GestorTecnicos GestorTecnicos
        {
            get { return gestorTecnicos; }
        }

        private GestorEquipos gestorEquipos;
        public GestorEquipos GestorEquipos
        {
            get { return gestorEquipos; }
        }

        private GestorSolicitudes gestorSolicitudes;
        public GestorSolicitudes GestorSolicitudes
        {
            get { return gestorSolicitudes; }
        }

        private GestorRepuestos gestorRepuestos;
        public GestorRepuestos GestorRepuestos
        {
            get { return gestorRepuestos; }
        }

        private MatrizCarga matrizCarga;
        public MatrizCarga MatrizCarga
        {
            get { return matrizCarga; }
        }

        private Reportes reportes;
        public Reportes Reportes
        {
            get { return reportes; }
        }

        private GeneradorDatos generadorDatos;
        public GeneradorDatos GeneradorDatos
        {
            get { return generadorDatos; }
        }

        public Sistema()
        {
            gestorClientes = new GestorClientes();
            gestorTecnicos = new GestorTecnicos();
            gestorEquipos = new GestorEquipos();
            gestorSolicitudes = new GestorSolicitudes();
            gestorRepuestos = new GestorRepuestos();
            matrizCarga = new MatrizCarga();
            reportes = new Reportes(gestorSolicitudes, gestorTecnicos, gestorRepuestos);
            generadorDatos = new GeneradorDatos(gestorClientes, gestorTecnicos, gestorEquipos,
                                                gestorRepuestos, gestorSolicitudes, matrizCarga);
        }
    }
}
