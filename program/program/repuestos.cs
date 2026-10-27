using System;

namespace XelaTech
{
    public class Repuesto
    {
        private string codigo;
        public string Codigo
        {
            get { return codigo; }
            set
            {
                if (value == null || value.Trim() == "")
                {
                    throw new ArgumentException("El código del repuesto no puede quedar vacío.");
                }
                if (value.Length > 10)
                {
                    throw new ArgumentException("El código del repuesto no puede exceder los 10 caracteres.");
                }
                codigo = value.Trim();
            }
        }

        private string nombre;
        public string Nombre
        {
            get { return nombre; }
            set
            {
                if (value == null || value.Trim() == "")
                {
                    throw new ArgumentException("El nombre del repuesto no puede quedar vacío.");
                }
                if (value.Length > 50)
                {
                    throw new ArgumentException("El nombre del repuesto no puede exceder los 50 caracteres.");
                }
                nombre = value.Trim();
            }
        }

        private string categoria;
        public string Categoria
        {
            get { return categoria; }
            set
            {
                if (value == null || value.Trim() == "")
                {
                    throw new ArgumentException("La categoría no puede quedar vacía.");
                }
                if (value.Length > 30)
                {
                    throw new ArgumentException("La categoría no puede exceder los 30 caracteres.");
                }
                categoria = value.Trim();
            }
        }

        private int existencia;
        public int Existencia
        {
            get { return existencia; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("La existencia no puede ser negativa.");
                }
                existencia = value;
            }
        }

        private int existenciaMinima;
        public int ExistenciaMinima
        {
            get { return existenciaMinima; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("La existencia mínima no puede ser negativa.");
                }
                existenciaMinima = value;
            }
        }

        private decimal costoUnitario;
        public decimal CostoUnitario
        {
            get { return costoUnitario; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("El costo unitario no puede ser negativo.");
                }
                costoUnitario = value;
            }
        }

        private decimal precioVenta;
        public decimal PrecioVenta
        {
            get { return precioVenta; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("El precio de venta no puede ser negativo.");
                }
                precioVenta = value;
            }
        }

        private int cantidadUtilizada;
        public int CantidadUtilizada
        {
            get { return cantidadUtilizada; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("La cantidad utilizada no puede ser negativa.");
                }
                cantidadUtilizada = value;
            }
        }

        public Repuesto(string nombre, string categoria, int existencia,
                        int existenciaMinima, decimal costoUnitario, decimal precioVenta)
        {
            Nombre = nombre;
            Categoria = categoria;
            Existencia = existencia;
            ExistenciaMinima = existenciaMinima;
            CostoUnitario = costoUnitario;
            PrecioVenta = precioVenta;
            cantidadUtilizada = 0;
        }

        public bool HayExistencia(int cantidad)
        {
            return cantidad <= existencia;
        }

        public void Usar(int cantidad)
        {
            if (cantidad <= 0)
            {
                throw new ArgumentException("La cantidad a usar debe ser mayor que 0.");
            }
            if (!HayExistencia(cantidad))
            {
                throw new InvalidOperationException("No hay existencia suficiente del repuesto " + nombre + ".");
            }
            existencia = existencia - cantidad;
            cantidadUtilizada = cantidadUtilizada + cantidad;
        }

        public bool EstaBajoMinimo()
        {
            return existencia <= existenciaMinima;
        }

        public override string ToString()
        {
            string aviso = "";
            if (EstaBajoMinimo())
            {
                aviso = " | ¡EXISTENCIA BAJA!";
            }

            return Codigo + " | " + Nombre + " | " + Categoria +
                   " | Existencia: " + Existencia + " (mín. " + ExistenciaMinima + ")" +
                   " | Costo: Q" + CostoUnitario.ToString("F2") +
                   " | Venta: Q" + PrecioVenta.ToString("F2") +
                   " | Usados: " + CantidadUtilizada + aviso;
        }
    }
}
