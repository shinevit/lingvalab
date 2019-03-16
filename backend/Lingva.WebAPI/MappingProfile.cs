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
            .ForMember("UserId", opt => opt.MapFrom(c => c.UserId))
            .ForMember("WordName", opt => opt.MapFrom(c => c.Word))
            .ForMember("LanguageName", opt => opt.MapFrom(c => c.Language))
            .ForMember("Translation", opt => opt.MapFrom(c => c.Translation))
            .ForMember("Context", opt => opt.MapFrom(c => c.Context))
            .ForMember("Picture", opt => opt.MapFrom(c => c.Picture))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<DictionaryRecord, WordViewDTO>()
            .ForMember("Word", opt => opt.MapFrom(c => c.WordName));

            CreateMap<ParserWord, WordDTO>()
            .ForMember("Word", opt => opt.MapFrom(c => c.Name));

            CreateMap<SubtitleRow, SubtitleRowDTO>()
                .ForMember("Value", opt => opt.MapFrom(c => c.Value))
                .ForMember("Language", opt => opt.MapFrom(c => c.LanguageName));

            CreateMap<User, AuthenticateUserDto>();
            CreateMap<AuthenticateUserDto, User>();
        }
    }
}
