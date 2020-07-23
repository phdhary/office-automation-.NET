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
    public class JenisCutiController : ApiController
    {
        public readonly IJenisCutiBL _jenisCutiBL;

        public JenisCutiController(IJenisCutiBL jenisCutiBL)
        {
            _jenisCutiBL = jenisCutiBL;
        }

        [HttpPost]
        public IHttpActionResult Add(JenisCutiModel jenisCuti)
        {
            try
            {
                var result = _jenisCutiBL.Add(jenisCuti);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult Update(JenisCutiModel jenisCuti)
        {
            try
            {
                var result = _jenisCutiBL.Update(jenisCuti);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(string jenisCutiID)
        {
            var jbtn = new JenisCutiModel { JenisCutiID = jenisCutiID };
            try
            {
                _jenisCutiBL.Delete(jbtn);
                return Ok("Data has been deleted");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetData(string jenisCutiID)
        {
            var jbtn = new JenisCutiModel { JenisCutiID = jenisCutiID };
            try
            {
                var result = _jenisCutiBL.GetData(jbtn);
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
                var result = _jenisCutiBL.ListData();
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
