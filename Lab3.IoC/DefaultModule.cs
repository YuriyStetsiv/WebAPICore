using Autofac;
using Lab3.Domain.Services;
using Lab3.Domain.Services.Interface;
using Lab3.Infrastructure.Sql;
using Lab3.Infrastructure.UnitOfWork;

namespace Lab3.IoC
{
    public class DefaultModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Register infrastructure dependencies.
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            // Register domain services.
            builder.RegisterType<SageService>().As<ISageService>();
            builder.RegisterType<BooksService>().As<IBookService>();
            builder.RegisterType<OrderService>().As<IOrderService>();
            builder.RegisterType<UserCartService>().As<IUserCartService>();
        }
    }
}
