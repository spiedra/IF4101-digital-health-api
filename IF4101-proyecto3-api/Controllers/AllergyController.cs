using IF4101_proyecto3_web.Data;
using IF4101_proyecto3_web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IF4101_proyecto3_api.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class AllergyController : ControllerBase
    {
        [HttpGet]
        [Route("/GetPatientAllergies")]
        public IActionResult Get(string IdCard)
        {
            ConnectionDb connectionDb = new ConnectionDb();
            this.ExcListPatientAllergies(connectionDb, IdCard);
            List<AllergyViewModel> allergies = this.GetPatientAllergies(connectionDb);
            connectionDb.SqlConnection.Close();
            return Ok(allergies);
        }

        private void ExcListPatientAllergies(ConnectionDb connectionDb, string IdCard)
        {
            string paramId = "@param_ID_CARD", commandText = "PATIENT.sp_LIST_PATIENT_ALLERGIES";
            connectionDb.InitSqlComponents(commandText);
            connectionDb.CreateParameter(paramId, SqlDbType.VarChar, IdCard);
            connectionDb.ExcecuteReader();
        }
        private List<AllergyViewModel> GetPatientAllergies(ConnectionDb connectionDb)
        {
            List<AllergyViewModel> allergies = new List<AllergyViewModel>();
            while (connectionDb.SqlDataReader.Read())
            {
                AllergyViewModel model = new AllergyViewModel();
                model.AllergyType = connectionDb.SqlDataReader["ALLERGY_TYPE"].ToString();
                model.FullName = connectionDb.SqlDataReader["full_name"].ToString();
                model.Description = connectionDb.SqlDataReader["DESCRIPTION"].ToString();
                model.DiagnosticDate = connectionDb.SqlDataReader["DIAGNOSTIC_DATE"].ToString();

                allergies.Add(model);
            }
            return allergies;
        }
    }
}
