using IF4101_proyecto3_api.Utility;
using IF4101_proyecto3_web.Data;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace IF4101_proyecto3_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        [HttpPost]
        [Route("SignIn")]
        public IActionResult SignInPatient(string idCard, string name, string lastName, string password, int age,
                                          string bloodType, string civilStatus, string address)
        {
            ConnectionDb connectionDb = new();
            ExcRegisterPatient(connectionDb, idCard, name, lastName, password, age, bloodType, civilStatus, address);
            if (CommonMethod.ReadParameterReturn(connectionDb))
            {
                return Ok("Patient successfully registered");
            }
            return Ok("User already exist");
        }

        private void ExcRegisterPatient(ConnectionDb connectionDb, string idCard, string name, string lastName,
                                string password, int age, string bloodType, string civilStatus, string address)
        {
            string paramIdCard = "@param_ID_CARD"
             , paramName = "@param_NAME"
             , paramLastName = "@param_LAST_NAME"
             , paramPassword = "@param_PASSWORD"
             , paramAge = "@param_AGE"
             , paramBloodType = "@param_BLOOD_TYPE"
             , paramCivilStatus = "@param_CIVIL_STATUS"
             , paramAddress = "@@param_ADDRESS"
             , commandText = "PATIENT.sp_REGISTER_PATIENT";
            connectionDb.InitSqlComponents(commandText);
            connectionDb.CreateParameter(paramIdCard, SqlDbType.VarChar, idCard);
            connectionDb.CreateParameter(paramName, SqlDbType.VarChar, name);
            connectionDb.CreateParameter(paramLastName, SqlDbType.VarChar, lastName);
            connectionDb.CreateParameter(paramPassword, SqlDbType.VarChar, password);
            connectionDb.CreateParameter(paramAge, SqlDbType.VarChar, age);
            connectionDb.CreateParameter(paramBloodType, SqlDbType.VarChar, bloodType);
            connectionDb.CreateParameter(paramCivilStatus, SqlDbType.VarChar, civilStatus);
            connectionDb.CreateParameter(paramAddress, SqlDbType.VarChar, address);
            connectionDb.CreateParameterOutput();
            connectionDb.ExcecuteReader();
        }
    }
}
