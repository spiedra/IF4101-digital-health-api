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
        public IActionResult ValidateInputLogIn(string patientIdCard, string paitientPassword)
        {
            ConnectionDb connectionDb = new();
            ExcValidateLogIn(connectionDb, patientIdCard, paitientPassword);
            if (CommonMethod.ReadParameterReturn(connectionDb))
            {
                return Ok();
            }
            return NotFound();
        }

        private void ExcValidateLogIn(ConnectionDb connectionDb,
                                      string patientIdCard,
                                      string patientPassword)
        {
            string paramId = "@param_ID_CARD"
             , paramPassword = "@param_PASSWORD"
             , commandText = "PATIENT.sp_VALIDATE_PATIENT_LOG_IN";
            connectionDb.InitSqlComponents(commandText);
            connectionDb.CreateParameter(paramId, SqlDbType.VarChar, patientIdCard);
            connectionDb.CreateParameter(paramPassword, SqlDbType.VarChar, patientPassword);
            connectionDb.CreateParameterOutput();
            connectionDb.ExcecuteReader();
        }
    }
}
