using AutoMapper;
using System;
using System.Collections.Generic;
using WeatherApp.DataLayer.Entities;
using WeatherApp.Models;

namespace WeatherApp.API.Helpers
{
    public class WeatherHelper
    {
        private readonly IMapper _mapper;

        public WeatherHelper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public DateTime GetDateTime(string dateTime)
        {
            DateTime date;

            if (string.IsNullOrWhiteSpace(dateTime))
            {
                date = DateTime.Now;
            }

            else
            {
                date = DateTime.ParseExact(dateTime, "yyyy-MM-dd HH:mm:ss",
                                   System.Globalization.CultureInfo.InvariantCulture);
            }
            return date;
        }

        public WeatherInfoModel FillModel(City res)
        {
            var resModel = new WeatherInfoModel()
            {
                CityId = res.Id,
                CityName = res.Name
            };

            var infoModel = _mapper.Map<WeatherInfoModel>(resModel);
            infoModel.WeatherInfo = _mapper.Map<List<WeatherModel>>(res.WeatherConditions);
            return resModel;
        }
    }
}
