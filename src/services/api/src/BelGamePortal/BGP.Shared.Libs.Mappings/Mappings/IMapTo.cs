using AutoMapper;

namespace BGP.Shared.Libs.Mappings.Mappings;

public interface IMapTo<T>
{
    IMappingExpression Mapping(Profile profile) => profile.CreateMap(GetType(), typeof(T));
}

