using System;

namespace XelaTech
{
    public class Menu
    {
        // +Iniciar()
        public void Iniciar()
        {
            int opcion = 0;
            // corrregir funciones y hacer pruebas 
            while (opcion != 11)
            {
                MostrarMenuPrincipal();
                opcion = LeerOpcion();

                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("Gestión de clientes: módulo en construcción.");
                        break;
                    case 2:
                        Console.WriteLine("Gestión de técnicos: módulo en construcción.");
                        break;
                    case 3:
                        Console.WriteLine("Gestión de equipos: módulo en construcción.");
                        break;
                    case 4:
                        Console.WriteLine("Gestión de solicitudes: módulo en construcción.");
                        break;
                    case 5:
                        Console.WriteLine("Gestión de repuestos: módulo en construcción.");
                        break;
                    case 6:
                        Console.WriteLine("Atención de colas: módulo en construcción.");
                        break;
                    case 7:
                        Console.WriteLine("Ordenamientos: módulo en construcción.");
                        break;
                    case 8:
                        Console.WriteLine("Búsquedas: módulo en construcción.");
                        break;
                    case 9:
                        Console.WriteLine("Reportes: módulo en construcción.");
                        break;
                    case 10:
                        Console.WriteLine("Generar datos de prueba: módulo en construcción.");
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

        // -MostrarMenuPrincipal()
        private void MostrarMenuPrincipal()
        {
            Console.Clear();
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

       
        // Si el usuario escribe algo que no es un número, devuelve -1 y el menú no se rompe
        private int LeerOpcion()
        {
            Console.Write("Seleccione una opción: ");
            string texto = Console.ReadLine();
            int opcion;

            if (int.TryParse(texto, out opcion))
            {
                return opcion;
            }
            return -1;

        }
    }
}