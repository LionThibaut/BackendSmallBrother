using Application.UseCases.Animal.Dtos;
using Application.UseCases.Client.Dtos;
using Application.UseCases.Post.Dtos;
using AutoMapper;
using Infrastructure.Ef.DbEntities;

namespace Application.UseCases;

public static class Mapper
{
    private static AutoMapper.Mapper _instance;

    public static AutoMapper.Mapper GetInstance()
    {
        return _instance ??= CreateMapper();
    }

    private static AutoMapper.Mapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            // Client
            cfg.CreateMap<Domain.Client, DtoOutputClient>();
            cfg.CreateMap<DbClient, DtoOutputClient>();
            cfg.CreateMap<DbClient, Domain.Client>();
            cfg.CreateMap<Domain.Client, DtoOutputAnimal.DtoClientAnimal>();
            // Animal
            cfg.CreateMap<Domain.Animal,DtoOutputAnimal>();
            cfg.CreateMap<DbAnimal, DtoOutputAnimal>();
            cfg.CreateMap<DbAnimal, Domain.Animal>();
            cfg.CreateMap<Domain.Animal, DbAnimal>();
            // Post
            cfg.CreateMap<Domain.Post, DtoOutputPost>();
            cfg.CreateMap<DbPost, DtoOutputPost>();
            cfg.CreateMap<DbPost, Domain.Post>();
            cfg.CreateMap<Domain.Animal, DtoOutputPost.DtoAnimalPost>();
            cfg.CreateMap<Domain.Client, DtoOutputPost.DtoClientAnimalPost>();
        });
        
        return new AutoMapper.Mapper(config);
    }
}