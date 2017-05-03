using AutoMapper;
using AutoMapper.Configuration;
using NasDTOUtils.Dto;
using NasDTOUtils.Dto.Response;
using NasModel.AuthModel;
using NasModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasServiceAPI.Config
{
    public class AutoMapperConfig
    {
        public static void Init()
        {
            var cfg = new MapperConfigurationExpression();
            cfg.CreateMap<ApplicationUser, UserInfo>();
            cfg.CreateMap<MinistryCode, MinistryCodeResponse>();


            Mapper.Initialize(cfg);
        }
    }
}