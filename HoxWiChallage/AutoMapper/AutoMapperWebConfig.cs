using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace HoxWiChallage.AutoMapper
{
    public class AutoMapperWebConfig
    {
        public static void Configure() => Mapper.Initialize(cfg =>
        {
            //cfg.AddProfile<>();
        });
    }
}