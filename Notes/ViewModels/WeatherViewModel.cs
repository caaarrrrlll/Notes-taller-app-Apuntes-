using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Notes.Models;
using Notes.Repositories;
using Notes.Services;

namespace Notes.ViewModels
{

    internal class WeatherViewModel : INotifyPropertyChanged
    {
        private WeatherData _weatherData = new WeatherData();
        public WeatherData WeatherDatainfo
        {
            get => _weatherData;
            set
            {
                if (_weatherData != value)
                {
                    _weatherData = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(EstadoDelClima)); 
                }
            }
        }
         
        public ICommand RecalculateWeather { get; set; }

        public WeatherViewModel()
        {
            GetCurrentWeatherFromLocation();
            RecalculateWeather = new Command(async () => GetWeatherFromLocation());
        }

        public async void GetWeatherFromLocation()
        {
            WeatherServices weatherServices = new WeatherServices();
            WeatherDatainfo = await weatherServices.GetWeatherDataFromLocationAsync(_weatherData.latitude, _weatherData.longitude);
        }



        public async void GetCurrentWeatherFromLocation()
        {
            WeatherServices weatherServices = new WeatherServices();
            WeatherDatainfo = await weatherServices.GetCurrentLocationWeatherAsync();
        }

        public string EstadoDelClima
        {
            get
            {
                if (WeatherDatainfo == null || WeatherDatainfo.current == null)
                    return "Desconocido";

                double temperatura = WeatherDatainfo.current.temperature_2m;
                double humedad = WeatherDatainfo.current.relative_humidity_2m;

                if (humedad > 85 && temperatura < 20)
                    return "Lluvioso o neblinoso";
                else if (humedad > 70 && temperatura >= 20 && temperatura <= 30)
                    return "Bochornoso";
                else if (humedad < 40 && temperatura > 30)
                    return "Caluroso y seco";
                else if (humedad < 40 && temperatura < 15)
                    return "Frío y seco";
                else if (temperatura >= 25 && humedad < 60)
                    return "Soleado";
                else if (temperatura < 10)
                    return "Frío";
                else if (humedad > 80)
                    return "Húmedo";
                else
                    return "Clima templado";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));



   


    }
}
