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
    [Route("api/[controller]")]
    [ApiController]
    public class VaccinationController : ControllerBase
    {
        // GET api/<VaccinationController>/5
        [HttpGet]
        [Route("GetPatientVaccines")]
        public IActionResult Get(string IdCard)
        {
            ConnectionDb connectionDb = new ConnectionDb();
            this.ExcListVaccinePatient(connectionDb, IdCard);
            List<VaccinationViewModel> vaccines = this.GetPatientVaccination(connectionDb);
            connectionDb.SqlConnection.Close();
            return Ok(vaccines);
        }

        private void ExcListVaccinePatient(ConnectionDb connectionDb, string IdCard)
        {
            string paramId = "@param_ID_CARD", commandText = "ADMINISTRATOR.sp_LIST_PATIENT_VACCINE";
            connectionDb.InitSqlComponents(commandText);
            connectionDb.CreateParameter(paramId, SqlDbType.VarChar, IdCard);
            connectionDb.ExcecuteReader();
        }

        private List<VaccinationViewModel> GetPatientVaccination(ConnectionDb connectionDb)
        {
            List<VaccinationViewModel> vaccines = new List<VaccinationViewModel>();
            while (connectionDb.SqlDataReader.Read())
            {
                VaccinationViewModel model = new VaccinationViewModel();
                model.VaccinationType = connectionDb.SqlDataReader["VACCINE_TYPE"].ToString();
                model.FullName = connectionDb.SqlDataReader["full_name"].ToString();
                model.Description = connectionDb.SqlDataReader["DESCRIPTION"].ToString();
                model.ApplicationDate = connectionDb.SqlDataReader["LATTEST_VACCINE_DATE"].ToString();
                model.NextVaccinationDate = connectionDb.SqlDataReader["NEXT_VACCINE_DATE"].ToString();
                vaccines.Add(model);
            }
            return vaccines;
        }

    }
}
