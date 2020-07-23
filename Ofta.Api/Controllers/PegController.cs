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
    public class PegController : ApiController
    {
        public readonly IPegBL _pegBL;

        public PegController(IPegBL pegBL)
        {
            _pegBL = pegBL;
        }

        [HttpPost]
        public IHttpActionResult Add(PegAddDto peg)
        {
            try
            {
                var result = _pegBL.Add(peg);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult Update(PegModel peg)
        {
            try
            {
                var result = _pegBL.Update(peg);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(string pegID)
        {
            var jbtn = new PegModel { PegID = pegID };
            try
            {
                _pegBL.Delete(jbtn);
                return Ok("Data has been deleted");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetData(string pegID)
        {
            var jbtn = new PegModel { PegID = pegID };
            try
            {
                var result = _pegBL.GetData(jbtn);
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
                var result = _pegBL.ListData();
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
