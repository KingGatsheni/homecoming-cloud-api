using homecoming.api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homecoming.api.Abstraction
{
    public interface IRepository<T>
    {
        public void Add(T Params);
        public List<T> FindAll();
        public T GetById(int id);
        public void RemoveById(int id);
        public void Update(int id, T Params);
    }
}
