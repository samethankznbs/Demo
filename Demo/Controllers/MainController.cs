using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Demo.Controllers
{
    public class MainController : Controller
    {

        public List<Row> tablo1;
        public List<Row> tablo2;
        public List<Row> tablo3;

        public MainController() {

            tablo1 = new List<Row>
    {
        new Row(1, DateTime.Parse("23.05.2020 07:30"), DateTime.Parse("23.05.2020 08:30"), TimeSpan.FromMinutes(59), "URETIM", ""),
        new Row(2, DateTime.Parse("23.05.2020 08:30"), DateTime.Parse("23.05.2020 12:00"), TimeSpan.FromMinutes(210), "URETIM", ""),
        new Row(3, DateTime.Parse("23.05.2020 12:00"), DateTime.Parse("23.05.2020 13:00"), TimeSpan.FromMinutes(60), "URETIM", ""),
        new Row(4, DateTime.Parse("23.05.2020 13:00"), DateTime.Parse("23.05.2020 13:45"), TimeSpan.FromMinutes(45), "DURUS", "ARIZA"),
        new Row(5, DateTime.Parse("23.05.2020 13:45"), DateTime.Parse("23.05.2020 17:30"), TimeSpan.FromMinutes(225), "URETIM", "")
    };
            tablo2 = new List<Row>
    {
        new Row(0, DateTime.Parse("23.05.2020 10:00"), DateTime.Parse("23.05.2020 10:15"), TimeSpan.FromMinutes(15), "DURUS", "Çay Molası"),
        new Row(0, DateTime.Parse("23.05.2020 12:00"), DateTime.Parse("23.05.2020 12:30"), TimeSpan.FromMinutes(30), "DURUS", "Yemek Molası"),
        new Row(0, DateTime.Parse("23.05.2020 15:00"), DateTime.Parse("23.05.2020 15:15"), TimeSpan.FromMinutes(15), "DURUS", "Çay Molası")
    };

           


        }



        public IActionResult Index()
        {

           
            var model = new MainViewModel
            {
                Tablo1 = tablo1,
                Tablo2 = tablo2
            };




            return View(model);
        }
        public IActionResult Index1()
        {
           

             tablo3 = new List<Row>();

            int index1 = 0, index2 = 0;

            while (index1 < tablo1.Count && index2 < tablo2.Count)
            {
                if (tablo1[index1].Bitis <= tablo2[index2].Baslangic)
                {
                    tablo3.Add(tablo1[index1]);
                    index1++;
                }
                else if (tablo2[index2].Bitis <= tablo1[index1].Baslangic)
                {
                    tablo3.Add(tablo2[index2]);
                    index2++;
                }
                else
                {
                    // Çakışma durumu
                    DateTime newEndTime = tablo1[index1].Bitis < tablo2[index2].Bitis ? tablo1[index1].Bitis : tablo2[index2].Bitis;
                    newEndTime = newEndTime - tablo2[index2].Sure;
                    // Tablo1 işi
                    if(newEndTime - tablo1[index1].Baslangic!=TimeSpan.Zero)
                    {
                        tablo3.Add(new Row(
                        tablo1[index1].Kaydet,
                        tablo1[index1].Baslangic,
                        newEndTime,
                        newEndTime - tablo1[index1].Baslangic,
                        tablo1[index1].Statu,
                        tablo1[index1].Neden
                    ));
                    }
                    

                    // Tablo2 molası
                    tablo3.Add(tablo2[index2]);

                    // Güncelleme işlemleri
                    if (tablo1[index1].Bitis < tablo2[index2].Bitis)
                    {
                        tablo2[index2].Baslangic = newEndTime + tablo2[index2].Sure;
                        
                        index1++;
                    }
                    else
                    {
                        tablo1[index1].Baslangic = newEndTime + tablo2[index2].Sure;
                        tablo1[index1].Sure = tablo1[index1].Bitis - tablo1[index1].Baslangic;
                        index2++;
                    }
                }
            }

            // Tamamlanmamış verileri ekle
            while (index1 < tablo1.Count)
            {
                tablo3.Add(tablo1[index1]);
                index1++;
            }

            while (index2 < tablo2.Count)
            {
                tablo3.Add(tablo2[index2]);
                index2++;
            }

            var model = new LastViewModel
            {
                Tablo3 = tablo3
            };

            return View(model);
        }

        
       






    }
}
