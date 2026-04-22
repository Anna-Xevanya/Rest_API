# Rest_API

### **a) Deskripsi Project dan Domain**
**Project**: API Sistem Informasi Akademik (Manajemen Guru & Siswa).
**Domain**: Pendidikan/E-Learning.
**Deskripsi**: Project ini adalah REST API yang dirancang untuk mengelola data operasional sekolah. Fokus utamanya adalah manajemen data **Guru** (sebagai user sistem/pengautentikasi), **Kelas**, dan **Siswa** (Murid). Project ini memungkinkan integrasi data antar tabel, seperti menetapkan Guru sebagai wali kelas dan memantau perkembangan nilai siswa dalam satu sistem yang terpusat.

### **b) Teknologi yang Digunakan**
* **Bahasa Pemrograman**: C#.
* **Framework**: ASP.NET Core Web API.
* **Database**: PostgreSQL.
* **Library Utama**: `Npgsql` (untuk koneksi database).

### **c) Langkah Instalasi & Cara Menjalankan**
1.  **Clone/Download**: Pastikan folder project (`Rest_API`) sudah ada di komputer Anda.
2.  **Visual Studio**: Buka file solusi (`.sln`) menggunakan Visual Studio 2022 atau versi terbaru.
3.  **NuGet Packages**: Klik kanan pada project, pilih **Manage NuGet Packages**, lalu instal `Npgsql`.
4.  **Konfigurasi Database**: Buka file `appsettings.json` dan sesuaikan `ConnectionStrings` dengan kredensial PostgreSQL Anda (Host, Port, Username, Password).
5.  **Build & Run**: Tekan tombol **Start** atau `F5` di Visual Studio untuk menjalankan aplikasi. Swagger UI akan otomatis terbuka di browser.

### **d) Cara Import Database**
1.  Buka aplikasi **pgAdmin 4** atau terminal **psql**.
2.  Buat database baru bernama `PAA_TM` (sesuaikan dengan nama di `appsettings.json`).
3.  Buka **Query Tool** pada database tersebut.
4.  Copy dan Paste script `database.sql` yang telah kita buat sebelumnya (berisi DDL `CREATE TABLE` dan data `INSERT`).
5.  Tekan tombol **Execute** (F5). Pastikan semua tabel (`akademik.guru`, `akademik.siswa`, dll.) berhasil dibuat dan data contoh sudah masuk.

### **e) Daftar Endpoint Lengkap**

| Method | URL | Keterangan |
| :--- | :--- | :--- |
| **POST** | `/api/akademik/register` | Mendaftarkan guru baru ke sistem. |
| **POST** | `/api/akademik/login` | Autentikasi guru untuk mengakses sistem. |
| **GET** | `/api/akademik/siswa` | Mengambil seluruh daftar siswa. |
| **GET** | `/api/akademik/siswa/{id}` | Mengambil data satu siswa berdasarkan ID. |
| **POST** | `/api/akademik/siswa` | Menambahkan data siswa baru (CRUD). |
| **PUT** | `/api/akademik/siswa/{id}` | Memperbarui data siswa yang sudah ada (CRUD). |
| **DELETE** | `/api/akademik/siswa/{id}` | Menghapus data siswa dari database (CRUD). |
