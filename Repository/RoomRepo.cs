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
    public class RoomRepo:IRepository<Room>
    {
        private readonly IWebHostEnvironment web;
        private IFileUpload<Room> fileUpLoad;
        private HomecomingDbContext db;
        private readonly BlobServiceClient client;
        private bool isToRevert = false;

        public RoomRepo(IWebHostEnvironment host,HomecomingDbContext context, BlobServiceClient client)
        {
            web = host;
            fileUpLoad = new RoomUpload(web, client);
            db = context;
            this.client = client;
        }

        /// <summary>
        /// This method still need to be worked on
        /// </summary>
        /// <param name="Params"></param>
        public void Add(Room Params)
        {
            if (Params !=null)
            {
                Room room = new Room()
                {
                    AccomodationId = Params.AccomodationId,
                    Description = Params.Description,
                    Price = Params.Price,
                    CreatedAt = DateTime.Now,
                    UpdatedOn = null
                };
                db.Rooms.Add(room);
                db.SaveChanges();

                int insertedRoomId = db.Rooms.Max(o=>o.RoomId);
                if (Params.RoomTypeInfo != null)
                {
                    RoomDetail type = new RoomDetail()
                    {
                        RoomId = insertedRoomId,
                        Type = Params.RoomTypeInfo.Type,
                        Description = Params.RoomTypeInfo.Description,
                        NumberOfBeds = Params.RoomTypeInfo.NumberOfBeds,
                        Television = Params.RoomTypeInfo.Television,
                        Air_condition = Params.RoomTypeInfo.Air_condition,
                        Wifi = Params.RoomTypeInfo.Wifi,
                        Private_bathroom = Params.RoomTypeInfo.Private_bathroom
                    };
                    db.RoomDetails.Add(type);
                    db.SaveChanges();
                }
                else
                {
                    RemoveById(insertedRoomId);
                    isToRevert = true;
                }

                if (Params.RoomGallary != null && !isToRevert)
                {
                    bool uploaded = fileUpLoad.MultiFileUpload(Params);
                    if (uploaded)
                    {
                        foreach (var image in Params.RoomGallary)
                        {
                            RoomImage roomImages = new RoomImage()
                            {
                                RoomId = db.Rooms.Max(o => o.RoomId),
                                ImageUrl = image.ImageUrl
                            };
                            db.RoomImages.Add(roomImages);
                            db.SaveChanges();
                        }
                    }
                }
            }
        }

        public List<Room> FindAll()
        {
            return db.Rooms.AsNoTracking().AsQueryable().Include(o=> o.Accomodation).Include(o => o.RoomTypeInfo).Include(o => o.RoomGallary).ToList();
        }

        public Room GetById(int id)
        {
            return db.Rooms.Include(o=>o.Accomodation).Include(o=> o.RoomTypeInfo).Include(o=>o.RoomGallary).FirstOrDefault(o => o.RoomId.Equals(id));
        }

        public Room GetRoomByAccomodationId(int id)
        {
            return db.Rooms.Include(o => o.Accomodation).Include(o => o.RoomTypeInfo).Include(o => o.RoomGallary).FirstOrDefault(o => o.AccomodationId.Equals(id));
        }

        public List<RoomDetail> GetRoomDetailsByRoomId(int roomId)
        {
            return db.RoomDetails.Include(o => o.Room).Where(o => o.RoomId.Equals(roomId)).ToList();
        }

        public void RemoveById(int id)
        {
            Room room = db.Rooms.Include(o =>o.RoomGallary).FirstOrDefault(o => o.RoomId.Equals(id));
            db.Rooms.Remove(room);
            db.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Params"></param>
        public void Update(int id, Room Params)
        {
            if(Params != null)
            {
                Room room = db.Rooms.SingleOrDefault(o => o.RoomId.Equals(id));
                room.Description = Params.Description;
                room.Price = Params.Price;
                room.UpdatedOn = DateTime.Now;

                db.Rooms.Attach(room);
                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
