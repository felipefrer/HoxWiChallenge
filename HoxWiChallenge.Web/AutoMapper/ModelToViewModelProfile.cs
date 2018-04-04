using AutoMapper;
using HoxWiChallenge.Web.Models;
using HoxWiChallenge.Web.Models.ViewModel;

namespace HoxWiChallenge.Web.AutoMapper
{
    public class ModelToViewModelProfile : Profile
    {
        #region Constructors

        public ModelToViewModelProfile()
        {
            CreateMap<Foreign, ForeignViewModel>()
                .ForMember(fvm => fvm.Id, opt => opt.MapFrom(f => f._id));
        }

        #endregion

        #region Properties

        public override string ProfileName => "ModelToViewModelProfile";

        #endregion

        #region Methods



        #endregion
    }
}