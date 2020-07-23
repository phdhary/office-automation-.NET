using Ofta.Lib.BL;
using Ofta.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ofta.Api.Controllers
{
    public class KotaController : ApiController
    {
        private IKotaBL _kotaBL;
        public KotaController(IKotaBL kotaBL)
        {
            _kotaBL = kotaBL;
        }

        [HttpPost]
        public IHttpActionResult Add(KotaModel kota)
        {
            try
            {
                var result = _kotaBL.Add(kota);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult Update(KotaModel kota)
        {
            try
            {
                var result = _kotaBL.Update(kota);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(string kotaID)
        {
            var kota = new KotaModel { KotaID = kotaID };
            try
            {
                _kotaBL.Delete(kota);
                return Ok("Data has been deleted");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetData(string kotaID)
        {
            var kota = new KotaModel { KotaID = kotaID };
            try
            {
                var result = _kotaBL.GetData(kota);
                if (result is null)
                    return BadRequest("Data Not Found");
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult ListData()
        {
            try
            {
                var result = _kotaBL.ListData();
                if (result is null)
                    return BadRequest("Data Not Found");
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
