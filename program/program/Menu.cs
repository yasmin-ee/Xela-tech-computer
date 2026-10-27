using System;
using System.Collections.Generic;

namespace XelaTech
{
    public class Menu
    {
        public void Iniciar()
        {
            Sistema sistema = new Sistema();
            int opcion = 0;

            while (opcion != 11)
            {
                MostrarMenuPrincipal();
                opcion = LeerOpcion();

                switch (opcion)
                {
                    case 1:
                        MenuClientes(sistema);
                        break;
                    case 2:
                        MenuTecnicos(sistema);
                        break;
                    case 3:
                        MenuEquipos(sistema);
                        break;
                    case 4:
                        MenuSolicitudes(sistema);
                        break;
                    case 5:
                        MenuRepuestos(sistema);
                        break;
                    case 6:
                        MenuColas(sistema);
                        break;
                    case 7:
                        MenuOrdenamientos(sistema);
                        break;
                    case 8:
                        MenuBusquedas(sistema);
                        break;
                    case 9:
                        MenuReportes(sistema);
                        break;
                    case 10:
                        MenuGenerarDatos(sistema);
                        break;
                    case 11:
                        Console.WriteLine("Gracias por usar XelaTech Soporte. ¡Hasta pronto!");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Ingrese un número del 1 al 11.");
                        break;
                }

                if (opcion != 11)
                {
                    Console.WriteLine();
                    Console.Write("Presione Enter para continuar...");
                    Console.ReadLine();
                }
            }
        }

        private void MostrarMenuPrincipal()
        {
            if (!Console.IsOutputRedirected)
            {
                Console.Clear();
            }
            Console.WriteLine("============================================");
            Console.WriteLine("              XELATECH SOPORTE");
            Console.WriteLine("============================================");
            Console.WriteLine("1. Gestión de clientes");
            Console.WriteLine("2. Gestión de técnicos");
            Console.WriteLine("3. Gestión de equipos");
            Console.WriteLine("4. Gestión de solicitudes");
            Console.WriteLine("5. Gestión de repuestos");
            Console.WriteLine("6. Atención de colas");
            Console.WriteLine("7. Ordenamientos");
            Console.WriteLine("8. Búsquedas");
            Console.WriteLine("9. Reportes");
            Console.WriteLine("10. Generar datos de prueba");
            Console.WriteLine("11. Salir");
            Console.WriteLine("============================================");
        }

        private int LeerOpcion()
        {
            Console.Write("Seleccione una opción: ");
            string texto = Console.ReadLine();

            if (texto == null)
            {
                Console.WriteLine();
                Console.WriteLine("Entrada finalizada. Cerrando el programa.");
                Environment.Exit(0);
            }

            int opcion;
            if (int.TryParse(texto, out opcion))
            {
                return opcion;
            }
            return -1;
        }

