using ASM.Share.Interfaces;
using ASM.Share.Models;
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
    public class DonHangController : ControllerBase
    {
        private readonly IDonHangService _donHangService;
        public DonHangController(IDonHangService donHangService)
        {
            _donHangService = donHangService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DonHang>>> GetDonHangByKhachHang([FromQuery]int id)
        {
            var listDonHang = await _donHangService.GetDonhangbyKhachhangAsync(id);
            return listDonHang;
        }
    }
}
