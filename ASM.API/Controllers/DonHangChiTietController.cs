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
    public class DonHangChiTietController : ControllerBase
    {
        private readonly IDonHangChiTietService _donHangChiTietService;
        private readonly IDonHangService _donHangService;

        public DonHangChiTietController(IDonHangChiTietService donHangChiTietService, IDonHangService donHangService)
        {
            _donHangChiTietService = donHangChiTietService;
            _donHangService = donHangService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DonHang>>> GetDonHang([FromQuery] int id)
        {
            var donhang = await _donHangService.GetDonHangAsync(id);
            List<DonHang> list = new List<DonHang>();
            list.Add(donhang);
            return list;
        }
    }
}
