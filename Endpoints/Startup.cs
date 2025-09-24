namespace ReadMovie.Endpoints
{
    public static class Startup
    {
        public static void UsarEndpoints(this WebApplication app)
        {
            CategoriaEndpoints.Add(app);
            GeneroEndpoints.Add(app);
            CriterioEndpoints.Add(app);
            LibroEndpoints.Add(app);
        }
    }
}
