using ASM.Share.Interfaces;
using ASM.Share.Models;
using ASM.Share.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASM.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IDonHangService _donHangService;
        private readonly IDonHangChiTietService _donHangChiTietService;

        public CartController(IDonHangChiTietService donHangChiTietService, IDonHangService donHangService)
        {
            _donHangChiTietService = donHangChiTietService;
            _donHangService = donHangService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> PostCart(Cart giohang)
        {
            try
            {
                var donhang = new DonHang()
                {
                    Status = TrangThaiDonHang.Moidat,
                    KhachHangID = giohang.KhanghangId,
                    Total = giohang.Tongtien,
                    OrderDay = DateTime.Now,
                    Note = ""
                };

                int donhangid = await _donHangService.AddDonhangAsync(donhang);
                donhang.DonHangID = donhangid;

                List<CartItem> dataCart = giohang.ListViewCart;
                for (int i = 0; i < dataCart.Count; i++)
                {
                    DonHangChiTiet chiTiet = new DonHangChiTiet()
                    {
                        DonHangID = donhangid,
                        MonAnID = dataCart[i].MonAn.MonAnID,
                        Quantity = dataCart[i].Quantity,
                        Price = dataCart[i].MonAn.Price * dataCart[i].Quantity,
                        Note = ""
                    };
                    _donHangChiTietService.AddDonHangChiTiet(chiTiet);
                }
            }
            catch
            {
                return BadRequest(-1);
            }
            return Ok(1);
        }
    }
}
