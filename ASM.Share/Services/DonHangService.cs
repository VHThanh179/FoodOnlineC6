using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASM.Share.Interfaces;
using ASM.Share.Models;
using Microsoft.EntityFrameworkCore;

namespace ASM.Share.Services
{
    public class DonHangService : IDonHangService
    {
        protected DataContext _context;
        public DonHangService(DataContext context)
        {
            _context = context;
        }
        public async Task<int> AddDonhangAsync(DonHang donhang)
        {
            int ret = 0;
            try
            {
                _context.Add(donhang);
                await _context.SaveChangesAsync();
                //_context.SaveChanges();
                ret = donhang.DonHangID;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }
        public List<DonHang> GetDonHangAll()
        {
            List<DonHang> list = new List<DonHang>();
            list = _context.DonHangs.OrderByDescending(h => h.OrderDay)
                    .Include(h => h.KhachHang)
                    .Include(h => h.DonHangChiTiets).ToList();
            return list;
        }

        public List<DonHang> GetDonHangByKhach(int KHId)
        {
            List<DonHang> list = new List<DonHang>();
            list = _context.DonHangs.Where(h => h.KhachHangID == KHId).OrderByDescending(h => h.OrderDay)
                    .Include(h => h.KhachHang)
                    .Include(h => h.DonHangChiTiets).ToList();
            return list;
        }

        public async Task<List<DonHang>> GetDonhangbyKhachhangAsync(int khachhangId)
        {
            List<DonHang> list = new List<DonHang>();
            // sử dụng kỹ thuật loading Eager // từ khóa Include
            list = await _context.DonHangs.Where(x => x.KhachHangID == khachhangId).OrderByDescending(x => x.OrderDay)
                .Include(x => x.KhachHang)
                .Include(x => x.DonHangChiTiets)
                .ToListAsync();
            return list;
        }

        public DonHang GetDonHang(int id)
        {
            DonHang donHang = null;
            donHang = _context.DonHangs.Where(h => h.DonHangID == id)
                    .Include(h => h.KhachHang)
                    .Include(h => h.DonHangChiTiets).ThenInclude(y => y.MonAn)
                    .FirstOrDefault(); ;
            return donHang;
        }

        public async Task<DonHang> GetDonHangAsync(int id)
        {
            DonHang donHang = null;
            donHang = await _context.DonHangs.Where(h => h.DonHangID == id)
                    .Include(h => h.KhachHang)
                    .Include(h => h.DonHangChiTiets).ThenInclude(y => y.MonAn)
                    .FirstOrDefaultAsync(); ;
            return donHang;
        }

        public int AddDonHang(DonHang donHang)
        {
            int ret = 0;
            try
            {
                _context.Add(donHang);
                _context.SaveChanges();
                ret = donHang.DonHangID;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }
        public int EditDonHang(int id, DonHang donHang)
        {
            int ret = 0;
            try
            {
                _context.Update(donHang);
                _context.SaveChanges();
                ret = donHang.DonHangID;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }
    }
}
