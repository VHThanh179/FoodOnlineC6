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
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangController : ControllerBase
    {
        private readonly IKhachHangService _khachHangService;

        public KhachHangController(IKhachHangService khachHangService)
        {
            _khachHangService = khachHangService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> PostKhachHang(KhachHang khachHang)
        {
            try
            {
                int id = await _khachHangService.AddKhachHangAsync(khachHang);
                khachHang.KhachHangID = id;
            }
            catch
            {

            }
            return Ok(1);
        }

        [HttpGet]
        public async Task<ActionResult<KhachHang>> GetKhachHang([FromQuery] int id)
        {
            KhachHang khachHang = await _khachHangService.GetKhachHangAsync(id);
            return khachHang;
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> EditKhachHang(int id, KhachHang khachHang)
        {
            try
            {
                await _khachHangService.EditKhachHangAsync(id, khachHang);
            }
            catch
            {
                return BadRequest();
            }
            return Ok(1);
        }
    }
}
