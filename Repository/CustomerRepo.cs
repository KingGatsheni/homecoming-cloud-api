using homecoming.api.Abstraction;
using homecoming.api.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homecoming.api.Repo
{
    public class CustomerRepo : IRepository<Customer>
    {
        HomecomingDbContext db;
        public CustomerRepo(HomecomingDbContext context)
        {
            db = context;
        }
        public void Add(Customer Params)
        {
           if(Params != null)
            {
                Customer customer = new Customer()
                {
                    AspUserId = Params.AspUserId,
                    FirstName = Params.FirstName,
                    LastName = Params.LastName,
                    Cell_No = Params.Cell_No,
                    Email = Params.Email,
                    Dob = Params.Dob,
                    CreatedAt = DateTime.Now,
                    UpdatedOn = null
                };

                db.Customers.Add(customer);
                db.SaveChanges();
            }
        }

        public List<Customer> FindAll()
        {
           return db.Customers.ToList();
        }

        public Customer GetById(int id)
        {
            return db.Customers.SingleOrDefault(o => o.CustomerId.Equals(id));
        }
        public Customer GetUserById(string userId)
        {
            var user = db.Customers.SingleOrDefault(o => o.AspUserId.Equals(userId));
            if(user != null)
            {
                return user;
            }
            return null;
        }

        public void RemoveById(int id)
        {
            Customer customer = db.Customers.SingleOrDefault(o => o.CustomerId.Equals(id));
            db.Customers.Remove(customer);
            db.SaveChanges();
        }

        public void Update(int id, Customer Params)
        {
            if(Params != null)
            {
                Customer cust = db.Customers.SingleOrDefault(o => o.CustomerId.Equals(id));
                cust.FirstName = Params.FirstName;
                cust.LastName = Params.LastName;
                cust.Cell_No = Params.Cell_No;
                cust.Email = Params.Email;
                cust.Dob = Params.Dob;
                cust.UpdatedOn = null;

                db.Customers.Attach(cust);
                db.Entry(cust).State = EntityState.Modified;
                db.SaveChanges();
            }
           
        }
    }
}
