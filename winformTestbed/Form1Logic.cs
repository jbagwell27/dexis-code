using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winformTestbed
{
    class Form1Logic
    {


        
    }

    class Form2Logic
    {

        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        Form2 form;

        public Form2Logic()
        {
            form = new Form2();
            form.Show();
        }



    }
}
