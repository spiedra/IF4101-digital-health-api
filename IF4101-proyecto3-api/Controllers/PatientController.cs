using IF4101_proyecto3_api.Models;
using IF4101_proyecto3_api.Utility;
using IF4101_proyecto3_web.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [HttpPost]
        [Route("SignIn")]
        public IActionResult GetPatientPersonalInformation(string idCard)
        {
            ConnectionDb connectionDb = new();
            ExcGetPatientPersonalInformation(connectionDb, idCard);
            return Ok(ReadGetPatientPersonalInformation(connectionDb));
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

        private static void ExcGetPatientPersonalInformation(ConnectionDb connectionDb, string idCard)
        {
            string paramIdCard = "@param_ID_CARD"
                , commandText = "PATIENT.sp_REGISTER_PATIENT";
            connectionDb.InitSqlComponents(commandText);
            connectionDb.CreateParameter(paramIdCard, SqlDbType.VarChar, idCard);
            connectionDb.ExcecuteReader();
        }

        private static List<PatientModel> ReadGetPatientPersonalInformation(ConnectionDb connectionDb)
        {
            List<PatientModel> list = new List<PatientModel>();
            while (connectionDb.SqlDataReader.Read())
            {
                list.Add(new()
                {
                    IdCard = connectionDb.SqlDataReader.GetString(0),
                    Name = connectionDb.SqlDataReader.GetString(1),
                    LastName = connectionDb.SqlDataReader.GetString(2),
                    Age = connectionDb.SqlDataReader.GetInt32(3),
                    BloodType = connectionDb.SqlDataReader.GetString(4),
                    CivilStatus = connectionDb.SqlDataReader.GetString(5),
                    Address = connectionDb.SqlDataReader.GetString(6),
                    PhoneNumber1 = connectionDb.SqlDataReader.GetString(7),
                    PhoneNumber2 = connectionDb.SqlDataReader.GetString(8),
                });
            }
            connectionDb.SqlConnection.Close();
            return list;
        }
    }
}
