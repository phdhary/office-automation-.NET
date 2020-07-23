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
    public class JenisArsipController : ApiController
    {
        public readonly IJenisArsipBL _jenisArsipBL;

        public JenisArsipController(IJenisArsipBL jenisArsipBL)
        {
            _jenisArsipBL = jenisArsipBL;
        }

        [HttpPost]
        public IHttpActionResult Add(JenisArsipModel jenisArsip)
        {
            try
            {
                var result = _jenisArsipBL.Add(jenisArsip);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult Update(JenisArsipModel jenisArsip)
        {
            try
            {
                var result = _jenisArsipBL.Update(jenisArsip);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(string jenisArsipID)
        {
            var jbtn = new JenisArsipModel { JenisArsipID = jenisArsipID };
            try
            {
                _jenisArsipBL.Delete(jbtn);
                return Ok("Data has been deleted");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetData(string jenisArsipID)
        {
            var jbtn = new JenisArsipModel { JenisArsipID = jenisArsipID };
            try
            {
                var result = _jenisArsipBL.GetData(jbtn);
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
                var result = _jenisArsipBL.ListData();
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
