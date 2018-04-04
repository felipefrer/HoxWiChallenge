using AutoMapper;
using HoxWiChallage.Models;
using HoxWiChallage.Models.HoxWi;
using HoxWiChallage.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoxWiChallage.AutoMapper
{
    public class HoxWiResultToModel : Profile
    {
        public override string ProfileName => "HoxWiResultToModel";

        public HoxWiResultToModel()
        {
            CreateMap<Foreign, ForeignViewModel>().ReverseMap();
            CreateMap<HoxWiResult, Foreign>().ReverseMap();
        }
    }
}