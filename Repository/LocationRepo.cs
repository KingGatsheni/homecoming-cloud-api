using homecoming.api.Interfaces;
using homecoming.api.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homecoming.api.Repository
{
    public class LocationRepo : ILocation
    {
        private readonly HomecomingDbContext db;

        public LocationRepo(HomecomingDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<Accomodation> GetAccomodationsByLocationName(string locationName)
        {
            if(!string.IsNullOrWhiteSpace(locationName) && !string.IsNullOrEmpty(locationName))
            {
              int locId =   db.Location.SingleOrDefault(o=>o.LocationName.ToLower().Equals(locationName.ToLower())).LocationId;
                if(locId > 0)
                {
                    return db.Accomodations.Include(o=>o.AccomodationGallary).Include(o=>o.AccomodationRooms).ThenInclude(o=>o.RoomDetails).Include(o=>o.AccomodationRooms).ThenInclude(o=>o.RoomGallary).Where(o => o.LocationId.Equals(locId)); 
                }
            }
            return null;
        }

        public IEnumerable<Accomodation> GetAccomodationsByLocationNameAndCheckInDates(string locationName, DateTime checkinDate, DateTime checkDate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Location> GetLocationByName()
        {
            return db.Location.ToList();
        }
    }
}
