using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASM.Share.Interfaces;
using ASM.Share.Models;
using ASM.Share.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ASM.Share.Services
{
    public class KhachHangService : IKhachHangService
    {
        protected DataContext _context;
        protected IMaHoaHelper _maHoaHelper;
        public KhachHangService(DataContext context, IMaHoaHelper maHoaHelper)
        {
            _context = context;
            _maHoaHelper = maHoaHelper;
        }
        public List<KhachHang> GetKhachHangAll()
        {
            List<KhachHang> list = new List<KhachHang>();
            list = _context.KhachHangs.ToList();
            return list;
        }
        public KhachHang GetKhachHang(int id)
        {
            KhachHang khachHang = null;
            khachHang = _context.KhachHangs.Find(id);
            return khachHang;
        }

        public async Task<KhachHang> GetKhachHangAsync(int id)
        {
            KhachHang khachHang = null;
            khachHang = await _context.KhachHangs.FindAsync(id);
            return khachHang;
        }

        public int AddKhachHang(KhachHang khachHang)
        {
            int ret = 0;
            try
            {
                khachHang.Password = _maHoaHelper.Mahoa(khachHang.Password);
                khachHang.ConfirmPassword = khachHang.Password;
                _context.Add(khachHang);
                _context.SaveChanges();
                ret = khachHang.KhachHangID;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }

        public async Task<int> AddKhachHangAsync(KhachHang khachHang)
        {
            int ret = 0;
            try
            {
                khachHang.Password = _maHoaHelper.Mahoa(khachHang.Password);
                khachHang.ConfirmPassword = khachHang.Password;
                _context.Add(khachHang);
                await _context.SaveChangesAsync();
                ret = khachHang.KhachHangID;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }

        public int EditKhachHang(int id, KhachHang khachHang)
        {
            int ret = 0;
            try
            {
                KhachHang _kh = null;
                _kh = _context.KhachHangs.Find(id);

                _kh.EmailKH = khachHang.EmailKH;
                _kh.FullName = khachHang.FullName;
                _kh.BirthDay = khachHang.BirthDay;
                _kh.PhoneNumber = khachHang.PhoneNumber;
                _kh.Address = khachHang.Address;
                if (khachHang.Password != null)
                {
                    khachHang.Password = _maHoaHelper.Mahoa(khachHang.Password);
                    _kh.Password = khachHang.Password;
                    _kh.ConfirmPassword = khachHang.Password;
                }
                _kh.Describe = khachHang.Describe;

                _context.Update(_kh);
                _context.SaveChanges();
                ret = _kh.KhachHangID;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }

        public async Task<int> EditKhachHangAsync(int id, KhachHang khachHang)
        {
            int ret = 0;
            try
            {
                KhachHang _kh = null;
                _kh = _context.KhachHangs.Find(id);

                _kh.EmailKH = khachHang.EmailKH;
                _kh.FullName = khachHang.FullName;
                _kh.BirthDay = khachHang.BirthDay;
                _kh.PhoneNumber = khachHang.PhoneNumber;
                _kh.Address = khachHang.Address;
                if (khachHang.Password != null && khachHang.Password != string.Empty)
                {
                    khachHang.Password = _maHoaHelper.Mahoa(khachHang.Password);
                    _kh.Password = khachHang.Password;
                    _kh.ConfirmPassword = khachHang.Password;
                }
                _kh.Describe = khachHang.Describe;

                _context.Update(_kh);
                await _context.SaveChangesAsync();
                ret = _kh.KhachHangID;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }

        public async Task<KhachHang> LoginAsync(ViewWebLogin viewWebLogin)
        {
            var u = await _context.KhachHangs.Where(p => p.EmailKH.Equals(viewWebLogin.Email)
                    && p.Password.Equals(_maHoaHelper.Mahoa(viewWebLogin.Password))
                    ).FirstOrDefaultAsync();
            return u;
        }

        public KhachHang Login(ViewWebLogin viewWebLogin)
        {
            var u = _context.KhachHangs.Where(p => p.EmailKH.Equals(viewWebLogin.Email)
                    && p.Password.Equals(_maHoaHelper.Mahoa(viewWebLogin.Password))
                    ).FirstOrDefault();
            return u;
        }
    }
}
