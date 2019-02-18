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
            .ForMember("TranslationLanguage", opt => opt.MapFrom(c => c.TranslationLanguageName))
            .ForMember("OriginalPhrase", opt => opt.MapFrom(c => c.OriginalPhraseName));

            CreateMap<DictionaryRecordCreatingDTO, DictionaryRecord>()
            .ForMember("UserId", opt => opt.MapFrom(c => c.User))
            .ForMember("TranslationLanguageName", opt => opt.MapFrom(c => c.TranslationLanguage))
            .ForMember("OriginalPhraseName", opt => opt.MapFrom(c => c.OriginalPhrase));
        }
    }
}
