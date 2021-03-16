using CsvHelper;
using MemberClaimAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;

namespace MemberClaimAPI.Controllers
{
    [Route("api/memberclaims")]
    [ApiController]
    public class MemberClaimsController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MemberClaimsController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult MemberClaimsResult()
        {
            try
            {
                string contentrootpath = _webHostEnvironment.ContentRootPath;

                List<Member> members = new List<Member>();
                List<Claim> claims = new List<Claim>();

                //Parse Members Data
                using (var reader = new StreamReader(contentrootpath + "/InputData/Member.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
                {
                    members = csv.GetRecords<Member>().ToList();
                }

                //Parse Claims Data
                using (var reader = new StreamReader(contentrootpath + "/InputData/Claim.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
                {
                    claims = csv.GetRecords<Claim>().ToList();
                }

                //Get List of Members with Claims
                List<MemberClaims> memberClaims = new List<MemberClaims>();
                members.ForEach(m =>
                {
                    MemberClaims memberClaims1 = new MemberClaims
                    {
                        MemberID = m.MemberID,
                        FirstName = m.FirstName,
                        LastName = m.LastName,
                        EnrollmentDate = m.EnrollmentDate,
                        Claims = claims.Where(x => x.MemberID == m.MemberID).ToList()
                    };
                    memberClaims.Add(memberClaims1);
                });

                //Return JSON Response
                return Ok(memberClaims);
            }
            catch (CsvHelperException e)
            {
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
