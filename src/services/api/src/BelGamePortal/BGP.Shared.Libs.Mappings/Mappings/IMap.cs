using AutoMapper;

namespace BGP.Shared.Libs.Mappings.Mappings;

public interface IMap
{
    IMappingExpression Mapping(Profile profile) => profile.CreateMap(GetType(), GetType());
}

public interface IMap<TSource, TDestination>
{
    IMappingExpression<TSource, TDestination> Mapping(Profile profile) => profile.CreateMap<TSource, TDestination>();
}
