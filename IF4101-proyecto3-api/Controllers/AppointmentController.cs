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
        [HttpGet]
        [Route("GetAppointmentByCard")]
        public IActionResult GetAppointmentByCard(string patientCardId)
        {
            ConnectionDb connectionDb = new();
            return Ok(this.ExcGetAppointmetsByCard(connectionDb, patientCardId));
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