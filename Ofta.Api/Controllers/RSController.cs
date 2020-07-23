using Ofta.Lib.BL;
using Ofta.Lib.Dto;
using Ofta.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ofta.Api.Controllers
{
    public class RSController : ApiController
    {
        public readonly IRSBL _rsBL;

        public RSController(IRSBL rsBL)
        {
            _rsBL = rsBL;
        }

        [HttpPost]
        public IHttpActionResult Add(RSAddDto rs)
        {
            try
            {
                var result = _rsBL.Save(rs);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(string rsID)
        {
            var jbtn = new RSModel { RSID = rsID };
            try
            {
                _rsBL.Delete(jbtn);
                return Ok("Data has been deleted");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetData(string rsID)
        {
            var jbtn = new RSModel { RSID = rsID };
            try
            {
                var result = _rsBL.GetData(jbtn);
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
                var result = _rsBL.ListData();
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
