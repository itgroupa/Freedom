using AutoMapper;
using Freedom.Auth.DataSchema.Models;

namespace Freedom.Auth.Dal.Mappers.ToData;

internal class ResponsePaginatorToResponsePaginator : Profile
{
    public ResponsePaginatorToResponsePaginator()
    {
        CreateMap(typeof(ResponsePaginator<>), typeof(ResponsePaginator<>));
    }
}
