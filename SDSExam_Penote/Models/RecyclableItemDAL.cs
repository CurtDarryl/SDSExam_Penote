using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace SDSExam_Penote.Models
{
    public class RecyclableItemDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["RecyclableDB_Penote"].ConnectionString;

        public List<RecyclableItem> GetAll()
        {
            List<RecyclableItem> list = new List<RecyclableItem>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllRecyclableItems", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    list.Add(new RecyclableItem
                    {
                        Id = (int)rdr["Id"],
                        RecyclableTypeId = (int)rdr["RecyclableTypeId"],
                        Weight = (decimal)rdr["Weight"],
                        ComputedRate = (decimal)rdr["ComputedRate"],
                        ItemDescription = rdr["ItemDescription"].ToString()
                    });
                }
            }
            return list;
        }
        public void InsertRecyclableItem(RecyclableItem item)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertRecyclableItem", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RecyclableTypeId", item.RecyclableTypeId);
                    cmd.Parameters.AddWithValue("@Weight", item.Weight);
                    cmd.Parameters.AddWithValue("@ItemDescription", item.ItemDescription);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public RecyclableItem GetById(int id)
        {
            RecyclableItem item = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetRecyclableItemById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    item = new RecyclableItem
                    {
                        Id = (int)rdr["Id"],
                        RecyclableTypeId = (int)rdr["RecyclableTypeId"],
                        Weight = (decimal)rdr["Weight"],
                        ComputedRate = (decimal)rdr["ComputedRate"],
                        ItemDescription = rdr["ItemDescription"].ToString()
                    };
                }
            }
            return item;
        }

        public void UpdateRecyclableItem(RecyclableItem item)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_UpdateRecyclableItem", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@RecyclableTypeId", item.RecyclableTypeId);
                    cmd.Parameters.AddWithValue("@Weight", item.Weight);
                    cmd.Parameters.AddWithValue("@ItemDescription", item.ItemDescription);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteRecyclableItem", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}

