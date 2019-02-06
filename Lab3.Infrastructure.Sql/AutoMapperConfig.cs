using AutoMapper;
using Lab3.Domain.Models.Entities;
using Lab3.Infrastructure.Sql.Models;

namespace Lab3.Infrastructure.Sql
{
    public class AutoMapperConfig
    {
        private static bool _isConfigured;
        private static readonly object Lock = new object();

        public static void Configure()
        {
            lock (Lock)
            {
                if (!_isConfigured)
                {
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<SageModel, Sages>().ReverseMap();
                        cfg.CreateMap<BookModel, Books>().ReverseMap();
                        cfg.CreateMap<OrderModel, Orders>().ReverseMap();
                        cfg.CreateMap<BooksOrderModel, BooksOrders>().ReverseMap();
                        cfg.CreateMap<UserCartModel, UserCart>().ReverseMap();
                        cfg.CreateMap<SagesBooksModel, SagesBooks>().ReverseMap();
                    });
                    _isConfigured = true;
                }
            }
        }
    }
}
