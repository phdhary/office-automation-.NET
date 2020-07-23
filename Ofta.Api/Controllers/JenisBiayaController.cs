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
    public class JenisBiayaController : ApiController
    {
        public readonly IJenisBiayaBL _jenisBiayaBL;

        public JenisBiayaController(IJenisBiayaBL jenisBiayaBL)
        {
            _jenisBiayaBL = jenisBiayaBL;
        }

        [HttpPost]
        public IHttpActionResult Add(JenisBiayaModel jenisBiaya)
        {
            try
            {
                var result = _jenisBiayaBL.Add(jenisBiaya);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult Update(JenisBiayaModel jenisBiaya)
        {
            try
            {
                var result = _jenisBiayaBL.Update(jenisBiaya);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(string jenisBiayaID)
        {
            var jbtn = new JenisBiayaModel { JenisBiayaID = jenisBiayaID };
            try
            {
                _jenisBiayaBL.Delete(jbtn);
                return Ok("Data has been deleted");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetData(string jenisBiayaID)
        {
            var jbtn = new JenisBiayaModel { JenisBiayaID = jenisBiayaID };
            try
            {
                var result = _jenisBiayaBL.GetData(jbtn);
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
                var result = _jenisBiayaBL.ListData();
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
