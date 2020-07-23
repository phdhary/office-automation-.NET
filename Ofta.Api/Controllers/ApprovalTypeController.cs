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
    public class ApprovalTypeController : ApiController
    {
        public readonly IApprovalTypeBL _approvalTypeBL;

        public ApprovalTypeController(IApprovalTypeBL approvalTypeBL)
        {
            _approvalTypeBL = approvalTypeBL;
        }

        [HttpPost]
        public IHttpActionResult Add(ApprovalTypeModel approvalType)
        {
            try
            {
                var result = _approvalTypeBL.Add(approvalType);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult Update(ApprovalTypeModel approvalType)
        {
            try
            {
                var result = _approvalTypeBL.Update(approvalType);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(string approvalTypeID)
        {
            var jbtn = new ApprovalTypeModel { ApprovalTypeID = approvalTypeID };
            try
            {
                _approvalTypeBL.Delete(jbtn);
                return Ok("Data has been deleted");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetData(string approvalTypeID)
        {
            var jbtn = new ApprovalTypeModel { ApprovalTypeID = approvalTypeID };
            try
            {
                var result = _approvalTypeBL.GetData(jbtn);
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
                var result = _approvalTypeBL.ListData();
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
