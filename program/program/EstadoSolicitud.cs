namespace XelaTech
{
    // El orden de los valores es el orden del proceso (CambiarEstado depende de él)
    public enum EstadoSolicitud
    {
        Recibida,
        EnEspera,
        Asignada,
        EnDiagnostico,
        EnReparacion,
        ListaParaEntregar,
        Entregada,
        Cancelada
    }
}
