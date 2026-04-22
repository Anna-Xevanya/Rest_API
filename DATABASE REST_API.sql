-- Schema
CREATE SCHEMA IF NOT EXISTS akademik;

-- 1. Tabel Guru 
CREATE TABLE akademik.guru (
    id_guru SERIAL PRIMARY KEY,
    username VARCHAR(50) UNIQUE NOT NULL,
    password TEXT NOT NULL,
    nip VARCHAR(20) UNIQUE NOT NULL,
    nama_guru VARCHAR(100) NOT NULL,
    spesialisasi VARCHAR(50),
    no_telp VARCHAR(15),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- 2. Tabel Kelas (Relasi ke Guru sebagai Wali Kelas)
CREATE TABLE akademik.kelas (
    id_kelas SERIAL PRIMARY KEY,
    nama_kelas VARCHAR(20) NOT NULL,
    id_guru_wali INT REFERENCES akademik.guru(id_guru) ON DELETE SET NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- 3. Tabel Siswa (Relasi ke Kelas)
CREATE TABLE akademik.siswa (
    id_siswa SERIAL PRIMARY KEY,
    id_kelas INT REFERENCES akademik.kelas(id_kelas) ON DELETE SET NULL,
    nama VARCHAR(100) NOT NULL,
    email VARCHAR(100) UNIQUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- 4. Tabel Nilai (Relasi ke Siswa)
CREATE TABLE akademik.nilai (
    id_nilai SERIAL PRIMARY KEY,
    id_siswa INT REFERENCES akademik.siswa(id_siswa) ON DELETE CASCADE,
    mata_pelajaran VARCHAR(50) NOT NULL,
    skor INT CHECK (skor >= 0 AND skor <= 100),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Indexing untuk optimasi filter
CREATE INDEX idx_guru_username ON akademik.guru(username);
CREATE INDEX idx_siswa_nama ON akademik.siswa(nama);
CREATE INDEX idx_nilai_matpel ON akademik.nilai(mata_pelajaran);

-- Sample Data (Minimal 5 baris per tabel)
INSERT INTO akademik.guru (username, password, nip, nama_guru, spesialisasi, no_telp) VALUES 
('budis', '1234', '198001', 'Budi Santoso', 'Matematika', '0812'),
('sitia', '1234', '198502', 'Siti Aminah', 'Inggris', '0813'),
('agusp', '1234', '197503', 'Agus Prayogo', 'Fisika', '0814'),
('larasw', '1234', '199004', 'Laras Wati', 'Informatika', '0815'),
('rudih', '1234', '198205', 'Rudi Hermawan', 'Sejarah', '0816');

INSERT INTO akademik.kelas (nama_kelas, id_guru_wali) VALUES 
('10-IPA', 1), ('10-IPS', 2), ('11-IPA', 3), ('11-IPS', 4), ('12-IPA', 5);

INSERT INTO akademik.siswa (id_kelas, nama, email) VALUES 
(1, 'Andi', 'andi@mail.com'), (1, 'Budi', 'budi@mail.com'), (2, 'Citra', 'citra@mail.com'), (3, 'Dewi', 'dewi@mail.com'), (4, 'Eko', 'eko@mail.com');

INSERT INTO akademik.nilai (id_siswa, mata_pelajaran, skor) VALUES 
(1, 'Matematika', 85), (2, 'Matematika', 90), (3, 'Ekonomi', 75), (4, 'Fisika', 80), (5, 'Biologi', 88);