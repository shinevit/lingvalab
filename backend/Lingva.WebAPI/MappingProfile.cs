using AutoMapper;
using Lingva.DataAccessLayer.Dto;
using Lingva.DataAccessLayer.Entities;
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
            .ForMember("TranslationLanguage", opt => opt.MapFrom(c => c.TranslationLanguage.Name))
            .ForMember("OriginalPhrase", opt => opt.MapFrom(c => c.OriginalPhrase.Name));
        }
    }
}
