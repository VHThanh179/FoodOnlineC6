using ASM.Share.Models;
using ASM.Share.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASM.Share.Interfaces
{
    public interface IKhachHangService
    {
        List<KhachHang> GetKhachHangAll();
        KhachHang GetKhachHang(int id);
        int AddKhachHang(KhachHang khachHang);
        int EditKhachHang(int id, KhachHang khachHang);
        KhachHang Login(ViewWebLogin viewWebLogin);
        Task<int> AddKhachHangAsync(KhachHang khachHang);
        Task<KhachHang> LoginAsync(ViewWebLogin viewWebLogin);
        Task<KhachHang> GetKhachHangAsync(int id);
        Task<int> EditKhachHangAsync(int id, KhachHang khachHang);
    }
}
