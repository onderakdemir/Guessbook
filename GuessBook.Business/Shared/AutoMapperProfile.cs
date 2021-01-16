using AutoMapper;
using GuessBook.Business.Models;
using GuessBook.EF.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuessBook.Business.Shared
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Questions,QuestionsDto>().ReverseMap();
            CreateMap<Options,OptionsDto>().ReverseMap();
            CreateMap<MemberAnswer,MemberAnswersDto>().ReverseMap()
                .ForMember(c=>c.CodeDate, opt=>opt.MapFrom(s=>DateTime.UtcNow))
                .ForMember(c=>c.Id, opt=>opt.Ignore());
        }

    }
}
