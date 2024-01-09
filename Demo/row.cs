namespace Demo.Controllers
{
    public class Row
    {
        private int kayit;
        private DateTime baslangic;
        private DateTime bitis;
        private string statu;
        private TimeSpan sure;
        private string neden;

        public Row()
        {
            kayit = 0;
            baslangic = new DateTime();
            bitis = new DateTime();
            statu = "";
            neden = "";
        }

        public Row(int kayit, DateTime baslangic, DateTime bitis,TimeSpan sure ,string statu, string neden)
        {
            this.kayit = kayit;
            this.baslangic = baslangic;
            this.bitis = bitis;
            this.statu = statu;
            this.neden = neden;
            this.sure = sure;
        }

        public int Kaydet
        {
            get { return kayit; }
            set { kayit = value; }
        }
        public TimeSpan Sure
        {
            get { return sure; }
            set { sure = value; }
        }

        public DateTime Baslangic
        {
            get { return baslangic; }
            set { baslangic = value; }
        }

        public DateTime Bitis
        {
            get { return bitis; }
            set { bitis = value; }
        }

        public string Statu
        {
            get { return statu; }
            set { statu = value; }
        }

        public string Neden
        {
            get { return neden; }
            set { neden = value; }
        }
    }

}
