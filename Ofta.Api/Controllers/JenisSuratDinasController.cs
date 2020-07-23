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
    public class JenisSuratDinasController : ApiController
    {
        public readonly IJenisSuratDinasBL _jenisSuratDinasBL;

        public JenisSuratDinasController(IJenisSuratDinasBL jenisSuratDinasBL)
        {
            _jenisSuratDinasBL = jenisSuratDinasBL;
        }

        [HttpPost]
        public IHttpActionResult Add(JenisSuratDinasModel jenisSuratDinas)
        {
            try
            {
                var result = _jenisSuratDinasBL.Add(jenisSuratDinas);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult Update(JenisSuratDinasModel jenisSuratDinas)
        {
            try
            {
                var result = _jenisSuratDinasBL.Update(jenisSuratDinas);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(string jenisSuratDinasID)
        {
            var jbtn = new JenisSuratDinasModel { JenisSuratDinasID = jenisSuratDinasID };
            try
            {
                _jenisSuratDinasBL.Delete(jbtn);
                return Ok("Data has been deleted");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetData(string jenisSuratDinasID)
        {
            var jbtn = new JenisSuratDinasModel { JenisSuratDinasID = jenisSuratDinasID };
            try
            {
                var result = _jenisSuratDinasBL.GetData(jbtn);
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
                var result = _jenisSuratDinasBL.ListData();
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
