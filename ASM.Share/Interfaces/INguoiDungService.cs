using ASM.Share.Models;
using ASM.Share.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASM.Share.Interfaces
{
    public interface INguoiDungService
    {
        List<NguoiDung> GetNguoiDungAll();
        NguoiDung GetNguoiDung(int id);
        int AddNguoiDung(NguoiDung nguoiDung);
        int EditNguoiDung(int id, NguoiDung nguoiDung);
        public NguoiDung Login(ViewLogin login);
    }
}
