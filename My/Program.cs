using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace My
{
    public class MyConfigSection : ConfigurationSection
    {
        
        
        [ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
        public Companies Instances
        {
            get { return (Companies)this[""]; }
            set { this[""] = value; }
        }
        

        [System.Configuration.ConfigurationProperty("Companies")]
        [ConfigurationCollection(typeof(Companies), AddItemName = "Company")]
        public Companies Companies
        {
            get
            {
                object o = this["Companies"];
                return o as Companies;
            }
        }


    }
    public class Companies : ConfigurationElementCollection
    {

        public Company this[int index]
        {
            get
            {
                return base.BaseGet(index) as Company;
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        /*public new Company this[string responseString]
        {
            get { return (Company)BaseGet(responseString); }
            set
            {
                if (BaseGet(responseString) != null)
                {
                    BaseRemoveAt(BaseIndexOf(BaseGet(responseString)));
                }
                BaseAdd(value);
            }
        }*/

        protected override ConfigurationElement CreateNewElement()
        {
            return new Company();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            //set to whatever Element Property you want to use for a key
            return ((Company)element).Name;
        }
    }

    public class Company : ConfigurationElement
    {
        //Make sure to set IsKey=true for property exposed as the GetElementKey above
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("code", IsRequired = true)]
        public string Code
        {
            get { return (string)base["code"]; }
            set { base["code"] = value; }
        }

       
    }

    class Program
    {


        static void Main(string[] args)
        {
            MyConfigSection config = ConfigurationManager.GetSection("registerCompanies") as MyConfigSection;
            foreach (Company e in config.Instances)
            {
                Console.WriteLine("Name: {0}, Code: {1}", e.Name, e.Code);
            }



            for (int i = 0; i < config.Instances.Count; i++)
            {
                Console.WriteLine(config.Instances[i].Name + " " + config.Instances[i].Code);
            }


            Console.WriteLine();

            MyConfigSection register = ConfigurationManager.GetSection("RegisterCompanies") as MyConfigSection;

            foreach (Company e in register.Companies)
            {
                Console.WriteLine("Name: {0}, Code: {1}", e.Name, e.Code);
            }
         


            for (int i = 0; i < register.Companies.Count; i++)
            {
                Console.WriteLine(register.Companies[i].Name + " " + register.Companies[i].Code);        
            }
     
           


            Console.WriteLine();

            Console.ReadKey();
        }
    }
}