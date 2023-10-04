using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CRUD_Interface
{
    public class ClassProduct
    {
        public ClassProduct() { }
        public ClassProduct(int id, string name )
        {
            this.id = id;
            this.name = name;
        }

        public int id { get; set; }
        public string name { get; set; }
    }
}
