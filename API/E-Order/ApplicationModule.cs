using Autofac;
using E_Order.Domain.Queries;

namespace E_Order
{
    public class ApplicationModule : Autofac.Module
    {
        public string QueriesConnectionString { get; }

        public ApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;

        }

         protected override void Load(ContainerBuilder builder)
        {
           /*  builder.Register(c => new ProductQueries(QueriesConnectionString))
                .As<IProductQueries>()
                .InstancePerLifetimeScope();

            builder.Register(c => new BrandQueries(QueriesConnectionString))
                .As<IBrandQueries>()
                .InstancePerLifetimeScope();

            builder.Register(c => new ClientQueries(QueriesConnectionString))
                .As<IClientQueries>()
                .InstancePerLifetimeScope(); */

           /*  builder.Register(c => new OrderQueries(QueriesConnectionString))
                .As<IOrderQueries>()
                .InstancePerLifetimeScope(); */
        } 
    }
}