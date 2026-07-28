using AutoMapper;

namespace BGP.Shared.Libs.Mappings.Mappings;

public interface IMapFrom<T>
{
    IMappingExpression Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
}
