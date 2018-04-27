using AutoMapper;

namespace HoxWiChallenge.Web.AutoMapper
{
    public class AutoMapperWebConfig
    {
        #region Properties



        #endregion

        #region Methods

        public static void Configure() => Mapper.Initialize(cfg =>
        {
            cfg.AddProfile<ModelToViewModelProfile>();
            cfg.AddProfile<ViewModelTodModelProfile>();
        });

        #endregion
    }
}