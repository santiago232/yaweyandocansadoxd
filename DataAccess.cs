using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Dapper;

namespace ProyectoFinal23
{
    public class DataAccess
    {
        // Cadena de conexión para base de datos local (LocalDB)
        public const string CONNECTION_STRING = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Pineda's\\source\\repos\\ProyectoFinal23\\ProyectoFinal23\\Tienda.mdf\";Integrated Security=True";

        // Cadena de conexión para instancia de SQL Server
        public const string CADENA_SQL_SERVER = "Server=DESKTOP-3C889HT\\SQLEXPRESS01;Integrated Security=true;Initial Catalog=master";

        public List<Producto> GetAllProductos()
        {
            List<Producto> productos = new List<Producto>();
            try
            {
                SqlConnection conn = new SqlConnection(CONNECTION_STRING);
                conn.Open();
                string query = "SELECT * FROM Productos";
                productos = conn.Query<Producto>(query).ToList();
                conn.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return productos;
        }

        public int InsertarProducto(Producto producto)
        {
            int result = 0;
            try
            {
                SqlConnection conn = new SqlConnection(CONNECTION_STRING);
                conn.Open();
                string query = @"INSERT INTO Productos (Codigo, Descripcion, Cantidad, PrecioDeIngreso, PrecioDeVenta, FechaDeIngreso)
                             VALUES (@Codigo, @Descripcion, @Cantidad, @PrecioDeIngreso, @PrecioDeVenta, @FechaDeIngreso)";
                result = conn.Execute(query, producto);
                conn.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }
            return result;
        }

        public List<Producto> GetAllDapper()
        {
            List<Producto> productos = new List<Producto>();
            try
            {
                // Utiliza la cadena de conexión adecuada según tu configuración
                SqlConnection conn = new SqlConnection(CADENA_SQL_SERVER);
                conn.Open();
                string query = "SELECT Codigo, Descripcion, Cantidad, PrecioDeIngreso, PrecioDeVenta, FechaDeIngreso FROM Productos";
                productos = conn.Query<Producto>(query).ToList();
                conn.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return productos;
        }

        // Otros métodos para insertar, actualizar, eliminar productos, etc.
    }
}