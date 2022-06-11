using homecoming.api.Abstraction;
using homecoming.api.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace homecoming.api.Repo
{
    public class RatingRepo : IRepository<Rating>
    {
        HomecomingDbContext db;
        public RatingRepo(HomecomingDbContext cx)
        {
            this.db = cx;
        }
        public void Add(Rating Params)
        {
            if(Params != null)
            {
                Rating rating = new Rating()
                {
                    Staff = Params.Staff,
                    CleanLiness = Params.CleanLiness,
                    Location = Params.Location,
                    Value_for_money = Params.Value_for_money,
                    Facilities = Params.Facilities,
                    Comfort = Params.Comfort,
                    Free_Wifi = Params.Free_Wifi,
                    CreatedAt = DateTime.Now
                };
                db.Ratings.Add(rating);
                db.SaveChanges();
            }
        }

        public List<Rating> FindAll()
        {
            return db.Ratings.ToList();
        }

        public Rating GetById(int id)
        {
          return db.Ratings.SingleOrDefault(o => o.RatingId.Equals(id));
          
        }

        public void RemoveById(int id)
        {
            Rating rating = db.Ratings.SingleOrDefault(o => o.RatingId.Equals(id));
            if(rating != null)
            {
                db.Ratings.Remove(rating);
                db.SaveChanges();
            }
        }

        public void Update(int id, Rating Params)
        {
            Rating rating = db.Ratings.SingleOrDefault(o => o.RatingId.Equals(id));
            if (rating != null)
            {
                rating.Staff = Params.Staff;
                rating.CleanLiness = Params.CleanLiness;
                rating.Location = Params.Location;
                rating.Value_for_money = Params.Value_for_money;
                rating.Facilities = Params.Facilities;
                rating.Comfort = Params.Comfort;
                rating.Free_Wifi = Params.Free_Wifi;

                db.Ratings.Attach(rating);
                db.Entry(rating).State = EntityState.Modified;
                db.SaveChanges();
            }

        }
    }
}
