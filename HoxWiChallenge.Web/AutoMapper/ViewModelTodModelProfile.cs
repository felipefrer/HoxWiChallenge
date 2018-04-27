using AutoMapper;
using HoxWiChallenge.Web.Models;
using HoxWiChallenge.Web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoxWiChallenge.Web.AutoMapper
{
    public class ViewModelTodModelProfile : Profile
    {
        #region Constructors

        public ViewModelTodModelProfile()
        {
            CreateMap<ForeignViewModel, Foreign>()
                .ForMember(f => f._id, opt => opt.MapFrom(fvm => fvm.Id)); ;
        }

        #endregion

        #region Properties

        public override string ProfileName => "ViewModelTodModelProfile";

        #endregion
    }
}