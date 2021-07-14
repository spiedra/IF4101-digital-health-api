using IF4101_proyecto3_web.Data;
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
    public class LogInController : ControllerBase
    {
        [HttpPost]
        [Route("LogIn")]
        public IActionResult ValidateInputLogIn(string patientIdCard, string paitientPassword)
        {
            if (ModelState.IsValid)
            {
                ConnectionDb connectionDb = new();
                this.ExcValidateLogIn(connectionDb, patientIdCard, paitientPassword);
                if (this.ReadValidateLogIn(connectionDb))
                {
                    return Ok();
                }
            }
            return NotFound();
        }

        [HttpPost]
        [Route("SignIn")]
        public IActionResult SingIn(string patientIdCard, string paitientPassword)
        {
            if (ModelState.IsValid)
            {
                ConnectionDb connectionDb = new();
                this.ExcValidateLogIn(connectionDb, patientIdCard, paitientPassword);
                if (this.ReadValidateLogIn(connectionDb))
                {
                    return Ok();
                }
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

        private bool ReadValidateLogIn(ConnectionDb connectionDb)
        {
            if ((int)connectionDb.ParameterReturn.Value == 1)
                return true;

            connectionDb.SqlConnection.Close();
            return false;
        }
    }
}
