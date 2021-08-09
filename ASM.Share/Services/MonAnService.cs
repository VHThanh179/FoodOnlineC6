using ASM.Share.Interfaces;
using ASM.Share.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASM.Share.Services
{
    public class MonAnService : IMonAnService
    {
        protected DataContext _context;
        public MonAnService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<MonAn>> GetMonAnAllAsync()
        {
            List<MonAn> list = await _context.MonAns.ToListAsync();
            return list;
        }
        public List<MonAn> GetMonAnByName(string name)
        {
            List<MonAn> list = new List<MonAn>();
            list = _context.MonAns.Where(x => x.Name.Contains(name.Trim())).ToList();
            return list;
        }
        public async Task<List<MonAn>> GetMonAnByNameAsync(string name)
        {
            List<MonAn> list = await _context.MonAns.Where(x => x.Name.Contains(name.Trim())).ToListAsync();
            return list;
        }


        public List<MonAn> GetMonAnAll()
        {
            List<MonAn> list = new List<MonAn>();
            list = _context.MonAns.ToList();
            return list;
        }

        public MonAn GetMonAn(int id)
        {
            MonAn monAn = null;
            monAn = _context.MonAns.Find(id); //cách này chỉ dùng cho Khóa chính
            //product = _context.Products.Where(e=>e.Id==id).FirstOrDefault(); //cách tổng quát
            return monAn;
        }

        public int AddMonAn(MonAn monAn)
        {
            int ret = 0;
            try
            {
                _context.Add(monAn);
                _context.SaveChanges();
                ret = monAn.MonAnID;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }

        public int EditMonAn(int id, MonAn monAn)
        {
            int ret = 0;
            try
            {
                MonAn _monAn = null;
                _monAn = _context.MonAns.Find(id); //cách này chỉ dùng cho Khóa chính

                _monAn.Name = monAn.Name;
                _monAn.Price = monAn.Price;
                _monAn.Type = monAn.Type;
                _monAn.Picture = monAn.Picture;
                _monAn.Describe = monAn.Describe;
                _monAn.Status = monAn.Status;

                _context.Update(_monAn);
                _context.SaveChanges();
                ret = monAn.MonAnID;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }

    }
}
