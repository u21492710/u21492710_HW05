using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tutorial16.ViewModels
{
    public class PersonViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool Gender { get; set; }
        public int Age { get; set; }
        public TitleViewModel Title { get; set; }

        public PersonViewModel()
        {
            Title = new TitleViewModel();
        }
    }
}