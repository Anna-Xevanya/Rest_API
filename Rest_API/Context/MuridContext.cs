using Npgsql;
using Rest_API.Models;
using Rest_API.Helpers;
    


namespace Rest_API.Context
{
    public class MuridContext
    {
        private string __constr;

        public MuridContext(string pCOnstr)
        {
            __constr = pCOnstr;
        }

        // READ ALL
        public List<Murid> ListMurid()
        {
            List<Murid> list = new List<Murid>();
            sqlDBHelper db = new sqlDBHelper(__constr);
            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand("SELECT id_murid, nama, alamat, email FROM users.murid");
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Murid()
                    {
                        id_murid = int.Parse(reader["id_murid"].ToString()),
                        nama = reader["nama"].ToString(),
                        alamat = reader["alamat"].ToString(),
                        email = reader["email"].ToString()
                    });
                }
                db.closeConnection();
            }
            catch (Exception ex) { throw ex; }
            return list;
        }

        // CREATE
        public void AddMurid(Murid mrd)
        {
            sqlDBHelper db = new sqlDBHelper(__constr);
            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand("INSERT INTO users.murid (nama, alamat, email) VALUES (@nama, @alamat, @email)");
                cmd.Parameters.AddWithValue("@nama", mrd.nama);
                cmd.Parameters.AddWithValue("@alamat", mrd.alamat);
                cmd.Parameters.AddWithValue("@email", mrd.email);
                cmd.ExecuteNonQuery();
                db.closeConnection();
            }
            catch (Exception ex) { throw ex; }
        }

        // UPDATE
        public void UpdateMurid(int id, Murid mrd)
        {
            sqlDBHelper db = new sqlDBHelper(__constr);
            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand("UPDATE users.murid SET nama=@nama, alamat=@alamat, email=@email WHERE id_murid=@id");
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nama", mrd.nama);
                cmd.Parameters.AddWithValue("@alamat", mrd.alamat);
                cmd.Parameters.AddWithValue("@email", mrd.email);
                cmd.ExecuteNonQuery();
                db.closeConnection();
            }
            catch (Exception ex) { throw ex; }
        }

        // DELETE
        public void DeleteMurid(int id)
        {
            sqlDBHelper db = new sqlDBHelper(__constr);
            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand("DELETE FROM users.murid WHERE id_murid=@id");
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                db.closeConnection();
            }
            catch (Exception ex) { throw ex; }
        }

    }
}
