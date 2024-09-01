using AutoMapper;
using NewMovieAPI.Dto.Movie;
using NewMovieAPI.Models;

namespace NewMovieAPI.Helpers
{
    public class MappingProfile : Profile
    {
        protected MappingProfile()
        {
            //CreateMap<Models.Genre, Dto.Genre.GenreDto>();
            //CreateMap<Dto.Genre.GenreDto, Models.Genre>();

            CreateMap<Movie, MovieDto>();
            CreateMap<MovieDto, Movie>()
                .ForMember(src => src.Poster, op => op.Ignore());
        }
    }
}
