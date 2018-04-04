using AutoMapper;
using HoxWiChallenge.Web.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace HoxWiChallenge.Web.AutoMapper
{
    public class HoxWiResultToModelProfile : Profile
    {
        #region Properties

        public override string ProfileName => "HoxWiResultToModelProfile";

        #endregion

        #region Methods

        public HoxWiResultToModelProfile()
        {
            //CreateMap<(, Foreign>();
  
                //.ForMember(f => f.FistName, cfg => cfg.MapFrom("FistName"))
                //.ForMember(f => f.SureName, cfg => cfg.MapFrom("SureName"))
                //.ForMember(f => f.BornDate, cfg => cfg.MapFrom("BornDate"))
                //.ForMember(f => f.Nationality, cfg => cfg.MapFrom("Nationality"))
                //.ForMember(f => f.Arrived, cfg => cfg.MapFrom("Arrived"))
                //.ForMember(f => f.KindOfVisa, cfg => cfg.MapFrom("KindOfVisa"))
                //.ForMember(f => f.hCreationDate, cfg => cfg.MapFrom("hCreationDate"));
        }

        #endregion
    }
}