using System;

namespace XelaTech
{
    public class DetalleRepuesto
    {
        private int cantidad;
        public int Cantidad
        {
            get { return cantidad; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("La cantidad debe ser mayor que 0.");
                }
                cantidad = value;
            }
        }

        // Precio de venta que tenía el repuesto en el momento de usarlo
        private decimal precioAplicado;
        public decimal PrecioAplicado
        {
            get { return precioAplicado; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("El precio aplicado no puede ser negativo.");
                }
                precioAplicado = value;
            }
        }

        // Relación "usa" del diagrama: el repuesto al que corresponde este detalle
        private Repuesto repuesto;
        public Repuesto Repuesto
        {
            get { return repuesto; }
        }

        public DetalleRepuesto(Repuesto repuesto, int cantidad)
        {
            if (repuesto == null)
            {
                throw new ArgumentException("El detalle debe usar un repuesto existente.");
            }
            this.repuesto = repuesto;
            Cantidad = cantidad;
            PrecioAplicado = repuesto.PrecioVenta;
        }

        // +CalcularSubtotal() : decimal
        public decimal Subtotal
        {
            get { return cantidad * precioAplicado; }
        }
    }
}