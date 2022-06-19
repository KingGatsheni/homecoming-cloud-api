using homecoming.api.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace homecoming.api.Interfaces
{
    public interface ILocation
    {
       public  IEnumerable<Location> GetLocationByName();
       public IEnumerable<Accomodation> GetAccomodationsByLocationName( string locationName);
       public IEnumerable<Accomodation> GetAccomodationsByLocationNameAndCheckInDates(string locationName, DateTime checkinDate, DateTime checkDate);
            
    }
}
