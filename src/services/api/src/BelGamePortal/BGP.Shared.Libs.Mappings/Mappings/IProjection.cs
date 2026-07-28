using AutoMapper;

namespace BGP.Shared.Libs.Mappings.Mappings;

public interface IProjection<TSource, TDestination>
{
    void Projection(Profile profile) => profile.CreateProjection<TSource, TDestination>();
}

