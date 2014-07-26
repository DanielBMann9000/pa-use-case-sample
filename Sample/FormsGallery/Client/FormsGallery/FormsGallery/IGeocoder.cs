using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsGallery
{
    public interface IGeocoder
    {
         void GetCurrentLocation(Action<Cordinate> action);
    }


    public struct Cordinate
    {
       public double longitude;
        public double latitude;
    }
}
