using ASM.Share.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASM.Share.Interfaces
{
    public interface IDonHangService
    {
        List<DonHang> GetDonHangAll();
        List<DonHang> GetDonHangByKhach(int KHId);
        DonHang GetDonHang(int id);
        int AddDonHang(DonHang donHang);
        int EditDonHang(int id, DonHang donHang);
        Task<int> AddDonhangAsync(DonHang donhang);
        Task<List<DonHang>> GetDonhangbyKhachhangAsync(int khachhangId);
        Task<DonHang> GetDonHangAsync(int id);
    }
}
