using ASM.Share.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASM.Share.Interfaces
{
    public interface IMonAnService
    {
        Task<List<MonAn>> GetMonAnAllAsync();
        List<MonAn> GetMonAnAll();
        public MonAn GetMonAn(int id);
        public int AddMonAn(MonAn monAn);
        public int EditMonAn(int id, MonAn monAn);
        List<MonAn> GetMonAnByName(string name);
        Task<List<MonAn>> GetMonAnByNameAsync(string name);
    }
}
