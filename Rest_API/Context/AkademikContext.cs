using Npgsql;
using Rest_API.Helpers;
using Rest_API.Models;

namespace Rest_API.Context
{
    public class AkademikContext
    {
        private readonly string _constr;
        public AkademikContext(string constr) => _constr = constr;


        public void RegisterGuru(Guru g)
        {
            using (var db = new sqlDBHelper(_constr))
            {
                try
                {
                    // Query untuk memasukkan data guru baru
                    var cmd = db.getNpgsqlCommand(@"
                        INSERT INTO akademik.guru 
                        (username, password, nip, nama_guru, spesialisasi, no_telp) 
                        VALUES (@u, @p, @nip, @nama, @spec, @telp)");

                    cmd.Parameters.AddWithValue("@u", g.username);
                    cmd.Parameters.AddWithValue("@p", g.password); // Sebaiknya di-hash sebelum disimpan
                    cmd.Parameters.AddWithValue("@nip", g.nip);
                    cmd.Parameters.AddWithValue("@nama", g.nama_guru);
                    // Gunakan DBNull.Value jika data opsional kosong
                    cmd.Parameters.AddWithValue("@spec", (object)g.spesialisasi ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@telp", (object)g.no_telp ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Gagal melakukan registrasi: " + ex.Message);
                }
            }
        }

        // AUTH: LOGIN GURU
        public bool LoginGuru(string username, string password)
        {
            using var db = new sqlDBHelper(_constr);
            var cmd = db.getNpgsqlCommand("SELECT COUNT(1) FROM akademik.guru WHERE username=@u AND password=@p");
            cmd.Parameters.AddWithValue("@u", username);
            cmd.Parameters.AddWithValue("@p", password);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        // CRUD SISWA: GET ALL
        public List<Siswa> ListSiswa()
        {
            List<Siswa> list = new List<Siswa>();
            using var db = new sqlDBHelper(_constr);
            var cmd = db.getNpgsqlCommand("SELECT id_siswa, id_kelas, nama, email FROM akademik.siswa");
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Siswa
                {
                    id_siswa = (int)reader["id_siswa"],
                    id_kelas = (int)reader["id_kelas"],
                    nama = reader["nama"].ToString(),
                    email = reader["email"].ToString()
                });
            }
            return list;
        }

        // CRUD SISWA: GET BY ID
        public Siswa GetSiswaById(int id)
        {
            using (var db = new sqlDBHelper(_constr))
            {
                try
                {
                    var cmd = db.getNpgsqlCommand("SELECT * FROM akademik.siswa WHERE id_siswa = @id");
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Siswa
                            {
                                id_siswa = (int)reader["id_siswa"],
                                id_kelas = (int)reader["id_kelas"],
                                nama = reader["nama"].ToString(),
                                email = reader["email"].ToString()
                            };
                        }
                    }
                    db.closeConnection();
                    return null;
                }
                catch (Exception ex) { throw ex; }
            }
        }

        // CRUD SISWA: POST
        public void AddSiswa(Siswa s)
        {
            using var db = new sqlDBHelper(_constr);
            var cmd = db.getNpgsqlCommand("INSERT INTO akademik.siswa (id_kelas, nama, email) VALUES (@k, @n, @e)");
            cmd.Parameters.AddWithValue("@k", s.id_kelas);
            cmd.Parameters.AddWithValue("@n", s.nama);
            cmd.Parameters.AddWithValue("@e", s.email);
            cmd.ExecuteNonQuery();
        }

        // CRUD SISWA: PUT
        public void UpdateSiswa(int id, Siswa s)
        {
            using var db = new sqlDBHelper(_constr);
            var cmd = db.getNpgsqlCommand("UPDATE akademik.siswa SET id_kelas=@k, nama=@n, email=@e, updated_at=CURRENT_TIMESTAMP WHERE id_siswa=@id");
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@k", s.id_kelas);
            cmd.Parameters.AddWithValue("@n", s.nama);
            cmd.Parameters.AddWithValue("@e", s.email);
            cmd.ExecuteNonQuery();
        }

        // CRUD SISWA: DELETE
        public void DeleteSiswa(int id)
        {
            using var db = new sqlDBHelper(_constr);
            var cmd = db.getNpgsqlCommand("DELETE FROM akademik.siswa WHERE id_siswa=@id");
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}



//using Npgsql;
//using Rest_API.Models;
//using Rest_API.Helpers;
//using System.Collections.Generic;
//using System;

//namespace Rest_API.Context
//{
//    public class AkademikContext
//    {

//        private string __constr;

//        public AkademikContext(string pCOnstr)
//        {
//            __constr = pCOnstr;
//        }

//        // ==========================================
//        // AUTHENTICATION: GURU (LOGIN & REGISTER)
//        // ==========================================

