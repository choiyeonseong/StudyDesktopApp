using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTestApp
{
    public class PersonList : ObservableCollection<Person>  // List<Person> 거의 비슷
    {
        public PersonList()
        {
            this.Add(new Person("Leonardo", "Dicaprio"));
            this.Add(new Person("Walt", "Disney"));
            this.Add(new Person("Tom", "Holland"));

        }
    }
}
