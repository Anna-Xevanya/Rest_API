using Microsoft.AspNetCore.Mvc;
using Rest_API.Context;
using Rest_API.Models;

namespace Rest_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AkademikController : ControllerBase
    {
        private readonly string _constr;
        public AkademikController(IConfiguration config) => _constr = config.GetConnectionString("webApiDatabase");

        [HttpPost("register")]
        public IActionResult Register([FromBody] Guru guru)
        {
            try
            {
                if (guru == null || string.IsNullOrEmpty(guru.username) || string.IsNullOrEmpty(guru.password))
                {
                    return BadRequest(new { success = false, message = "Data registrasi tidak lengkap" });
                }

                var context = new AkademikContext(_constr);
                context.RegisterGuru(guru);

                return StatusCode(201, new { success = true, message = "Registrasi guru berhasil" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Guru g)
        {
            bool isValid = new AkademikContext(_constr).LoginGuru(g.username, g.password);
            if (!isValid) return Unauthorized(new ApiResponse<object> { Success = false, Message = "Username/Password salah" });
            return Ok(new ApiResponse<object> { Success = true, Message = "Login Berhasil" });
        }

        [HttpGet("siswa")]
        public IActionResult GetSiswa()
        {
            var data = new AkademikContext(_constr).ListSiswa();
            return Ok(new ApiResponse<List<Siswa>> { Success = true, Message = "Data Siswa", Data = data });
        }

        [HttpGet("siswa/{id}")]
        public IActionResult GetSiswaById(int id)
        {
            try
            {
                var context = new AkademikContext(_constr);
                var data = context.GetSiswaById(id);

                // Validasi jika data tidak ditemukan
                if (data == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"Siswa dengan ID {id} tidak ditemukan",
                        Data = null
                    });
                }

                // Response jika data ditemukan
                return Ok(new ApiResponse<Siswa>
                {
                    Success = true,
                    Message = "Data siswa berhasil ditemukan",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                // Response jika terjadi error sistem
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "Terjadi kesalahan pada server: " + ex.Message,
                    Data = null
                });
            }
        }

            [HttpPost("siswa")]
        public IActionResult PostSiswa([FromBody] Siswa s)
        {
            new AkademikContext(_constr).AddSiswa(s);
            return StatusCode(201, new ApiResponse<object> { Success = true, Message = "Siswa ditambahkan" });
        }

        [HttpPut("siswa/{id}")]
        public IActionResult PutSiswa(int id, [FromBody] Siswa s)
        {
            new AkademikContext(_constr).UpdateSiswa(id, s);
            return Ok(new ApiResponse<object> { Success = true, Message = "Siswa diperbarui" });
        }

        [HttpDelete("siswa/{id}")]
        public IActionResult DeleteSiswa(int id)
        {
            new AkademikContext(_constr).DeleteSiswa(id);
            return Ok(new ApiResponse<object> { Success = true, Message = "Siswa dihapus" });
        }
    }
}