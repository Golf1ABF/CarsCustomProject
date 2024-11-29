using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutokNagyFeladatű
{
    internal class Autok
    {
        public int ID { get; set; }
        public string Marka { get; set; }
        public string Tipus { get; set; }
        public int GyartasiEv { get; set; }
        public string Szin { get; set; }
        public double Fogyasztas { get; set; }
        public int Ar { get; set; }
        public string TulajdonosTipusa { get; set; }
        public string Rendszam { get; set; }

        public Autok(string sor)
        {
            var s = sor.Split(';');
            this.ID = int.Parse(s[0]);
            this.Marka = s[1];
            this.Tipus = s[2];
            this.GyartasiEv = int.Parse(s[3]);
            this.Szin = s[4];
            this.Fogyasztas = double.Parse(s[5]);
            this.Ar = int.Parse(s[6]);
            this.TulajdonosTipusa = s[7];
            this.Rendszam = s[8];
        }

        public override string ToString()
        {
            return $"{ID} {Marka} {Tipus} {GyartasiEv} {Szin} {Fogyasztas} {Ar} {TulajdonosTipusa} {Rendszam}";
        }
    }
}
