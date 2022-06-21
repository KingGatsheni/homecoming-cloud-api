using homecoming.api.Abstraction;
using homecoming.api.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace homecoming.api.Repo
{
    public class BookingRepo:IRepository<Booking>
    {
        HomecomingDbContext db;
        public BookingRepo(HomecomingDbContext cx)
        {
            db = cx;  
        }

        public void Add(Booking Params)
        {
            if(Params != null)
            {
                Booking booking = new Booking()
                {
                    CustomerId = Params.CustomerId,
                    RoomId = Params.RoomId,
                    NoOfRooms = Params.NoOfRooms,
                    NoOfOccupants = Params.NoOfOccupants,
                    BookingPrice = Params.BookingPrice,
                    Check_In_Date = Params.Check_In_Date,
                    Check_Out_Date = Params.Check_Out_Date,
                };

                db.Bookings.Add(booking);
                db.SaveChanges();
            }
        }

        public Booking GetBookingByRoomId(int id)
        {
            return db.Bookings.Include(o => o.Customer).Include(o => o.Room).Where(o=>o.RoomId.Equals(id)).OrderBy(o=>o.BookingId).Last();
        }
        public List<Booking> FindAll()
        {
            return db.Bookings.Include(o => o.Customer).Include(o => o.Room).ToList();
        }

        public Booking GetById(int id)
        {
            return db.Bookings.Include(o => o.Customer).Include(o => o.Room).SingleOrDefault( o => o.BookingId.Equals(id));
        }

        public void RemoveById(int id)
        {
            Booking booking = db.Bookings.SingleOrDefault(o => o.BookingId.Equals(id));
            if(booking != null)
            {
                db.Bookings.Remove(booking);
                db.SaveChanges();
            }
            
        }

        public void Update(int id, Booking Params)
        {
            //to be implemented
        }

        public bool updateBookedRoomStatus(int id)
        {
            var roomToUpdate = db.Rooms.SingleOrDefault(o => o.RoomId.Equals(id));
            if(roomToUpdate != null)
            {
                roomToUpdate.IsBooked = true;
                db.Rooms.Attach(roomToUpdate).Property(o => o.IsBooked).IsModified = true; ;
                int updated =  db.SaveChanges();
                if (updated > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
