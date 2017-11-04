using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ConfigFile
{
    public class RegisterCompaniesConfig : ConfigurationSection
    {

        public static RegisterCompaniesConfig GetConfig()
        {
            return (RegisterCompaniesConfig)System.Configuration.ConfigurationManager.GetSection("RegisterCompanies") ?? new RegisterCompaniesConfig();
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

        public new Company this[string responseString]
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
        }

        protected override System.Configuration.ConfigurationElement CreateNewElement()
        {
            return new Company();
        }

        protected override object GetElementKey(System.Configuration.ConfigurationElement element)
        {
            return ((Company)element).Name;
        }
    }

    public class Company : ConfigurationElement
    {

        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get
            {
                return this["name"] as string;
            }
        }
        [ConfigurationProperty("code", IsRequired = true)]
        public string Code
        {
            get
            {
                return this["code"] as string;
            }
        }
    }

    class Program
    {

       
        static void Main(string[] args)
        {

            var config = RegisterCompaniesConfig.GetConfig();

            foreach (var item in config.Companies)
            {
                Console.WriteLine(((Company)item).Name);
            }
            Console.WriteLine();


            RegisterCompaniesConfig register = ConfigurationManager.GetSection("RegisterCompanies") as RegisterCompaniesConfig;

            for (int i = 0; i < register.Companies.Count; i++)
            {
                Console.WriteLine(register.Companies[i].Code);
            }

           

            Console.ReadKey();
        }
    }
}
