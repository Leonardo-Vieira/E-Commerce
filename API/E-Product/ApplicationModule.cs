using Autofac;
using E_Product.Domain.Queries;

namespace E_Product
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
            /* builder.Register(c => new ProductQueries(QueriesConnectionString))
                .As<IProductQueries>()
                .InstancePerLifetimeScope(); */

           /*  builder.Register(c => new BrandQueries(QueriesConnectionString))
                .As<IBrandQueries>()
                .InstancePerLifetimeScope(); */
          /*    builder.Register(c => new EFBrandQueries()
                .As<IBrandQueries>()
                .InstancePerLifetimeScope();  */

            /* builder.Register(c => new ProductTypeQueries(QueriesConnectionString))
                .As<IProductTypeQueries>()
                .InstancePerLifetimeScope();
                
            builder.Register(c => new ProviderQueries(QueriesConnectionString))
                .As<IProviderQueries>()
                .InstancePerLifetimeScope(); */
        }
    }
}