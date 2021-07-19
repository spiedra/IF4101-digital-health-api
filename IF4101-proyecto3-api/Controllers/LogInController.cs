using IF4101_proyecto3_api.Models;
using IF4101_proyecto3_api.Utility;
using IF4101_proyecto3_web.Data;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq;

namespace IF4101_proyecto3_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        [HttpPost]
        [Route("LogIn")]
        public ActionResult ValidateInputLogIn([FromBody] PatientModel patient)
        {
            //ConnectionDb connectionDb = new();
            //ExcValidateLogIn(connectionDb, patient);
            //if (CommonMethod.ReadParameterReturn(connectionDb))
            //{
            //    return Ok("OK");
            //}
            return Ok("Patient not found");
        }

        private static void ExcValidateLogIn(ConnectionDb connectionDb, PatientModel patient)
        {
            string paramId = "@param_ID_CARD"
             , paramPassword = "@param_PASSWORD"
             , commandText = "PATIENT.sp_VALIDATE_PATIENT_LOG_IN";
            connectionDb.InitSqlComponents(commandText);
            connectionDb.CreateParameter(paramId, SqlDbType.VarChar, patient.IdCard);
            connectionDb.CreateParameter(paramPassword, SqlDbType.VarChar, patient.Password);
            connectionDb.CreateParameterOutput();
            connectionDb.ExcecuteReader();
        }
    }
}
