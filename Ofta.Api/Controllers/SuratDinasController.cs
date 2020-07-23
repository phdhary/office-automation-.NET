using Ofta.Lib.BL;
using Ofta.Lib.Dto;
using Ofta.Lib.Helper;
using Ofta.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web.Http;

namespace Ofta.Api.Controllers
{
    public class SuratDinasController : ApiController
    {
        private readonly ISuratDinasBL _suratDinasBL;

        public SuratDinasController(ISuratDinasBL suratDinasBL)
        {
            _suratDinasBL = suratDinasBL;
        }

        public IHttpActionResult CreateSuratDinas(SuratDinasAddDto surat)
        {
            try
            {
                var result = _suratDinasBL.Propose(surat);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Update(SuratDinasModel surat)
        {
            try
            {
                var result = _suratDinasBL.Revise(surat);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Delete(string suratDinasID)
        {
            var surat = new SuratDinasModel(suratDinasID);
            try
            {
                _suratDinasBL.Void(surat);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public IHttpActionResult GetData(string suratDinasID)
        {
            var surat = new SuratDinasModel(suratDinasID);
            try
            {
                var result = _suratDinasBL.GetData(surat);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public IHttpActionResult ListData(string tglYmd1, string tglYmd2)
        {
            var tgl1 = tglYmd1.ToDate();
            var tgl2 = tglYmd2.ToDate();
            try
            {
                var result = _suratDinasBL.ListData(tgl1, tgl2);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }


    }
}
