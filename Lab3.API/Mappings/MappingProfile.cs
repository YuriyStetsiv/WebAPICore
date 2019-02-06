using AutoMapper;
using Lab3.API.Models;
using Lab3.Domain.Models.Entities;
using System.Linq;

namespace Lab3.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SageModel, SageViewModel>()
                    .ForMember(view => view.Books, opt => opt.MapFrom(
                         x => x.SagesBooks.Select(y => y.Book).ToList()));

            CreateMap<BookModel, BookViewModel>()
                .ForMember(view => view.Sages, opt => opt.MapFrom(
                    x => x.SagesBooks.Select(y => y.Sage).ToList()));

            CreateMap<OrderModel, OrderViewModel>()
                .ForMember(view => view.Books, opt => opt.MapFrom(
                    x => x.BooksOrders.Select(y => y.Book).ToList()));

            CreateMap<BookViewModel, BookModel>()
                .ForMember(view => view.SagesBooks,
                    opt => opt.MapFrom(s => s.Sages.Select(tmp => new SagesBooksModel()
                    {
                        BookId = s.Id,
                        SageId = tmp.Id
                    })));

            CreateMap<SageViewModel, SageModel>()
                .ForMember(view => view.SagesBooks,
                    opt => opt.MapFrom(s => s.Books.Select(tmp => new SagesBooksModel()
                    {
                        BookId = tmp.Id,
                        SageId = s.Id
                    })));

            CreateMap<OrderViewModel, OrderModel>()
                .ForMember(view => view.BooksOrders,
                    opt => opt.MapFrom(s => s.Books.Select(tmp => new BooksOrderModel()
                    {
                        BookId = tmp.Id,
                        OrderId = s.Id
                    })));
        }
    }
}
