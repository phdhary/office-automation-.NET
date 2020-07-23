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
    public class JenisKontrakController : ApiController
    {
        public readonly IJenisKontrakBL _jenisKontrakBL;

        public JenisKontrakController(IJenisKontrakBL jenisKontrakBL)
        {
            _jenisKontrakBL = jenisKontrakBL;
        }

        [HttpPost]
        public IHttpActionResult Add(JenisKontrakModel jenisKontrak)
        {
            try
            {
                var result = _jenisKontrakBL.Add(jenisKontrak);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult Update(JenisKontrakModel jenisKontrak)
        {
            try
            {
                var result = _jenisKontrakBL.Update(jenisKontrak);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(string jenisKontrakID)
        {
            var jbtn = new JenisKontrakModel { JenisKontrakID = jenisKontrakID };
            try
            {
                _jenisKontrakBL.Delete(jbtn);
                return Ok("Data has been deleted");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetData(string jenisKontrakID)
        {
            var jbtn = new JenisKontrakModel { JenisKontrakID = jenisKontrakID };
            try
            {
                var result = _jenisKontrakBL.GetData(jbtn);
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
                var result = _jenisKontrakBL.ListData();
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
