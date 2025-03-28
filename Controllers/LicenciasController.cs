﻿using Archiva.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;

namespace Archiva.Controllers
{


    public class LicenciasController : Controller
    {
        private readonly DbConexion _db;

        public LicenciasController(DbConexion db)
        {
            _db = db;
        }

              // -------------------------   Inicio (carga de tabla Licencias)   --------------------------------

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ObtenerLicencias()
        {
            List<Licencias> lista = new List<Licencias>();

            using (var conn = _db.CrearConexion())
            {
                conn.Open();
                string query = "SELECT * FROM LicenciasWeb";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Licencias
                        {
                            Id = reader.GetInt32("Id"),
                            Nombre = reader["Nombre"]?.ToString(),
                            Fecha = reader["Fecha"]?.ToString(),
                            DiasInc = reader.GetInt32("DiasInc"),
                            Diagnostico = reader["Diagnostico"]?.ToString(),
                            Municipio = reader["Municipio"]?.ToString(),
                            Nivel = reader["Nivel"]?.ToString(),
                            Serie = reader["Serie"]?.ToString(),
                            Codigo = reader.GetInt32("Codigo")
                        });
                    }
                }
            }

            return Json(lista);
        }

        // -------------------------   Inicio (Formulario InserciónLicencias)   --------------------------------

        // Mostrar el formulario
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Recibir el formulario y llamar al procedimiento
        [HttpPost]
        public JsonResult CreateAjax(Licencias lic)
        {
            try
            {
                using (var conn = _db.CrearConexion())
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand("InsertarLicencia", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@pNombre", lic.Nombre);
                        cmd.Parameters.AddWithValue("@pFecha", lic.Fecha);
                        cmd.Parameters.AddWithValue("@pDiasInc", lic.DiasInc);
                        cmd.Parameters.AddWithValue("@pDiagnostico", lic.Diagnostico);
                        cmd.Parameters.AddWithValue("@pMunicipio", lic.Municipio);
                        cmd.Parameters.AddWithValue("@pNivel", lic.Nivel);
                        cmd.Parameters.AddWithValue("@pSerie", lic.Serie);
                        cmd.Parameters.AddWithValue("@pCodigo", lic.Codigo);

                        cmd.ExecuteNonQuery();
                    }
                }

                return Json(new { success = true, message = "Licencia registrada con éxito." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }


        // -------------------------   Inicio (Graficas Licencias)   --------------------------------



        [HttpGet]
        public JsonResult GetDatosGrafica(string fechaInicio, string fechaFin)
        {
            try
            {
                var datos = new Dictionary<string, int>();

                using (var conn = _db.CrearConexion())
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand("SELECT Nivel, COUNT(*) AS Cantidad FROM LicenciasWeb WHERE STR_TO_DATE(Fecha, '%d/%m/%Y') BETWEEN @fechaInicio AND @fechaFin GROUP BY Nivel", conn))
                    {
                        cmd.Parameters.AddWithValue("@fechaInicio", DateTime.Parse(fechaInicio).ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@fechaFin", DateTime.Parse(fechaFin).ToString("yyyy-MM-dd"));

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                datos[reader.GetString("Nivel")] = reader.GetInt32("Cantidad");
                            }
                        }
                    }
                }

                return Json(new { success = true, data = new { labels = datos.Keys, valores = datos.Values } });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }



        // -------------------------   END   --------------------------------
    }
}


    
