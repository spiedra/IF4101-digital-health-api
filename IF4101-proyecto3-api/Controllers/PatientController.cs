using IF4101_proyecto3_api.Models;
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
        public IActionResult SignInPatient([FromBody] PatientModel patient)
        {
            ConnectionDb connectionDb = new();
            ExcRegisterPatient(connectionDb, patient);
            if (CommonMethod.ReadParameterReturn(connectionDb))
            {
                return Ok("Patient successfully registered");
            }
            return Ok("User already exist");
        }

        private static void ExcRegisterPatient(ConnectionDb connectionDb, PatientModel patient)
        {
            string paramIdCard = "@param_ID_CARD"
             , paramName = "@param_NAME"
             , paramLastName = "@param_LAST_NAME"
             , paramPassword = "@param_PASSWORD"
             , paramAge = "@param_AGE"
             , paramBloodType = "@param_BLOOD_TYPE"
             , paramCivilStatus = "@param_CIVIL_STATUS"
             , paramAddress = "@param_ADDRESS"
             , paramPhoneNumber1 = "@param_PHONE_NUMBER_1"
             , paramPhoneNumber2 = "@param_PHONE_NUMBER_2"
             , commandText = "PATIENT.sp_REGISTER_PATIENT";
            connectionDb.InitSqlComponents(commandText);
            connectionDb.CreateParameter(paramIdCard, SqlDbType.VarChar, patient.IdCard);
            connectionDb.CreateParameter(paramName, SqlDbType.VarChar, patient.Name);
            connectionDb.CreateParameter(paramLastName, SqlDbType.VarChar, patient.LastName);
            connectionDb.CreateParameter(paramPassword, SqlDbType.VarChar, patient.Password);
            connectionDb.CreateParameter(paramAge, SqlDbType.Int, patient.Age);
            connectionDb.CreateParameter(paramBloodType, SqlDbType.VarChar, patient.BloodType);
            connectionDb.CreateParameter(paramCivilStatus, SqlDbType.VarChar, patient.CivilStatus);
            connectionDb.CreateParameter(paramAddress, SqlDbType.VarChar, patient.Address);
            connectionDb.CreateParameter(paramAddress, SqlDbType.VarChar, paramPhoneNumber1);
            connectionDb.CreateParameter(paramAddress, SqlDbType.VarChar, paramPhoneNumber2);
            connectionDb.CreateParameterOutput();
            connectionDb.ExcecuteReader();
        }
    }
}
