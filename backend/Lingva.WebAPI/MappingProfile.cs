using AutoMapper;
using Lingva.DataAccessLayer.Entities;
using Lingva.WebAPI.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.WebAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DictionaryRecord, DictionaryRecordViewDTO>()
            .ForMember("Language", opt => opt.MapFrom(c => c.LanguageName))
            .ForMember("Word", opt => opt.MapFrom(c => c.WordName));

            CreateMap<DictionaryRecordCreatingDTO, DictionaryRecord>()
            .ForMember("UserId", opt => opt.MapFrom(c => c.User))
            .ForMember("LanguageName", opt => opt.MapFrom(c => c.Language))
            .ForMember("WordName", opt => opt.MapFrom(c => c.Word));

            CreateMap<DictionaryRecord, WordViewDTO>()
            .ForMember("Word", opt => opt.MapFrom(c => c.WordName));

            CreateMap<User, AuthenticateUserDto>();
            CreateMap<AuthenticateUserDto, User>();
        }
    }
}
