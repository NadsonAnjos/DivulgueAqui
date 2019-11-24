using DivulgueAqui.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DivulgueAqui.Controllers
{
    public class DashboardAdministracaoController : Controller
    {
        // GET: DashboardAdministracao
        public ActionResult Faturamento()
        {
            return View();
        }

        public ActionResult IndexAdministracao()
        {
            return View();
        }

        public ActionResult Anunciantes()
        {
            List<Models.Anunciante> lista = teste();
            //List<Models.Anunciante> lista = GetAnunciantes();
            return View(lista);
        }

        public List<Models.Anunciante> teste()
        {
            List<Models.Anunciante> lista = new List<Models.Anunciante>();
            Models.Anunciante fiel = new Models.Anunciante();
            fiel.Email = "ola@gmai.com";
            fiel.NomeAnunciante = "Adenilson";
            fiel.NomeComercio = "mercadinho";
            fiel.TipoPacote = "4 ANUNCIOS";

            Models.Anunciante fiel1 = new Models.Anunciante();
            fiel1.Email = "abesvaldo@gmai.com";
            fiel1.NomeAnunciante = "Abesvaldo";
            fiel1.NomeComercio = "Churros";
            fiel1.TipoPacote = "9 ANUNCIOS";
            lista.Add(fiel1);

            return lista;
        }

        public ActionResult Detalhes(Anunciante a) {
            string connectionString = "Server=DESKTOP-IO4T1DR\\SQLExpress; AttachDbFilename=C:\\Program Files\\Microsoft SQL Server\\MSSQL11.SQLEXPRESS\\MSSQL\\DATA\\DivulgueAquiDB.mdf; Database=DivulgueAquiDB; Trusted_Connection = Yes;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string cmd = "SELECT * FROM Anunciante WHERE Email=@Email";

                    SqlCommand command = new SqlCommand(cmd, connection);
                    command.Connection.Open();
                    var reader = command.ExecuteReader();
                    Models.Anunciante anun = new Models.Anunciante();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            anun.NomeAnunciante = (string)reader["NomeAnunciante"];
                            anun.NomeComercio = (string)reader["NomeComercio"];
                            anun.TipoComercio = (string)reader["TipoComercio"];
                            anun.TipoPacote = (string)reader["TipoPacote"];
                            anun.IdAnunciante = (int)reader["IdAnunciante"];
                            anun.Email = (string)reader["Email"];
                            anun.Location = (string)reader["Location"];
                        }
                    }
                    return View(anun);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro: " + ex.Message);
                }
            }
        }

        public List<Models.Anunciante> GetAnunciantes()
        {
            string connectionString = "Server=DESKTOP-IO4T1DR\\SQLExpress; AttachDbFilename=C:\\Program Files\\Microsoft SQL Server\\MSSQL11.SQLEXPRESS\\MSSQL\\DATA\\DivulgueAquiDB.mdf; Database=DivulgueAquiDB; Trusted_Connection = Yes;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string cmd = "SELECT NomeAnunciante, NomeComercio, Email, TipoPacote FROM Anunciante";

                    SqlCommand command = new SqlCommand(cmd, connection);
                    command.Connection.Open();
                    var reader = command.ExecuteReader();
                    List<Models.Anunciante> lista = new List<Models.Anunciante>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Models.Anunciante anun = new Models.Anunciante();
                            anun.NomeAnunciante = (string)reader["NomeAnunciante"];
                            anun.NomeComercio = (string)reader["NomeComercio"];
                            anun.Email = (string)reader["Email"];
                            anun.TipoPacote = (string)reader["TIpoPacote"];

                            lista.Add(anun);
                        }
                        return lista;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro: " + ex.Message);
                }
                return null;
            }
        }

        public List<Models.Faturamento> getFatura()
        {
            List<Models.Faturamento> lista = new List<Faturamento>();
            string connectionString = "Server=DESKTOP-IO4T1DR\\SQLExpress; AttachDbFilename=C:\\Program Files\\Microsoft SQL Server\\MSSQL11.SQLEXPRESS\\MSSQL\\DATA\\DivulgueAquiDB.mdf; Database=DivulgueAquiDB; Trusted_Connection = Yes;";
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                try
                {
                    string cmd = "SELECT p.Mes, p.ValorArrecadado FROM Pagamentos AS p GROUP BY p.Mes";
                    SqlCommand command = new SqlCommand(cmd, connection);
                    command.Connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Models.Faturamento fat = new Models.Faturamento();
                            fat.Mes = (string)reader["Mes"];
                            fat.ValorEstimado = (float)reader["ValorEstimado"];
                            fat.ValorArrecadado = (float)reader["ValorArrecadado"];

                            lista.Add(fat);
                        }
                        return lista;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro: " + ex.Message);
                }
            }
            return lista;
        }
    }
}