using Microsoft.AspNetCore.Mvc;
using Rest_API.Context;
using Rest_API.Models;

namespace Rest_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MuridController : ControllerBase
    {
        private string __constr;

        public MuridController(IConfiguration configuration)
        {
            __constr = configuration.GetConnectionString("webApiDatabase");
        }

        // GET: api/murid
        [HttpGet]
        public ActionResult<List<Murid>> GetMurid()
        {
            MuridContext context = new MuridContext(this.__constr);
            return Ok(context.ListMurid());
        }

        // POST: api/murid
        [HttpPost]
        public IActionResult AddMurid([FromBody] Murid mrd)
        {
            MuridContext context = new MuridContext(this.__constr);
            context.AddMurid(mrd);
            return Ok(new { message = "Data murid berhasil ditambahkan" });
        }

        // PUT: api/murid/5
        [HttpPut("{id}")]
        public IActionResult UpdateMurid(int id, [FromBody] Murid mrd)
        {
            MuridContext context = new MuridContext(this.__constr);
            context.UpdateMurid(id, mrd);
            return Ok(new { message = "Data murid berhasil diperbarui" });
        }

        // DELETE: api/murid/5
        [HttpDelete("{id}")]
        public IActionResult DeleteMurid(int id)
        {
            MuridContext context = new MuridContext(this.__constr);
            context.DeleteMurid(id);
            return Ok(new { message = "Data murid berhasil dihapus" });
        }
    }
}
