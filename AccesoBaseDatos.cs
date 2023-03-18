using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock2._0
{
    public class AccesoBaseDatos
    {
        public SqlConnection conn = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=stock;Data Source=AGUSTIN");
        public void InsertCliente(Clientes clientes)
        {
            try
            {
                conn.Open();
                String query = @"INSERT INTO clientes (nombre)
VALUES (@nombre_cliente);";
                SqlParameter nombreCliente = new SqlParameter();
                nombreCliente.ParameterName = "@nombre_cliente";
                nombreCliente.Value = clientes.nombre_cliente;
                nombreCliente.DbType = System.Data.DbType.String;

                SqlCommand commmand = new SqlCommand(query, conn);
                commmand.Parameters.Add(nombreCliente);

                commmand.ExecuteNonQuery();

                
            }
            catch (Exception )
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
        public List<Clientes> GetClientes(string buscar_cliente = null)
        {
            List<Clientes> clientes = new List<Clientes>();
           
            try
            {
                conn.Open();
                string query = @"SELECT *
  FROM [stock].[dbo].[clientes]";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    clientes.Add(new Clientes {
                        Id = int.Parse(reader["Id"].ToString()),
                        nombre_cliente = reader["nombre"].ToString()
                    });
                }
            }
            catch
            {        
                
            }
            finally
            {
                conn.Close();
            }
            return clientes;
        }
        public void InsertarSucursal(Sucursal sucursal)
        {
            try
            {
                conn.Open();
                string query = @"insert into sucursales ([id_cliente], [nombre], [direccion]) values (@nombre_cliente, @nombre_sucursal, @direccion_sucursal);";

                SqlParameter cliente = new SqlParameter();
                cliente.ParameterName = "@nombre_cliente";
                cliente.Value = sucursal.nombre_cliente;
                cliente.DbType = System.Data.DbType.Int32;

                SqlParameter sucursal_nombre = new SqlParameter();
                sucursal_nombre.ParameterName = "@nombre_sucursal";
                sucursal_nombre.Value = sucursal.nombre_sucursal;
                sucursal_nombre.DbType = System.Data.DbType.String;

                SqlParameter sucursal_direccion = new SqlParameter();
                sucursal_direccion.ParameterName = "@direccion_sucursal";
                sucursal_direccion.Value = sucursal.direccion_sucursal;
                sucursal_direccion.DbType = System.Data.DbType.String;

                SqlCommand command = new SqlCommand(query,conn);

                command.Parameters.Add(cliente);
                command.Parameters.Add(sucursal_nombre);
                command.Parameters.Add(sucursal_direccion);

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("error"+ e);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public void InsertEquipos(Equipos equipos)
        {
            try
            {
                conn.Open();
                string query = @"INSERT INTO equipos (id_sucursal, modelo, numero_serie, ip, mac)
VALUES (@id_sucursal, @modelo, @numero_serie, @ip, @mac);";

                SqlParameter id_sucursal = new SqlParameter();
                id_sucursal.ParameterName = "@id_sucursal";
                id_sucursal.Value = equipos.id_sucursal;
                id_sucursal.DbType = System.Data.DbType.Int32;

                SqlParameter modelo = new SqlParameter();
                modelo.ParameterName = "@modelo";
                modelo.Value = equipos.modelo;
                modelo.DbType = System.Data.DbType.String;

                SqlParameter numero_serie = new SqlParameter();
                numero_serie.ParameterName = "@numero_serie";
                numero_serie.Value = equipos.numero_serie;
                numero_serie.DbType = System.Data.DbType.String;

                SqlParameter ip = new SqlParameter();
                ip.ParameterName = "@ip";
                ip.Value = equipos.ip;
                ip.DbType = System.Data.DbType.String;

                SqlParameter mac = new SqlParameter();
                mac.ParameterName = "@mac";
                mac.Value = equipos.mac;
                mac.DbType = System.Data.DbType.String;

                SqlCommand command = new SqlCommand(query,conn);

                command.Parameters.Add(id_sucursal);
                command.Parameters.Add(modelo);
                command.Parameters.Add(numero_serie);
                command.Parameters.Add(ip);
                command.Parameters.Add(mac);

                command.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
        }
        public List<Sucursal> GetSucursales(int buscar_sucursal = 0)
        {
            List<Sucursal> sucursal = new List<Sucursal>();
            try
            {
                conn.Open();
                string query = @"SELECT * FROM [stock].[dbo].[sucursales];";
                SqlCommand commmand = new SqlCommand(query, conn);
                SqlDataReader reader = commmand.ExecuteReader();
                while (reader.Read())
                {
                    sucursal.Add(new Sucursal {
                        id_sucursal = int.Parse(reader["id"].ToString()),
                        nombre_cliente = int.Parse(reader["id_cliente"].ToString()),
                        nombre_sucursal = reader["nombre"].ToString(),
                        direccion_sucursal = reader["direccion"].ToString()
                    });
                }
            }
            catch
            {
            }
            finally
            {
                conn.Close();
            }
            return sucursal;
        }
        public List<Equipos> GetEquipos(int buscar_equipo = 0)
        {
            List<Equipos> equipos = new List<Equipos>();
            try
            {
                conn.Open();
                string query = "SELECT * FROM[stock].[dbo].[equipos] order by id asc;";
                SqlCommand command = new SqlCommand(query,conn);
                SqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    equipos.Add(new Equipos {
                        id = int.Parse(reader["id"].ToString()),
                        id_sucursal = int.Parse(reader["id"].ToString()),
                        modelo = reader["modelo"].ToString(),
                        numero_serie = reader["numero_serie"].ToString(),
                        ip = reader["ip"].ToString(),
                        mac = reader["mac"].ToString()
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return equipos;
        }
        public void InsertModulos(Modulos modulos)
        {
            try
            {
                conn.Open();
                string query = @"INSERT INTO [dbo].[modulos]
                               ([id_equipo]
                               ,[nombre])
                                VALUES (@id_equipo, @nombre);";

                SqlParameter id_equipo = new SqlParameter();
                id_equipo.ParameterName = "@id_equipo";
                id_equipo.Value = modulos.id_equipo;
                id_equipo.DbType = System.Data.DbType.Int32;

                SqlParameter nombre_modulo = new SqlParameter();
                nombre_modulo.ParameterName = "@nombre";
                nombre_modulo.Value = modulos.nombre;
                nombre_modulo.DbType = System.Data.DbType.String;

                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.Add(id_equipo);
                command.Parameters.Add(nombre_modulo);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }


        //Obtener modulos
        public List<Modulos> GetModulos(int buscar_equipo = 0)
        {
            List<Modulos> modulos = new List<Modulos>();
            try
            {
                conn.Open();
                string query = "SELECT *  FROM [stock].[dbo].[modulos] order by id asc;";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    modulos.Add(new Modulos
                    {
                        id = int.Parse(reader["id"].ToString()),
                        id_equipo = int.Parse(reader["id_equipo"].ToString()),
                        nombre = reader["nombre"].ToString(),
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return modulos;
        }
        public List<Repuestos> GetRepuestos(int buscar_repuesto = 0)
        {
            List<Repuestos> repuestos = new List<Repuestos>();
            try
            {
                conn.Open();
                string query = @"SELECT TOP (1000) [id]
                              ,[id_repuesto]
                              ,[cantidad]
                              ,[id_modulo]
                              ,[id_equipo]
                              FROM [stock].[dbo].[stock_repuestos]";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    repuestos.Add(new Repuestos
                    {
                        id_repuesto = int.Parse(reader["id_repuesto"].ToString()),
                        tipo = reader["tipo"].ToString(),
                        descripcion = reader["descripcion"].ToString(),
                        estado = reader["estado"].ToString(),
                        codigo = reader["codigo"].ToString(),
                        cantidad = int.Parse(reader["cantidad"].ToString())
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            return repuestos;
        }
        public List<Equipos> Encontrar_Equipos(int id_cliente,int id_sucursal)
        {
            List<Equipos> equipos_encontrados = new List<Equipos>();
            List<Sucursal> sucursals = new List<Sucursal>();
            List<Clientes> clientes = new List<Clientes>();

            try
            {
                conn.Open();
                string query = @"SELECT *
                FROM equipos e
                INNER JOIN sucursales s ON e.id_sucursal = s.id
                WHERE s.nombre = 'Las Piedras';";

                SqlCommand command = new SqlCommand(query,conn);


            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            return equipos_encontrados;
        }
        public void AgregarModulos(int id_equipo = 0, int id_modulo = 0)
        {
            try
            {
                conn.Open();
                string query =  @"INSERT INTO modulos(id_equipo, nombre)
                                SELECT e.id, m.nombre
                                FROM(VALUES(@id_modulo)) AS e(id)
                                CROSS JOIN modulos AS m
                                WHERE m.id_equipo = @id_equipo;";
                SqlParameter id_del_equipo = new SqlParameter();
                id_del_equipo.ParameterName = "@id_equipo";
                id_del_equipo.Value = id_equipo;
                id_del_equipo.DbType = System.Data.DbType.Int32;

                SqlParameter id_del_modulo = new SqlParameter();
                id_del_modulo.ParameterName = "@id_modulo";
                id_del_modulo.Value = id_modulo;
                id_del_modulo.DbType = System.Data.DbType.Int32;

                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.Add(id_del_equipo);
                command.Parameters.Add(id_del_modulo);

                command.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;
            }
            finally 
            {
                conn.Close();
            }
        }
        public List<Equipos> Modelos_unicos()
        {
            List<Equipos> equipos = new List<Equipos>();
            try
            {
                conn.Open();
                string query = @"SELECT DISTINCT modelo
                FROM[stock].[dbo].[equipos];";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    equipos.Add(new Equipos
                    {
                        modelo = reader["modelo"].ToString()
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            return equipos;
        }
        public void InsertRepuesto(Repuestos repuestos)
        {

            try
            {
                conn.Open();
                string query = @"INSERT INTO repuestos (id_modulo, tipo, descripcion, estado, codigo, cantidad)
                VALUES (@id_modulo, @tipo, @descripcion, @estado, @codigo, @cantidad);";

                SqlParameter id_modulo = new SqlParameter();
                id_modulo.ParameterName = "@id_modulo";
                id_modulo.Value = repuestos.id_modulo;
                id_modulo.DbType = System.Data.DbType.Int32;

                SqlParameter tipo = new SqlParameter();
                tipo.ParameterName = "@tipo";
                tipo.Value = repuestos.tipo;
                tipo.DbType = System.Data.DbType.String;

                SqlParameter descripcion = new SqlParameter();
                descripcion.ParameterName = "@descripcion";
                descripcion.Value = repuestos.descripcion;
                descripcion.DbType = System.Data.DbType.String;

                SqlParameter estado = new SqlParameter();
                estado.ParameterName = "@estado";
                estado.Value = repuestos.estado;
                estado.DbType = System.Data.DbType.String;

                SqlParameter codigo = new SqlParameter();
                codigo.ParameterName = "@codigo";
                codigo.Value = repuestos.codigo;
                codigo.DbType = System.Data.DbType.String;

                SqlParameter cantidad = new SqlParameter();
                cantidad.ParameterName = "@cantidad";
                cantidad.Value = repuestos.cantidad;
                cantidad.DbType = System.Data.DbType.Int32;

                SqlCommand command = new SqlCommand(query,conn);

                command.Parameters.Add(id_modulo);
                command.Parameters.Add(tipo);
                command.Parameters.Add(descripcion);
                command.Parameters.Add(estado);
                command.Parameters.Add(codigo);
                command.Parameters.Add(cantidad);

                command.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
