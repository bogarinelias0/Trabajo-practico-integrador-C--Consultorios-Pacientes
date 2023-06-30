using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{

    // CAPA DE ACCESO A DATOS
    public class DataAccessLayer

    {

        private SqlConnection conecion = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=dbContacEjemplos;Data Source=DESKTOP-FL7VFSG\\SQLEXPRESS");

        public void insertContac(ContacModel contac)
        
        {

            try
            {
                conecion.Open();

                string query = @"
                                    INSERT INTO contactos( nombre, apellido, edad, correo)
                                    VALUES (@nombre , @apellido, @edad ,@correo)

                                ";
                // forma larga 
                SqlParameter nombre = new SqlParameter();
                nombre.ParameterName = "@nombre";
                nombre.Value = contac.nombre;
                nombre.DbType = System.Data.DbType.String; 

                // forma corta
                SqlParameter apellido = new SqlParameter("@apellido",contac.apellido);
                SqlParameter edad = new SqlParameter("@edad",contac.edad);
                SqlParameter correo = new SqlParameter("@correo" , contac.correo);

                SqlCommand sqlCommand = new SqlCommand(query,conecion);
                sqlCommand.Parameters.Add(nombre);
                sqlCommand.Parameters.Add (apellido);
                sqlCommand.Parameters.Add (edad);
                sqlCommand.Parameters.Add (correo);

                sqlCommand.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }
            finally { conecion.Close(); }


            
        }


        public List<ContacModel> GetContactos(string buscartxt = null)
        {
            List <ContacModel> contactos = new List<ContacModel>(); 
            try
            {
                conecion.Open();

                string query = @"SELECT   id,nombre,apellido , edad,correo
                                FROM contactos";

                SqlCommand coman = new SqlCommand();
                if (!string.IsNullOrEmpty(buscartxt))
                {
                    query += @" WHERE nombre LIKE @buscartxt OR  apellido LIKE @buscartxt OR edad LIKE @buscartxt   OR
                                correo LIKE @buscartxt ";


                    coman.Parameters.Add (new SqlParameter ("@buscartxt",$"%{buscartxt}%"));
                }
                coman.CommandText = query;
                coman.Connection = conecion;
                // para traer todsa las filas
                SqlDataReader reader = coman.ExecuteReader();

                while (reader.Read()) 
                {
                    contactos.Add(new ContacModel
                    {
                        id = int.Parse(reader["id"].ToString()),
                        nombre = reader["nombre"].ToString(),
                        apellido = reader["apellido"].ToString(),
                        edad = reader["edad"].ToString(),
                        correo = reader["correo"].ToString()

                    });
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally { conecion.Close(); }
            return contactos;


        }


        public void updateContacto (ContacModel contac )
        {

            try
            {
                conecion.Open();
                string quary = @"UPDATE    contactos 
                                   SET  nombre = @nombre ,
                                        apellido = @apellido,
                                        edad = @edad ,
                                        correo = @correo 
                                            WHERE id = @id        ";
                SqlParameter id = new SqlParameter("@id", contac.id);
                SqlParameter nombre = new SqlParameter("@nombre", contac.nombre);
                SqlParameter apellido = new SqlParameter("@apellido", contac.apellido);
                SqlParameter edad = new SqlParameter("@edad", contac.edad);
                SqlParameter correo = new SqlParameter("@correo", contac.correo);

                SqlCommand cmd = new SqlCommand(quary,conecion);
                cmd.Parameters.Add(id);
                cmd.Parameters.Add(nombre);
                cmd.Parameters.Add(apellido);
                cmd.Parameters.Add(edad);
                cmd.Parameters.Add(correo);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally { conecion.Close(); }
        
        }

        public void EliminarContac(int id) 
        {
            try
            {
                conecion.Open();
                string query = @"DELETE FROM contactos WHERE id = @id ";

                SqlCommand cmd = new SqlCommand (query,conecion);
                cmd.Parameters.Add(new SqlParameter (@"id",id));
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }finally { conecion.Close(); }
        }
    }
}
