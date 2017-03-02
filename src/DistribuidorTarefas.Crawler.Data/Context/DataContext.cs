using System.Data.Entity; 

namespace DistribuidorTarefas.Crawler.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext() : base("Minuto.Web")
        { }
    }
}
