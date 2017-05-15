using RomanticWeb;
using RomanticWeb.Mapping;
using RomanticWeb.DotNetRDF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Program
    {

        static IEntityContext context;

        static void Main(string[] args)
        {

            initContext();
            createPerson("Tim-Berners-Lee", "Tim", "Berners-Lee");
            var tim = loadPerson("Tim-Berners-Lee");
            
            Console.WriteLine(tim.Id);
            Console.Read();
        }

        public static void initContext()
        {
            var contextFactory = new EntityContextFactory();
            contextFactory.WithMappings((MappingBuilder builder) =>
            {
                builder.FromAssemblyOf<IPerson>();
            });

            var dnrTripleStore = new VDS.RDF.TripleStore();
            contextFactory.WithDotNetRDF(dnrTripleStore);
            contextFactory.WithMetaGraphUri(new Uri("http://example.com/data#"));
            context = contextFactory.CreateContext();
        }


        public static void createPerson(string personId, string name, string lastName)
        {
            // create an entity
            var person = context.Create<IPerson>(new Uri("http://example.com/data#"+personId));

            // set some properties
            person.Name = name;
            person.LastName = lastName;

            // commit data to store
            context.Commit();
        }

        public static IPerson loadPerson(string personId)
        {
           return context.Load<IPerson>(new Uri("http://example.com/data#" + personId));
        }

    }
}
