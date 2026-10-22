using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using ReceptekApi.Models;

namespace ReceptekApi.Controllers
{
    [Route("hozzavalo")]
    [ApiController]
    public class HozzavaloController : ControllerBase
    {
        MySqlConnection kapcsolat = new MySqlConnection("Server=localhost;Database=receptek;User=root;Password=;");

        [HttpGet]
        public List<Hozzavalo> OsszesHozzavalo()
        {
            List<Hozzavalo> lista = new List<Hozzavalo>();

            kapcsolat.Open();
            MySqlCommand parancs = new MySqlCommand("SELECT * FROM hozzavalo", kapcsolat);
            MySqlDataReader olvaso = parancs.ExecuteReader();

            while (olvaso.Read())
            {
                Hozzavalo h = new Hozzavalo();
                h.Id = olvaso.GetInt32("id");
                h.HozzavaloNev = olvaso.GetString("ingredientName");
                h.Mennyiseg = olvaso.GetString("quantity");
                h.Leiras = olvaso.GetString("description");
                h.HozzaadasIdeje = olvaso.GetDateTime("addedTime");
                h.FrissitesIdeje = olvaso.GetDateTime("updateTime");
                h.SzakacsId = olvaso.GetInt32("szakacsId");
                lista.Add(h);
            }

            olvaso.Close();
            kapcsolat.Close();
            return lista;
        }

        [HttpGet("{id}")]
        public Hozzavalo? EgyHozzavalo(int id)
        {
            kapcsolat.Open();
            MySqlCommand parancs = new MySqlCommand("SELECT * FROM hozzavalo WHERE id = @id", kapcsolat);
            parancs.Parameters.AddWithValue("@id", id);
            MySqlDataReader olvaso = parancs.ExecuteReader();

            if (!olvaso.Read())
            {
                olvaso.Close();
                kapcsolat.Close();
                return null;
            }

            Hozzavalo h = new Hozzavalo();
            h.Id = olvaso.GetInt32("id");
            h.HozzavaloNev = olvaso.GetString("ingredientName");
            h.Mennyiseg = olvaso.GetString("quantity");
            h.Leiras = olvaso.GetString("description");
            h.HozzaadasIdeje = olvaso.GetDateTime("addedTime");
            h.FrissitesIdeje = olvaso.GetDateTime("updateTime");
            h.SzakacsId = olvaso.GetInt32("szakacsId");

            olvaso.Close();
            kapcsolat.Close();
            return h;
        }

        [HttpPost]
        public Hozzavalo UjHozzavalo(Hozzavalo h)
        {
            h.HozzaadasIdeje = DateTime.Now;
            h.FrissitesIdeje = DateTime.Now;

            kapcsolat.Open();
            MySqlCommand parancs = new MySqlCommand(
                "INSERT INTO hozzavalo (ingredientName, quantity, description, addedTime, updateTime, szakacsId) " +
                "VALUES (@nev, @mennyiseg, @leiras, @hozzaadas, @frissites, @szakacsId)", kapcsolat);
            parancs.Parameters.AddWithValue("@nev", h.HozzavaloNev);
            parancs.Parameters.AddWithValue("@mennyiseg", h.Mennyiseg);
            parancs.Parameters.AddWithValue("@leiras", h.Leiras);
            parancs.Parameters.AddWithValue("@hozzaadas", h.HozzaadasIdeje);
            parancs.Parameters.AddWithValue("@frissites", h.FrissitesIdeje);
            parancs.Parameters.AddWithValue("@szakacsId", h.SzakacsId);
            parancs.ExecuteNonQuery();
            h.Id = (int)parancs.LastInsertedId;
            kapcsolat.Close();

            return h;
        }

