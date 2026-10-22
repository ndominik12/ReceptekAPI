namespace ReceptekApi.Models
{
    public class Hozzavalo
    {
        public int Id { get; set; }
        public string HozzavaloNev { get; set; } = "";
        public string Mennyiseg { get; set; } = "";
        public string Leiras { get; set; } = "";
        public DateTime HozzaadasIdeje { get; set; }
        public DateTime FrissitesIdeje { get; set; }
        public int SzakacsId { get; set; }
    }
}