//        public bool LoginGuru(string username, string password)
//        {
//            using (var db = new sqlDBHelper(__constr))
//            {
//                try
//                {
//                    var cmd = db.getNpgsqlCommand("SELECT COUNT(1) FROM akademik.guru WHERE username=@u AND password=@p");
//                    cmd.Parameters.AddWithValue("@u", username);
//                    cmd.Parameters.AddWithValue("@p", password);
//                    var count = Convert.ToInt32(cmd.ExecuteScalar());
//                    db.closeConnection();
//                    return count > 0;
//                }
//                catch (Exception ex) { throw ex; }
//            }
//        }

//        public void RegisterGuru(Guru g)
//        {
//            using (var db = new sqlDBHelper(__constr))
//            {
//                try
//                {
//                    var cmd = db.getNpgsqlCommand("INSERT INTO akademik.guru (username, password, nip, nama_guru, spesialisasi, no_telp) VALUES (@u, @p, @nip, @nama, @spec, @telp)");
//                    cmd.Parameters.AddWithValue("@u", g.username);
//                    cmd.Parameters.AddWithValue("@p", g.password);
//                    cmd.Parameters.AddWithValue("@nip", g.nip);
//                    cmd.Parameters.AddWithValue("@nama", g.nama_guru);
//                    cmd.ExecuteNonQuery();
//                    db.closeConnection();
//                }
//                catch (Exception ex) { throw ex; }
//            }
//        }

//        // ==========================================
//        // CRUD: SISWA
//        // ==========================================

//        public List<Siswa> ListSiswa()
//        {
//            List<Siswa> list = new List<Siswa>();
//            using (var db = new sqlDBHelper(__constr))
//            {
//                try
//                {
//                    var cmd = db.getNpgsqlCommand("SELECT id_siswa, id_kelas, nama, email FROM akademik.siswa");
//                    using (var reader = cmd.ExecuteReader())
//                    {
//                        while (reader.Read())
//                        {
//                            list.Add(new Siswa()
//                            {
//                                id_siswa = (int)reader["id_siswa"],
//                                id_kelas = reader["id_kelas"] != DBNull.Value ? (int)reader["id_kelas"] : 0,
//                                nama = reader["nama"].ToString(),
//                                email = reader["email"].ToString()
//                            });
//                        }
//                    }
//                    db.closeConnection();
//                }
//                catch (Exception ex) { throw ex; }
//            }
//            return list;
//        }

//        public Siswa GetSiswaById(int id)
//        {
//            using (var db = new sqlDBHelper(__constr))
//            {
//                try
//                {
//                    var cmd = db.getNpgsqlCommand("SELECT * FROM akademik.siswa WHERE id_siswa = @id");
//                    cmd.Parameters.AddWithValue("@id", id);
//                    using (var reader = cmd.ExecuteReader())
//                    {
//                        if (reader.Read())
//                        {
//                            return new Siswa
//                            {
//                                id_siswa = (int)reader["id_siswa"],
//                                id_kelas = (int)reader["id_kelas"],
//                                nama = reader["nama"].ToString(),
//                                email = reader["email"].ToString()
//                            };
//                        }
//                    }
//                    db.closeConnection();
//                    return null;
//                }
//                catch (Exception ex) { throw ex; }
//            }
//        }

//        public void AddSiswa(Siswa s)
//        {
//            using (var db = new sqlDBHelper(__constr))
//            {
//                try
//                {
//                    var cmd = db.getNpgsqlCommand("INSERT INTO akademik.siswa (id_kelas, nama, email) VALUES (@k, @n, @e)");
//                    cmd.Parameters.AddWithValue("@k", s.id_kelas);
//                    cmd.Parameters.AddWithValue("@n", s.nama);
//                    cmd.Parameters.AddWithValue("@e", s.email);
//                    cmd.ExecuteNonQuery();
//                    db.closeConnection();
//                }
//                catch (Exception ex) { throw ex; }
//            }
//        }

//        public void UpdateSiswa(int id, Siswa s)
//        {
//            using (var db = new sqlDBHelper(__constr))
//            {
//                try
//                {
//                    var cmd = db.getNpgsqlCommand("UPDATE akademik.siswa SET id_kelas=@k, nama=@n, email=@e, updated_at=CURRENT_TIMESTAMP WHERE id_siswa=@id");
//                    cmd.Parameters.AddWithValue("@id", id);
//                    cmd.Parameters.AddWithValue("@k", s.id_kelas);
//                    cmd.Parameters.AddWithValue("@n", s.nama);
//                    cmd.Parameters.AddWithValue("@e", s.email);
//                    cmd.ExecuteNonQuery();
//                    db.closeConnection();
//                }
//                catch (Exception ex) { throw ex; }
//            }
//        }

//        public void DeleteSiswa(int id)
//        {
//            using (var db = new sqlDBHelper(__constr))
//            {
//                try
//                {
//                    var cmd = db.getNpgsqlCommand("DELETE FROM akademik.siswa WHERE id_siswa=@id");
//                    cmd.Parameters.AddWithValue("@id", id);
//                    cmd.ExecuteNonQuery();
//                    db.closeConnection();
//                }
//                catch (Exception ex) { throw ex; }
//            }
//        }
//    }
//}