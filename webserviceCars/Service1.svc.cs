using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Configuration;

namespace webserviceCars
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private string connectionString = @"Server=tcp:ebvadatabase.database.windows.net,1433;Initial Catalog=students;Persist Security Info=False;User ID=ebva;Password=st0pTyven;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public Car addCar(Car car)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO cars(model, brand, color, engine) OUTPUT Inserted.Id VALUES(@model, @brand, @color, @engine) ";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@model", car.Model);
                cmd.Parameters.AddWithValue("@brand", car.Brand);
                cmd.Parameters.AddWithValue("@color", car.color.ToString());
                cmd.Parameters.AddWithValue("@engine", car.engine);
                cmd.CommandType = CommandType.Text;
                //cmd.ExecuteNonQuery();
                car.id = (Int32) cmd.ExecuteScalar();
            }
            return car;
        }

        public Car getCar(int id)
        {
            Car car = new Car();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * from cars where Id = @id";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.CommandType = CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                
                car.id = Convert.ToInt32(reader["Id"].ToString());
                car.Model = reader["model"].ToString();
                car.Brand = reader["brand"].ToString();
                car.engine = float.Parse(reader["engine"].ToString());
                car.color = (Colors) Enum.Parse(typeof(Colors), reader["color"].ToString(), true);
            }
            return car;
        }

        public List<Car> getCars()
        {
            List<Car> cars = new List<Car>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * from cars";
                SqlCommand cmd = new SqlCommand(sql, connection);
               
                cmd.CommandType = CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Car car = new Car();
                    car.id = Convert.ToInt32(reader["Id"].ToString());
                    car.Model = reader["model"].ToString();
                    car.Brand = reader["brand"].ToString();
                    car.engine = float.Parse(reader["engine"].ToString());
                    car.color = (Colors)Enum.Parse(typeof(Colors), reader["color"].ToString(), true);
                }                
            }
            return cars;
        }
    }
}
