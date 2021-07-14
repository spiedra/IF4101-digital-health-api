using IF4101_proyecto3_web.Data;
using IF4101_proyecto3_web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IF4101_proyecto3_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        // GET: api/<AppointmentController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<AppointmentController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<AppointmentController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        //// PUT api/<AppointmentController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<AppointmentController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

        [HttpPost]
        [Route("GetAppointmentByCard")]
        public IActionResult GetAppointmentByCard(string paitentCardId)
        {
            ConnectionDb connectionDb = new();
            return Ok(this.ExcGetAppointmetsByCard(connectionDb, paitentCardId));
        }

        private List<AppointmentViewModel> ExcGetAppointmetsByCard(ConnectionDb connectionDb, string PaitentCardId)
        {
            string paramIdCard = "@param_ID_CARD"
          , commandText = "ADMINISTRATOR.sp_GET_APPOINTMENTS_BY_CARD";
            connectionDb.InitSqlComponents(commandText);
            connectionDb.CreateParameter(paramIdCard, SqlDbType.VarChar, PaitentCardId);
            connectionDb.ExcecuteReader();
            return this.ReadGetAppointmetsByCard(connectionDb);
        }

        private List<AppointmentViewModel> ReadGetAppointmetsByCard(ConnectionDb connectionDb)
        {
            List<AppointmentViewModel> list = new List<AppointmentViewModel>();
            while (connectionDb.SqlDataReader.Read())
            {
                list.Add(new()
                {
                    PatientName = connectionDb.SqlDataReader.GetString(0),
                    Date = connectionDb.SqlDataReader.GetDateTime(1),
                    HealthCenter = connectionDb.SqlDataReader.GetString(2),
                    SpecialityType = connectionDb.SqlDataReader.GetString(3),
                    AppointmentId = connectionDb.SqlDataReader.GetInt32(4),
                    Description = this.CheckIsNull(connectionDb)
                });
            }
            connectionDb.SqlConnection.Close();
            return list;
        }

        private string CheckIsNull(ConnectionDb connectionDb)
        {
            if (!connectionDb.SqlDataReader.IsDBNull(5))
            {
                return connectionDb.SqlDataReader.GetString(5);
            }
            return "Pending appointment";
        }
    }
}