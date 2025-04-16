using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SDSExam_Penote.Models
{
    public class RecyclableTypeDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["RecyclableDB_Penote"].ConnectionString;

        public List<RecyclableType> GetAll()
        {
            List<RecyclableType> list = new List<RecyclableType>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllRecyclableTypes", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    list.Add(new RecyclableType
                    {
                        Id = (int)rdr["Id"],
                        Type = rdr["Type"].ToString(),
                        Rate = (decimal)rdr["Rate"],
                        MinKg = (decimal)rdr["MinKg"],
                        MaxKg = (decimal)rdr["MaxKg"]
                    });
                }
            }
            return list;
        }

        public void Insert(RecyclableType rt)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_InsertRecyclableType", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Type", rt.Type);
                cmd.Parameters.AddWithValue("@Rate", rt.Rate);
                cmd.Parameters.AddWithValue("@MinKg", rt.MinKg);
                cmd.Parameters.AddWithValue("@MaxKg", rt.MaxKg);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public RecyclableType GetById(int id)
        {
            RecyclableType rt = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetRecyclableTypeById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    rt = new RecyclableType
                    {
                        Id = (int)rdr["Id"],
                        Type = rdr["Type"].ToString(),
                        Rate = (decimal)rdr["Rate"],
                        MinKg = (decimal)rdr["MinKg"],
                        MaxKg = (decimal)rdr["MaxKg"]
                    };
                }
            }
            return rt;
        }

        public void Update(RecyclableType rt)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateRecyclableType", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", rt.Id);
                cmd.Parameters.AddWithValue("@Type", rt.Type);
                cmd.Parameters.AddWithValue("@Rate", rt.Rate);
                cmd.Parameters.AddWithValue("@MinKg", rt.MinKg);
                cmd.Parameters.AddWithValue("@MaxKg", rt.MaxKg);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteRecyclableType", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}