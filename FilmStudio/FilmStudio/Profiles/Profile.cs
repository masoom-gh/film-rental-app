using FilmStudio.Authentication;
using FilmStudio.Entities;
using FilmStudio.Profiles.Models;

namespace FilmStudio.Profiles
{
    public class Profile : AutoMapper.Profile
    {
        public Profile()
        {
            this.CreateMap<ApplicationUser, FilmStudioInfoModel>();

            this.CreateMap<Film, FilmModel>().ReverseMap();

            this.CreateMap<RentalRecord, RentalRecordModel>()
                .ForMember(r => r.FilmStudioChairmanName, o =>
                    o.MapFrom(f => f.FilmStudio.ChairmanName))
                .ForMember(r => r.FilmStudioUsername, o =>
                    o.MapFrom(f => f.FilmStudio.UserName))
                .ForMember(r => r.FilmStudioName, o =>
                    o.MapFrom(f => f.FilmStudio.StudioName))
                .ReverseMap()
                .ForMember(r=>r.FilmStudio,opt=>opt.Ignore())
                .ForMember(r=>r.Film,opt=>opt.Ignore());

            this.CreateMap<FilmUpdateModel, Film>();

            this.CreateMap<Film, FilmModelForNonauthorized>();
        }
    }
}
