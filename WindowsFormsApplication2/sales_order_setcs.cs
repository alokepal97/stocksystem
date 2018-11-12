using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WindowsFormsApplication2
{
    class sales_order_setcs
    {


        public string name { get; set; }
        public string customer_address { get; set; }
        public string customer_city { get; set; }
        public string customer_zip { get; set; }
        public string customer_state { get; set; }
        public string customer_country { get; set; }

        /* public string in_no { get; set; }
         public string in_date { get; set; }
         public string order_no { get; set; }
         public string order_date { get; set; }
       
         public string grand_total { get; set; }*/

        public string item_code { get; set; }
        public string item_name { get; set; }
        public string qty { get; set; }
        public string unit { get; set; }
        public string price { get; set; }
        public string disc { get; set; }
        public string total { get; set; }

       
      
    }
}
