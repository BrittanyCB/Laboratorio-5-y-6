using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capa_Datos.Entidad;
using System.Configuration;
using System.Data;

namespace Capa_Datos
{
    public class GestionAutos
    {
        SqlConnection conexion;
        SqlCommand comando;
        string cadenacon = "Data Source=DESKTOP-VSL7C3P;Initial Catalog = Autos; Integrated Security = True";
        List<Info_Carro> listadoCarros;

        #region "CRUD"

        public int actualizarCarro(Info_Carro objCarro)
        {
            int registros = -1;
            using (SqlConnection sqlCon = new SqlConnection(cadenacon))
            {
                SqlCommand comd = new SqlCommand();
                comd.Connection = sqlCon;
                comd.CommandType = CommandType.Text;
                comd.CommandText = "Update Info_Carro " +
                                  "    set Marca = @Marca, " +
                                  "    Modelo = @Modelo, " +
                                  "    Pais = @Pais, " +
                                  "    Costo = @Costo " +
                                  "Where IdCarro = @IdCarro ";
                comd.Parameters.Add(new SqlParameter("@Marca", objCarro.Marca));
                comd.Parameters.Add(new SqlParameter("@Modelo", objCarro.Modelo));
                comd.Parameters.Add(new SqlParameter("@Pais", objCarro.Pais));
                comd.Parameters.Add(new SqlParameter("@Costo", objCarro.Costo));
                comd.Parameters.Add(new SqlParameter("@IdCarro", objCarro.IdCarro));
                sqlCon.Open();
                registros = comd.ExecuteNonQuery();
                sqlCon.Close();
            }

            return registros;
        }
        public int eliminarCarro(Info_Carro objCarro)
        {
            int registros = -1;
            using (SqlConnection sqlCon = new SqlConnection(cadenacon))
            {
                SqlCommand comd = new SqlCommand();
                comd.Connection = sqlCon;
                comd.CommandType = CommandType.Text;
                comd.CommandText = "Delete from Info_Carro Where IdCarro = @IdCarro";
                comd.Parameters.Add(new SqlParameter("@IdCarro", objCarro.IdCarro));
                sqlCon.Open();
                registros = comd.ExecuteNonQuery();
                sqlCon.Close();
            }

            return registros;
        }
        public int registrarCarro(Info_Carro objCarro)
        {
            int registros = -1;
            using (SqlConnection sqlCon = new SqlConnection(cadenacon))
            {
                SqlCommand comd = new SqlCommand();
                comd.Connection = sqlCon;
                comd.CommandType = CommandType.Text;
                comd.CommandText = "Insert into Info_Carro (IdCarro,Marca,Modelo,Pais,Costo) Values (@IdCarro,@Marca,@Modelo,@Pais,@Costo)";
                comd.Parameters.Add(new SqlParameter("@IdCarro", objCarro.IdCarro));
                comd.Parameters.Add(new SqlParameter("@Marca", objCarro.Marca));
                comd.Parameters.Add(new SqlParameter("@Modelo", objCarro.Modelo));
                comd.Parameters.Add(new SqlParameter("@Pais", objCarro.Pais));
                comd.Parameters.Add(new SqlParameter("@Costo", objCarro.Costo));
                sqlCon.Open();
                registros = comd.ExecuteNonQuery();
                sqlCon.Close();
            }

            return registros;

        }
        public DataTable cargaCarro()
        {
            DataTable objTable = new DataTable();
            conexion = new SqlConnection(cadenacon);
            comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Select * from Info_Carro";
            comando.Connection = conexion;
            SqlDataAdapter sqlAdaptador = new SqlDataAdapter(comando);
            sqlAdaptador.Fill(objTable); 
            return objTable;
        }

        #endregion
    }
}