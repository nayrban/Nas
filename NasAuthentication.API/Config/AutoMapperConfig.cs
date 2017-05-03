using AutoMapper;
using AutoMapper.Configuration;
using NasDTOUtils.Dto;
using NasModel.Model;

namespace NasAuthentication.Config
{
    public class AutoMapperConfig
    {
        public static void Init()
        {
            var cfg = new MapperConfigurationExpression();
            cfg.CreateMap<Device, DeviceInfo>();            


            Mapper.Initialize(cfg);
        }
    }
}