        [HttpPut("{id}")]
        public string HozzavaloModositas(int id, Hozzavalo h)
        {
            h.FrissitesIdeje = DateTime.Now;

            kapcsolat.Open();
            MySqlCommand parancs = new MySqlCommand(
                "UPDATE hozzavalo SET ingredientName = @nev, quantity = @mennyiseg, description = @leiras, " +
                "updateTime = @frissites, szakacsId = @szakacsId WHERE id = @id", kapcsolat);
            parancs.Parameters.AddWithValue("@nev", h.HozzavaloNev);
            parancs.Parameters.AddWithValue("@mennyiseg", h.Mennyiseg);
            parancs.Parameters.AddWithValue("@leiras", h.Leiras);
            parancs.Parameters.AddWithValue("@frissites", h.FrissitesIdeje);
            parancs.Parameters.AddWithValue("@szakacsId", h.SzakacsId);
            parancs.Parameters.AddWithValue("@id", id);
            int sorok = parancs.ExecuteNonQuery();
            kapcsolat.Close();

            if (sorok == 0)
            {
                return "Nincs ilyen hozzávaló.";
            }

            return "A hozzávaló módosítva.";
        }

        [HttpDelete("{id}")]
        public string HozzavaloTorles(int id)
        {
            kapcsolat.Open();
            MySqlCommand parancs = new MySqlCommand("DELETE FROM hozzavalo WHERE id = @id", kapcsolat);
            parancs.Parameters.AddWithValue("@id", id);
            int sorok = parancs.ExecuteNonQuery();
            kapcsolat.Close();

            if (sorok == 0)
            {
                return "Nincs ilyen hozzávaló.";
            }

            return "A hozzávaló törölve.";
        }
        [HttpGet("szakacs/{id}")]
        public object? SzakacsAdatai(int id)
        {
            kapcsolat.Open();
            MySqlCommand parancs = new MySqlCommand("SELECT name, email FROM szakacs WHERE id = @id", kapcsolat);
            parancs.Parameters.AddWithValue("@id", id);
            MySqlDataReader olvaso = parancs.ExecuteReader();

            if (!olvaso.Read())
            {
                olvaso.Close();
                kapcsolat.Close();
                return null;
            }

            string nev = olvaso.GetString("name");
            string email = olvaso.GetString("email");

            olvaso.Close();
            kapcsolat.Close();
            return new { nev = nev, email = email };
        }

        [HttpGet("szakacs/{id}/hozzavalok")]
        public object? SzakacsHozzavaloi(int id)
        {
            string nev = "";
            bool vanSzakacs = false;
            List<object> hozzavalok = new List<object>();

            kapcsolat.Open();
            MySqlCommand parancs = new MySqlCommand(
                "SELECT szakacs.name, hozzavalo.ingredientName, hozzavalo.quantity " +
                "FROM szakacs LEFT JOIN hozzavalo ON hozzavalo.szakacsId = szakacs.id " +
                "WHERE szakacs.id = @id", kapcsolat);
            parancs.Parameters.AddWithValue("@id", id);
            MySqlDataReader olvaso = parancs.ExecuteReader();

            while (olvaso.Read())
            {
                vanSzakacs = true;
                nev = olvaso.GetString("name");

                if (!olvaso.IsDBNull(olvaso.GetOrdinal("ingredientName")))
                {
                    string hozzavaloNev = olvaso.GetString("ingredientName");
                    string mennyiseg = olvaso.GetString("quantity");
                    hozzavalok.Add(new { hozzavaloNev = hozzavaloNev, mennyiseg = mennyiseg });
                }
            }

            olvaso.Close();
            kapcsolat.Close();

            if (!vanSzakacs)
            {
                return null;
            }

            return new { szakacsNev = nev, hozzavalok = hozzavalok };
        }

        [HttpGet("szamlalas")]
        public int OsszesHozzavaloSzama()
        {
            kapcsolat.Open();
            MySqlCommand parancs = new MySqlCommand("SELECT COUNT(*) FROM hozzavalo", kapcsolat);
            int darab = Convert.ToInt32(parancs.ExecuteScalar());
            kapcsolat.Close();
            return darab;
        }

        [HttpGet("szakacs/{id}/szamlalas")]
        public int SzakacsHozzavaloinakSzama(int id)
        {
            kapcsolat.Open();
            MySqlCommand parancs = new MySqlCommand("SELECT COUNT(*) FROM hozzavalo WHERE szakacsId = @id", kapcsolat);
            parancs.Parameters.AddWithValue("@id", id);
            int darab = Convert.ToInt32(parancs.ExecuteScalar());
            kapcsolat.Close();
            return darab;
        }
    }
}