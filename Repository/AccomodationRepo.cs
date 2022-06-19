using Azure.Storage.Blobs;
using homecoming.api.Abstraction;
using homecoming.api.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace homecoming.api.Repo
{
    public class AccomodationRepo:IRepository<Accomodation>
    {
        private readonly IWebHostEnvironment web;
        private IFileUpload<Accomodation> fileUpLoad;
        private HomecomingDbContext db;
        private readonly BlobServiceClient client;

        public AccomodationRepo(IWebHostEnvironment host, HomecomingDbContext context, BlobServiceClient client)
        {
            web = host;
            fileUpLoad = new AccomodationUpload(web, client);
            db = context;
            this.client = client;
        }

        public void Add(Accomodation Params)
        {
            bool uploaded = fileUpLoad.MultiFileUpload(Params);
            if (uploaded)
            {
                Accomodation accomodation = new Accomodation()
                {
                    BusinessId = Params.BusinessId,
                    LocationId = Params.LocationId,
                    CoverPhoto = fileUpLoad.FileUploadAsync(Params.CoverImage),
                    AccomodationName = Params.AccomodationName,
                    Description = Params.Description,
                    CreatedAt = DateTime.Now,
                    UpdatedOn = null
                };
                db.Accomodations.Add(accomodation);
                db.SaveChanges();
            }
           
            foreach (var image in Params.AccomodationGallary)
            {
                ListingImage listing = new ListingImage()
                {
                    AccomodationId = db.Accomodations.Max(o => o.AccomodationId),
                    ImageUrl = image.ImageUrl
                };
                db.ListingImages.Add(listing);
                db.SaveChanges();
            }
           
        }

        public List<Accomodation> FindAll()
        {

            return db.Accomodations.Include(o => o.GeoLocation).AsNoTracking().AsQueryable().Include(o=> o.Business).Include(o => o.AccomodationRooms).Include(o => o.AccomodationGallary).ToList();
        }

        public Accomodation GetById(int id)
        {
            return db.Accomodations.AsNoTracking().AsQueryable().Include(o => o.GeoLocation).Include(o=>o.Business).Include(o=>o.AccomodationGallary).Include(o=>o.AccomodationRooms).ThenInclude(o=>o.RoomDetails).Include(o=>o.AccomodationRooms).ThenInclude(o=>o.RoomGallary).SingleOrDefault(o => o.AccomodationId.Equals(id));
        }
        public List<Accomodation> GetAccomodationByBusinessId(int id)
        {
            return db.Accomodations.AsNoTracking().AsQueryable().Include(o => o.GeoLocation).Include(o => o.AccomodationGallary).Include(o => o.AccomodationRooms).Where(o => o.BusinessId.Equals(id)).ToList();
        }
        public List<Accomodation> GetAccomodationsByBusinessUserId(string id)
        {
            var bussId = db.Businesses.SingleOrDefault(o => o.AspUser.Equals(id));
            if(bussId != null)
            {
                int businessId = bussId.BusinessId;
                return db.Accomodations.AsNoTracking().AsQueryable().Include(o=>o.GeoLocation).Include(o => o.AccomodationGallary).Include(o => o.AccomodationRooms).ThenInclude(o=>o.RoomDetails).Include(o=>o.AccomodationRooms).ThenInclude(o=>o.RoomGallary).Where(o => o.BusinessId.Equals(businessId)).ToList();
            }
            return null;
        }

        public void RemoveById(int id)
        {
            Accomodation accomodation = db.Accomodations.SingleOrDefault(o => o.AccomodationId.Equals(id));
            db.Accomodations.Remove(accomodation);
            db.SaveChanges();
        }

        public void Update(int id, Accomodation Params)
        {
            if(Params  != null)
            {
                Accomodation accomodation = db.Accomodations.SingleOrDefault(o => o.AccomodationId.Equals(id));
                accomodation.AccomodationName = Params.AccomodationName;
                accomodation.Description = Params.Description;
                accomodation.UpdatedOn = DateTime.Now;

                db.Accomodations.Attach(accomodation);
                db.Entry(accomodation).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
