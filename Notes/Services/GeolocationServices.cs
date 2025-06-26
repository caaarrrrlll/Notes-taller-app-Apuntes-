using System;
using Microsoft.Maui.Devices.Sensors;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Repositories
{
    internal class GeolocationServices
    {
        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation;

        public async Task<Location> GetCurrentLocation()
        {
            try
            {
                _isCheckingLocation = true;

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                _cancelTokenSource = new CancellationTokenSource();

                Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                if (location != null)
                {
                    return location;
                }
                throw new Exception("Unable to get location");

            }
            
            catch (Exception ex)
            {
                throw;
                // Unable to get location
            }
            finally
            {
                _isCheckingLocation = false;
            }
            /*return new Location
            {
                Latitude = 0,
                Longitude = 0,
            };*/
        }


        public async Task<(string City, string Country)> GetCityAndCountryAsync(Location location)
        {
            if (location == null)
                throw new ArgumentNullException(nameof(location));

            var placemarks = await Geocoding.Default.GetPlacemarksAsync(location);

            var placemark = placemarks?.FirstOrDefault();
            if (placemark != null)
            {
                string city = placemark.Locality;
                string country = placemark.CountryName;
                return (city, country);
            }

            return (null, null);
        }




        public void CancelRequest()
        {
            if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
                _cancelTokenSource.Cancel();
        }



    }
}
