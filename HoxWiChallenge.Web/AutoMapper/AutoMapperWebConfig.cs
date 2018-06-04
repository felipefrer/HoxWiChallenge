using AutoMapper;
using System;

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

            cfg.CreateMap<int, Gender>().ConvertUsing(x => (Gender)Enum.Parse(typeof(Gender), x.ToString()));
            cfg.CreateMap<Gender, int>().ConvertUsing(x => (int)x);
        });

        #endregion
    }
}