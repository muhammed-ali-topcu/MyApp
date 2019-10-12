
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.WebUI.Entity
{
    public class Page //Main menu items
    {
        public int PageId { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public double Code { get; set; }// menu de item kacinci sırada olacak 
        public string Action { get; set; }
        public string Controller { get; set; }


    }
}
