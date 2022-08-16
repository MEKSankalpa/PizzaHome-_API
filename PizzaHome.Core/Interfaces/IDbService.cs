using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHome.Core.Interfaces
{
    public interface IDbService
    {
        public Task<List<T>> GetAll<T>(string command, object param);
        public Task<T> Get<T>(string command, object param);
        public Task<int> CreateAndEdit(string command, object param);
    }
}
