using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kasir
{
    class Item
    {
        public int id;
        public string nama;
        public int diskon;
        public bool buy1get1; 
        public bool buy2get3;
        public int harga;

        public Item()
        {
        }

        public Item(int id, string nama, int diskon, bool buy1get1, bool buy2get3, int harga)
        {
            this.id = id;
            this.nama = nama;
            this.diskon = diskon;
            this.buy1get1 = buy1get1;
            this.buy2get3 = buy2get3;
            this.harga = harga;
        }
    }
}
