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
    public class JabatanController : ApiController
    {
        public readonly IJabatanBL _jabatanBL;

        public JabatanController(IJabatanBL jabatanBL)
        {
            _jabatanBL = jabatanBL;
        }

        [HttpPost]
        public IHttpActionResult Add(JabatanModel jabatan)
        {
            try
            {
                var result = _jabatanBL.Add(jabatan);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult Update(JabatanModel jabatan)
        {
            try
            {
                var result = _jabatanBL.Update(jabatan);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(string jabatanID)
        {
            var jbtn = new JabatanModel { JabatanID = jabatanID };
            try
            {
                _jabatanBL.Delete(jbtn);
                return Ok("Data has been deleted");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetData(string jabatanID)
        {
            var jbtn = new JabatanModel { JabatanID = jabatanID };
            try
            {
                var result = _jabatanBL.GetData(jbtn);
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
                var result = _jabatanBL.ListData();
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