        private void MenuClientes(Sistema sistema)
        {
            int opcion = -1;

            while (opcion != 0)
            {
                Console.WriteLine();
                Console.WriteLine("--- GESTIÓN DE CLIENTES ---");
                Console.WriteLine("1. Registrar cliente");
                Console.WriteLine("2. Consultar cliente por código");
                Console.WriteLine("3. Actualizar teléfono y correo");
                Console.WriteLine("4. Buscar por nombre parcial");
                Console.WriteLine("5. Listar clientes");
                Console.WriteLine("0. Volver");
                opcion = LeerOpcion();

                try
                {
                    switch (opcion)
                    {
                        case 1:
                            RegistrarCliente(sistema);
                            break;
                        case 2:
                            Cliente c = sistema.GestorClientes.BuscarPorCodigo(LeerCodigo("Código del cliente: "));
                            if (c == null)
                            {
                                Console.WriteLine("No existe un cliente con ese código.");
                            }
                            else
                            {
                                Console.WriteLine(c);
                            }
                            break;
                        case 3:
                            sistema.GestorClientes.Actualizar(LeerCodigo("Código del cliente: "));
                            Console.WriteLine("Datos actualizados.");
                            break;
                        case 4:
                            MostrarLista(sistema.GestorClientes.BuscarPorNombreParcial(LeerTexto("Nombre o parte del nombre: ")), "clientes");
                            break;
                        case 5:
                            MostrarLista(sistema.GestorClientes.Clientes, "clientes");
                            break;
                        case 0:
                            break;
                        default:
                            Console.WriteLine("Opción no válida.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        private void RegistrarCliente(Sistema sistema)
        {
            string nombre = LeerTexto("Nombre completo: ");
            string telefono = LeerTexto("Teléfono: ");
            string correo = LeerTexto("Correo electrónico: ");
            TipoCliente tipo = LeerTipoCliente();

            sistema.GestorClientes.Registrar(nombre, telefono, correo, tipo);
            List<Cliente> lista = sistema.GestorClientes.Clientes;
            Console.WriteLine("Cliente registrado: " + lista[lista.Count - 1]);
        }

        private void MenuTecnicos(Sistema sistema)
        {
            int opcion = -1;

            while (opcion != 0)
            {
                Console.WriteLine();
                Console.WriteLine("--- GESTIÓN DE TÉCNICOS ---");
                Console.WriteLine("1. Registrar técnico");
                Console.WriteLine("2. Listar técnicos");
                Console.WriteLine("3. Calificar técnico");
                Console.WriteLine("0. Volver");
                opcion = LeerOpcion();

                try
                {
                    switch (opcion)
                    {
                        case 1:
                            string nombre = LeerTexto("Nombre completo: ");
                            Especialidad especialidad = LeerEspecialidad();
                            sistema.GestorTecnicos.Registrar(nombre, especialidad);
                            List<Tecnico> lista = sistema.GestorTecnicos.Tecnicos;
                            Console.WriteLine("Técnico registrado: " + lista[lista.Count - 1]);
                            break;
                        case 2:
                            MostrarLista(sistema.GestorTecnicos.Tecnicos, "técnicos");
                            break;
                        case 3:
                            string codigo = LeerCodigo("Código del técnico: ");
                            int calificacion = LeerEntero("Calificación (1 a 5): ");
                            sistema.GestorTecnicos.Calificar(codigo, calificacion);
                            Console.WriteLine("Calificación registrada.");
                            break;
                        case 0:
                            break;
                        default:
                            Console.WriteLine("Opción no válida.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }


        private void MenuEquipos(Sistema sistema)
        {
            int opcion = -1;

            while (opcion != 0)
            {
                Console.WriteLine();
                Console.WriteLine("--- GESTIÓN DE EQUIPOS ---");
                Console.WriteLine("1. Registrar equipo");
                Console.WriteLine("2. Listar equipos");
                Console.WriteLine("3. Ver ficha de un equipo");
                Console.WriteLine("4. Buscar por marca");
                Console.WriteLine("0. Volver");
                opcion = LeerOpcion();

                try
                {
                    switch (opcion)
                    {
                        case 1:
                            RegistrarEquipo(sistema);
                            break;
                        case 2:
                            MostrarEquipos(sistema.GestorEquipos.Equipos);
                            break;
                        case 3:
                            Console.WriteLine(BuscarEquipo(sistema, LeerCodigo("Código del equipo: ")).MostrarFicha());
                            break;
                        case 4:
                            MostrarEquipos(sistema.GestorEquipos.BuscarPorMarca(LeerTexto("Marca o parte de la marca: ")));
                            break;
                        case 0:
                            break;
                        default:
                            Console.WriteLine("Opción no válida.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        private void RegistrarEquipo(Sistema sistema)
        {
            Cliente propietario = sistema.GestorClientes.BuscarPorCodigo(LeerCodigo("Código del propietario (cliente): "));
            if (propietario == null)
            {
                throw new ArgumentException("El propietario no existe. Registre primero al cliente.");
            }

            Console.WriteLine("Tipo de equipo: 1. Computadora portátil  2. Computadora de escritorio  3. Impresora");
            int tipo = LeerEntero("Seleccione el tipo: ");
            if (tipo < 1 || tipo > 3)
            {
                throw new ArgumentException("Tipo de equipo no válido.");
            }

            string serie = LeerTexto("Número de serie: ");
            string marca = LeerTexto("Marca: ");
            string modelo = LeerTexto("Modelo: ");
            string estadoFisico = LeerTexto("Estado físico recibido: ");
            string accesorios = LeerTexto("Accesorios entregados (Enter si ninguno): ");

            Equipo equipo;
            if (tipo == 1)
            {
                string respuesta = LeerTexto("¿Incluye cargador? (s/n): ").ToLower();
                equipo = new Portatil(serie, marca, modelo, propietario, estadoFisico, accesorios, respuesta == "s");
            }
            else if (tipo == 2)
            {
                equipo = new Escritorio(serie, marca, modelo, propietario, estadoFisico, accesorios,
                                        LeerTexto("Tipo de torre (ATX, Mini, Gamer...): "));
            }
            else
            {
                equipo = new Impresora(serie, marca, modelo, propietario, estadoFisico, accesorios,
                                       LeerTexto("Tipo de impresión (Laser, Tinta...): "));
            }

            sistema.GestorEquipos.Registrar(equipo);
            Console.WriteLine("Equipo registrado con código " + equipo.CodigoInterno + ".");
        }

        private void MenuSolicitudes(Sistema sistema)
        {
            int opcion = -1;

            while (opcion != 0)
            {
                Console.WriteLine();
                Console.WriteLine("--- GESTIÓN DE SOLICITUDES ---");
                Console.WriteLine("1. Crear solicitud");
                Console.WriteLine("2. Encolar solicitud");
                Console.WriteLine("3. Listar solicitudes");
                Console.WriteLine("4. Registrar diagnóstico, tiempo estimado y mano de obra");
                Console.WriteLine("5. Agregar repuesto a una solicitud");
                Console.WriteLine("6. Cambiar estado");
                Console.WriteLine("7. Consultar historial de estados");
                Console.WriteLine("8. Deshacer último cambio de estado");
                Console.WriteLine("9. Cancelar solicitud");
                Console.WriteLine("0. Volver");
                opcion = LeerOpcion();

                try
                {
                    switch (opcion)
                    {
                        case 1:
                            CrearSolicitud(sistema);
                            break;
                        case 2:
                            sistema.GestorSolicitudes.Encolar(BuscarSolicitud(sistema, LeerEntero("Código de la solicitud: ")));
                            Console.WriteLine("Solicitud colocada en la cola.");
                            break;
                        case 3:
                            MostrarLista(sistema.GestorSolicitudes.Solicitudes, "solicitudes");
                            break;
                        case 4:
                            RegistrarDiagnostico(sistema);
                            break;
                        case 5:
                            AgregarRepuestoASolicitud(sistema);
                            break;
                        case 6:
                            int codigo = LeerEntero("Código de la solicitud: ");
                            sistema.GestorSolicitudes.CambiarEstado(codigo, LeerEstado());
                            Console.WriteLine("Estado actualizado: " + BuscarSolicitud(sistema, codigo));
                            break;
                        case 7:
                            VerHistorial(sistema);
                            break;
                        case 8:
                            int codigoDeshacer = LeerEntero("Código de la solicitud: ");
                            sistema.GestorSolicitudes.DeshacerCambio(codigoDeshacer);
                            Console.WriteLine("Cambio deshecho: " + BuscarSolicitud(sistema, codigoDeshacer));
                            break;
                        case 9:
                            CancelarSolicitud(sistema);
                            break;
                        case 0:
                            break;
                        default:
                            Console.WriteLine("Opción no válida.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        private void CrearSolicitud(Sistema sistema)
        {
            Cliente cliente = sistema.GestorClientes.BuscarPorCodigo(LeerCodigo("Código del cliente: "));
            if (cliente == null)
            {
                throw new ArgumentException("El cliente no existe. Registre primero al cliente.");
            }
            Equipo equipo = BuscarEquipo(sistema, LeerCodigo("Código del equipo: "));
            string descripcion = LeerTexto("Descripción del problema: ");
            Prioridad prioridad = LeerPrioridad();

            sistema.GestorSolicitudes.Crear(cliente, equipo, descripcion, prioridad);
            Console.WriteLine("Solicitud creada: " + BuscarSolicitud(sistema, sistema.GestorSolicitudes.ContadorCodigo));
            Console.WriteLine("Recuerde encolarla (opción 2) para que pueda atenderse.");
        }

        private void RegistrarDiagnostico(Sistema sistema)
        {
            Solicitud solicitud = BuscarSolicitud(sistema, LeerEntero("Código de la solicitud: "));

            if (solicitud.Estado == EstadoSolicitud.Entregada || solicitud.Estado == EstadoSolicitud.Cancelada)
            {
                throw new InvalidOperationException("No se puede modificar una solicitud entregada o cancelada.");
            }

            solicitud.Diagnostico = LeerTexto("Diagnóstico: ");
            solicitud.TiempoEstimado = LeerEntero("Tiempo estimado de atención (horas): ");
            solicitud.CostoManoObra = LeerDecimal("Costo de mano de obra (Q): ");
            Console.WriteLine("Datos guardados.");
        }

        private void AgregarRepuestoASolicitud(Sistema sistema)
        {
            Solicitud solicitud = BuscarSolicitud(sistema, LeerEntero("Código de la solicitud: "));
            Repuesto repuesto = BuscarRepuesto(sistema, LeerCodigo("Código del repuesto: "));
            int cantidad = LeerEntero("Cantidad a usar: ");

            solicitud.AgregarRepuesto(repuesto, cantidad);
            Console.WriteLine("Repuesto agregado. Costo de repuestos de la solicitud: Q" + solicitud.CostoRepuestos.ToString("F2"));

            if (repuesto.EstaBajoMinimo())
            {
                Console.WriteLine("AVISO: el repuesto " + repuesto.Nombre + " alcanzó o quedó por debajo de su existencia mínima (" + repuesto.Existencia + ").");
            }
        }

        private void VerHistorial(Sistema sistema)
        {
            Solicitud solicitud = BuscarSolicitud(sistema, LeerEntero("Código de la solicitud: "));

            Console.WriteLine("Estado actual: " + solicitud.Estado);
            if (solicitud.HistorialEstados.Count == 0)
            {
                Console.WriteLine("La solicitud todavía no tiene estados anteriores.");
                return;
            }

            Console.WriteLine("Historial (del más reciente al más antiguo):");
            foreach (EstadoSolicitud estado in solicitud.HistorialEstados)
            {
                Console.WriteLine("- " + estado);
            }
        }

        private void CancelarSolicitud(Sistema sistema)
        {
            Solicitud solicitud = BuscarSolicitud(sistema, LeerEntero("Código de la solicitud: "));
            solicitud.Cancelar();
            sistema.GestorSolicitudes.ColaUrgente = SinSolicitud(sistema.GestorSolicitudes.ColaUrgente, solicitud);
            sistema.GestorSolicitudes.ColaNormal = SinSolicitud(sistema.GestorSolicitudes.ColaNormal, solicitud);

            Console.WriteLine("Solicitud cancelada.");
        }

        private Queue<Solicitud> SinSolicitud(Queue<Solicitud> cola, Solicitud excluida)
        {
            Queue<Solicitud> nueva = new Queue<Solicitud>();

            foreach (Solicitud s in cola)
            {
                if (s != excluida)
                {
                    nueva.Enqueue(s);
                }
            }
            return nueva;
        }


        private void MenuRepuestos(Sistema sistema)
        {
            int opcion = -1;

            while (opcion != 0)
            {
                Console.WriteLine();
                Console.WriteLine("--- GESTIÓN DE REPUESTOS ---");
                Console.WriteLine("1. Registrar repuesto");
                Console.WriteLine("2. Listar repuestos");
                Console.WriteLine("3. Aumentar existencia");
                Console.WriteLine("0. Volver");
                opcion = LeerOpcion();

                try
                {
                    switch (opcion)
                    {
                        case 1:
                            string nombre = LeerTexto("Nombre: ");
                            string categoria = LeerTexto("Categoría: ");
                            int existencia = LeerEntero("Existencia: ");
                            int minima = LeerEntero("Existencia mínima: ");
                            decimal costo = LeerDecimal("Costo unitario (Q): ");
                            decimal precio = LeerDecimal("Precio de venta (Q): ");
                            sistema.GestorRepuestos.Registrar(new Repuesto(nombre, categoria, existencia, minima, costo, precio));
                            List<Repuesto> lista = sistema.GestorRepuestos.Repuestos;
                            Console.WriteLine("Repuesto registrado: " + lista[lista.Count - 1]);
                            break;
                        case 2:
                            MostrarLista(sistema.GestorRepuestos.Repuestos, "repuestos");
                            break;
                        case 3:
                            Repuesto r = BuscarRepuesto(sistema, LeerCodigo("Código del repuesto: "));
                            int cantidad = LeerEntero("Cantidad a agregar: ");
                            if (cantidad <= 0)
                            {
                                throw new ArgumentException("La cantidad debe ser mayor que 0.");
                            }
                            r.Existencia = r.Existencia + cantidad;
                            Console.WriteLine("Existencia actualizada: " + r);
                            break;
                        case 0:
                            break;
                        default:
                            Console.WriteLine("Opción no válida.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }


        private void MenuColas(Sistema sistema)
        {
            int opcion = -1;

            while (opcion != 0)
            {
                Console.WriteLine();
                Console.WriteLine("--- ATENCIÓN DE COLAS ---");
                Console.WriteLine("1. Ver solicitudes en espera");
                Console.WriteLine("2. Atender siguiente y asignar técnico");
                Console.WriteLine("3. Mostrar matriz de carga semanal");
                Console.WriteLine("0. Volver");
                opcion = LeerOpcion();

                try
                {
                    switch (opcion)
                    {
                        case 1:
                            sistema.Reportes.SolicitudesEnEspera();
                            break;
                        case 2:
                            AtenderSiguiente(sistema);
                            break;
                        case 3:
                            sistema.MatrizCarga.Mostrar();
                            break;
                        case 0:
                            break;
                        default:
                            Console.WriteLine("Opción no válida.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        private void AtenderSiguiente(Sistema sistema)
        {
            Solicitud siguiente = null;
            if (sistema.GestorSolicitudes.ColaUrgente.Count > 0)
            {
                siguiente = sistema.GestorSolicitudes.ColaUrgente.Peek();
            }
            else if (sistema.GestorSolicitudes.ColaNormal.Count > 0)
            {
                siguiente = sistema.GestorSolicitudes.ColaNormal.Peek();
            }

            if (siguiente == null)
            {
                Console.WriteLine("No hay solicitudes en espera en ninguna cola.");
                return;
            }

            Tecnico tecnico = sistema.GestorTecnicos.BuscarDisponible(siguiente.Equipo.ObtenerEspecialidad());
            if (tecnico == null)
            {
                Console.WriteLine("No hay un técnico disponible con la especialidad " + siguiente.Equipo.ObtenerEspecialidad() +
                                  ". La solicitud " + siguiente.Codigo + " permanece en cola.");
                return;
            }

            int dia = LeerEntero("Día de atención (1=Lunes ... 5=Viernes): ") - 1;
            if (dia < 0 || dia >= sistema.MatrizCarga.Dias.Length)
            {
                throw new ArgumentException("Día no válido. Debe estar entre 1 y " + sistema.MatrizCarga.Dias.Length + ".");
            }

            Solicitud atendida = sistema.GestorSolicitudes.AtenderSiguiente();
            atendida.AsignarTecnico(tecnico);
            sistema.MatrizCarga.Registrar(sistema.GestorTecnicos.Tecnicos.IndexOf(tecnico), dia);

            Console.WriteLine("Solicitud " + atendida.Codigo + " asignada a " + tecnico.NombreCompleto + " (" + tecnico.Codigo + ").");
        }


        private void MenuOrdenamientos(Sistema sistema)
        {
            int opcion = -1;

            while (opcion != 0)
            {
                Console.WriteLine();
                Console.WriteLine("--- ORDENAMIENTOS ---");
                Console.WriteLine("1. Solicitudes por costo total (Bubble Sort)");
                Console.WriteLine("2. Técnicos por servicios finalizados (Selection Sort)");
                Console.WriteLine("3. Repuestos por existencia (Shell Sort)");
                Console.WriteLine("4. Solicitudes por fecha de recepción (Quick Sort)");
                Console.WriteLine("0. Volver");
                opcion = LeerOpcion();

                try
                {
                    switch (opcion)
                    {
                        case 1:
                            OrdenarSolicitudesPorCosto(sistema);
                            break;
                        case 2:
                            OrdenarTecnicos(sistema);
                            break;
                        case 3:
                            OrdenarRepuestos(sistema);
                            break;
                        case 4:
                            OrdenarSolicitudesPorFecha(sistema);
                            break;
                        case 0:
                            break;
                        default:
                            Console.WriteLine("Opción no válida.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        private void OrdenarSolicitudesPorCosto(Sistema sistema)
        {
            List<Solicitud> lista = sistema.GestorSolicitudes.Solicitudes;
            bool asc = LeerAscendente();

            for (int i = 0; i < lista.Count; i++)
            {
                lista[i].CalcularTotal();
            }

            Solicitud[] arreglo = lista.ToArray();
            Ordenador.BubbleSort(arreglo, "total", asc);

            Console.WriteLine("Algoritmo: Bubble Sort | Criterio: costo total | Orden: " + TextoOrden(asc));
            MostrarArreglo(arreglo);
        }

        private void OrdenarTecnicos(Sistema sistema)
        {
            bool asc = LeerAscendente();

            Tecnico[] arreglo = sistema.GestorTecnicos.Tecnicos.ToArray();
            Ordenador.SelectionSort(arreglo, asc);

            Console.WriteLine("Algoritmo: Selection Sort | Criterio: servicios finalizados | Orden: " + TextoOrden(asc));
            MostrarArreglo(arreglo);
        }

        private void OrdenarRepuestos(Sistema sistema)
        {
            bool asc = LeerAscendente();

            Repuesto[] arreglo = sistema.GestorRepuestos.Repuestos.ToArray();
            Ordenador.ShellSort(arreglo, asc);

            Console.WriteLine("Algoritmo: Shell Sort | Criterio: existencia disponible | Orden: " + TextoOrden(asc));
            MostrarArreglo(arreglo);
        }

        private void OrdenarSolicitudesPorFecha(Sistema sistema)
        {
            bool asc = LeerAscendente();

            Solicitud[] arreglo = sistema.GestorSolicitudes.Solicitudes.ToArray();
            Ordenador.QuickSort(arreglo, 0, arreglo.Length - 1, asc);

            Console.WriteLine("Algoritmo: Quick Sort | Criterio: fecha de recepción | Orden: " + TextoOrden(asc));
            MostrarArreglo(arreglo);
        }


        private void MenuBusquedas(Sistema sistema)
        {
            int opcion = -1;

            while (opcion != 0)
            {
                Console.WriteLine();
                Console.WriteLine("--- BÚSQUEDAS ---");
                Console.WriteLine("1. Secuencial: clientes por nombre parcial");
                Console.WriteLine("2. Secuencial: solicitudes por estado");
                Console.WriteLine("3. Secuencial: equipos por marca");
                Console.WriteLine("4. Binaria: solicitud por código");
                Console.WriteLine("0. Volver");
                opcion = LeerOpcion();

                try
                {
                    switch (opcion)
                    {
                        case 1:
                            Console.WriteLine("Búsqueda secuencial por nombre parcial:");
                            MostrarLista(sistema.GestorClientes.BuscarPorNombreParcial(LeerTexto("Nombre o parte del nombre: ")), "clientes");
                            break;
                        case 2:
                            EstadoSolicitud estado = LeerEstado();
                            Console.WriteLine("Búsqueda secuencial por estado " + estado + ":");
                            MostrarLista(sistema.GestorSolicitudes.BuscarPorEstado(estado), "solicitudes");
                            break;
                        case 3:
                            Console.WriteLine("Búsqueda secuencial por marca:");
                            MostrarEquipos(sistema.GestorEquipos.BuscarPorMarca(LeerTexto("Marca o parte de la marca: ")));
                            break;
                        case 4:
                            BusquedaBinariaPorCodigo(sistema);
                            break;
                        case 0:
                            break;
                        default:
                            Console.WriteLine("Opción no válida.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        private void BusquedaBinariaPorCodigo(Sistema sistema)
        {
            int codigo = LeerEntero("Código de la solicitud a buscar: ");

            Solicitud[] arreglo = sistema.GestorSolicitudes.Solicitudes.ToArray();
            int posicion = Buscador.BusquedaBinaria(arreglo, codigo);

            Console.WriteLine("Búsqueda binaria por código:");
            if (posicion == -1)
            {
                Console.WriteLine("No se encontró la solicitud " + codigo + ".");
            }
            else
            {
                Console.WriteLine("Encontrada en la posición " + posicion + ": " + arreglo[posicion]);
            }
        }

        private void MenuReportes(Sistema sistema)
        {
            int opcion = -1;

            while (opcion != 0)
            {
                Console.WriteLine();
                Console.WriteLine("--- REPORTES ---");
                Console.WriteLine("1. Solicitudes en espera (por prioridad)");
                Console.WriteLine("2. Solicitudes activas, finalizadas y canceladas");
                Console.WriteLine("3. Técnicos disponibles y con carga completa");
                Console.WriteLine("4. Técnico con más servicios finalizados");
                Console.WriteLine("5. Equipos atendidos por tipo");
                Console.WriteLine("6. Repuestos con existencia mínima o agotados");
                Console.WriteLine("7. Ingresos totales por servicios finalizados");
                Console.WriteLine("8. Matriz semanal de carga de trabajo");
                Console.WriteLine("(Los reportes ordenados y de búsqueda están en los módulos 7 y 8)");
                Console.WriteLine("0. Volver");
                opcion = LeerOpcion();

                try
                {
                    switch (opcion)
                    {
                        case 1:
                            sistema.Reportes.SolicitudesEnEspera();
                            break;
                        case 2:
                            sistema.Reportes.SolicitudesPorEstado();
                            break;
                        case 3:
                            sistema.Reportes.TecnicosDisponibles();
                            break;
                        case 4:
                            sistema.Reportes.TecnicoMasServicios();
                            break;
                        case 5:
                            sistema.Reportes.EquiposPorTipo();
                            break;
                        case 6:
                            sistema.Reportes.RepuestosBajoMinimo();
                            break;
                        case 7:
                            sistema.Reportes.IngresosTotales();
                            break;
                        case 8:
                            sistema.MatrizCarga.Mostrar();
                            break;
                        case 0:
                            break;
                        default:
                            Console.WriteLine("Opción no válida.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        private void MenuGenerarDatos(Sistema sistema)
        {
            try
            {
                int cantidad = LeerEntero("Cantidad de solicitudes de prueba a generar (1 a 100): ");
                sistema.GeneradorDatos.Generar(cantidad);

                Console.WriteLine("Datos generados correctamente. Totales actuales:");
                Console.WriteLine("Clientes: " + sistema.GestorClientes.Clientes.Count);
                Console.WriteLine("Técnicos: " + sistema.GestorTecnicos.Tecnicos.Count);
                Console.WriteLine("Equipos: " + sistema.GestorEquipos.Equipos.Count);
                Console.WriteLine("Repuestos: " + sistema.GestorRepuestos.Repuestos.Count);
                Console.WriteLine("Solicitudes: " + sistema.GestorSolicitudes.Solicitudes.Count);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private string LeerTexto(string mensaje)
        {
            Console.Write(mensaje);
            string texto = Console.ReadLine();
            if (texto == null)
            {
                return "";
            }
            return texto;
        }

        private string LeerCodigo(string mensaje)
        {
            return LeerTexto(mensaje).Trim().ToUpper();
        }

        private int LeerEntero(string mensaje)
        {
            int valor;
            if (int.TryParse(LeerTexto(mensaje), out valor))
            {
                return valor;
            }
            throw new FormatException("Debe ingresar un número entero.");
        }

        private decimal LeerDecimal(string mensaje)
        {
            decimal valor;
            if (decimal.TryParse(LeerTexto(mensaje), out valor))
            {
                return valor;
            }
            throw new FormatException("Debe ingresar un número válido.");
        }

        private bool LeerAscendente()
        {
            int opcion = LeerEntero("Orden: 1. Ascendente  2. Descendente: ");
            if (opcion != 1 && opcion != 2)
            {
                throw new ArgumentException("Opción de orden no válida.");
            }
            return opcion == 1;
        }

        private string TextoOrden(bool asc)
        {
            if (asc)
            {
                return "ascendente";
            }
            return "descendente";
        }

        private TipoCliente LeerTipoCliente()
        {
            Console.WriteLine("Tipo de cliente: 1. Estudiante  2. Profesional  3. Negocio  4. Institución");
            int opcion = LeerEntero("Seleccione el tipo: ");
            if (opcion < 1 || opcion > 4)
            {
                throw new ArgumentException("Tipo de cliente no válido.");
            }
            return (TipoCliente)(opcion - 1);
        }

        private Especialidad LeerEspecialidad()
        {
            Console.WriteLine("Especialidad: 1. Portátiles  2. Escritorio  3. Impresoras  4. General");
            int opcion = LeerEntero("Seleccione la especialidad: ");
            if (opcion < 1 || opcion > 4)
            {
                throw new ArgumentException("Especialidad no válida.");
            }
            return (Especialidad)(opcion - 1);
        }

        private Prioridad LeerPrioridad()
        {
            Console.WriteLine("Prioridad: 1. Normal  2. Urgente");
            int opcion = LeerEntero("Seleccione la prioridad: ");
            if (opcion < 1 || opcion > 2)
            {
                throw new ArgumentException("Prioridad no válida.");
            }
            return (Prioridad)(opcion - 1);
        }

        private EstadoSolicitud LeerEstado()
        {
            Console.WriteLine("Estado: 1. Recibida  2. En espera  3. Asignada  4. En diagnóstico  5. En reparación");
            Console.WriteLine("        6. Lista para entregar  7. Entregada  8. Cancelada");
            int opcion = LeerEntero("Seleccione el estado: ");
            if (opcion < 1 || opcion > 8)
            {
                throw new ArgumentException("Estado no válido.");
            }
            return (EstadoSolicitud)(opcion - 1);
        }

        private Solicitud BuscarSolicitud(Sistema sistema, int codigo)
        {
            List<Solicitud> lista = sistema.GestorSolicitudes.Solicitudes;
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].Codigo == codigo)
                {
                    return lista[i];
                }
            }
            throw new ArgumentException("No existe una solicitud con el código " + codigo + ".");
        }

        private Equipo BuscarEquipo(Sistema sistema, string codigo)
        {
            List<Equipo> lista = sistema.GestorEquipos.Equipos;
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].CodigoInterno == codigo)
                {
                    return lista[i];
                }
            }
            throw new ArgumentException("No existe un equipo con el código " + codigo + ".");
        }

        private Repuesto BuscarRepuesto(Sistema sistema, string codigo)
        {
            List<Repuesto> lista = sistema.GestorRepuestos.Repuestos;
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].Codigo == codigo)
                {
                    return lista[i];
                }
            }
            throw new ArgumentException("No existe un repuesto con el código " + codigo + ".");
        }

        private void MostrarLista<T>(List<T> lista, string nombre)
        {
            if (lista.Count == 0)
            {
                Console.WriteLine("No hay " + nombre + " para mostrar.");
                return;
            }
            for (int i = 0; i < lista.Count; i++)
            {
                Console.WriteLine(lista[i]);
            }
        }

        private void MostrarArreglo<T>(T[] arreglo)
        {
            for (int i = 0; i < arreglo.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + arreglo[i]);
            }
        }

        private void MostrarEquipos(List<Equipo> lista)
        {
            if (lista.Count == 0)
            {
                Console.WriteLine("No hay equipos para mostrar.");
                return;
            }
            for (int i = 0; i < lista.Count; i++)
            {
                Equipo e = lista[i];
                Console.WriteLine(e.CodigoInterno + " | " + e.GetType().Name + " | " + e.Marca + " " + e.Modelo +
                                  " | Serie: " + e.NumeroSerie + " | Propietario: " + e.Propietario.NombreCompleto +
                                  " | Solicitud activa: " + (e.TieneSolicitudActiva ? "Sí" : "No"));
            }
        }
    }
}
