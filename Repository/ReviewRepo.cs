using homecoming.api.Abstraction;
using homecoming.api.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homecoming.api.Repo
{
    public class ReviewRepo : IRepository<Review>
    {
        HomecomingDbContext db;
        public ReviewRepo(HomecomingDbContext dbContext)
        {
            db = dbContext;
        }
        public void Add(Review Params)
        {
            if (Params != null)
            {
                Review review = new Review() 
                { 
                CustomerId = Params.CustomerId,
                BookingId = Params.BookingId,
                RatingId = Params.RatingId,
                ReviewTitle = Params.ReviewTitle,
                positive_comment = Params.positive_comment,
                negative_comment = Params.negative_comment,
                ReviewedAt = DateTime.Now
                };

                db.Reviews.Add(review);
                db.SaveChanges();
            }
        }

        public List<Review> FindAll()
        {
            return db.Reviews.Include(o => o.Customer).Include(o => o.Booking).Include(o => o.Rating).ToList();
        }

        public Review GetById(int id)
        {
            return db.Reviews.Include(o=>o.Customer).Include(o => o.Booking).Include(o => o.Rating).SingleOrDefault(o => o.ReviewId.Equals(id));
        }

        public void RemoveById(int id)
        {
            Review review = db.Reviews.SingleOrDefault(o => o.ReviewId.Equals(id));
            if (review != null)
            {
                db.Reviews.Remove(review);
                db.SaveChanges();
            }
        }

        public void Update(int id, Review Params)
        {
            Review review = db.Reviews.SingleOrDefault(o => o.ReviewId.Equals(id));
            if(review != null)
            {
                review.ReviewTitle = Params.ReviewTitle;
                review.positive_comment = Params.positive_comment;
                review.negative_comment = Params.negative_comment;

                db.Reviews.Attach(review);
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
