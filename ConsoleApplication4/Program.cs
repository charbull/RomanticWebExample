using RomanticWeb;
using RomanticWeb.Mapping;
using RomanticWeb.DotNetRDF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF;
using VDS.RDF.Writing;
using VDS.RDF.Storage;
using VDS.RDF.Storage.Management;
using RomanticWeb.DotNetRDF.Configuration;

namespace ConsoleApplication4
{
    class Program
    {

        static IEntityContext context;
        static ITripleStore store;

        static void Main(string[] args)
        {

            initContextWithStore();
            createPerson("Tim-Berners-Lee", "Tim", "Berners-Lee");
            var tim = loadPerson("Tim-Berners-Lee");
            
            Console.WriteLine(tim.Id);
           // Console.Read();
        }

        public static void initContext()
        {
            var contextFactory = new EntityContextFactory();
            contextFactory.WithMappings((MappingBuilder builder) =>
            {
                builder.FromAssemblyOf<IPerson>();
            });

            store = new TripleStore();
            contextFactory.WithDotNetRDF(store);
            contextFactory.WithMetaGraphUri(new Uri("http://example.com/data#"));
            context = contextFactory.CreateContext();
        }


        public static void initContextWithStore()
        {
            var contextFactory = new EntityContextFactory();
            contextFactory.WithMappings((MappingBuilder builder) =>
            {
                builder.FromAssemblyOf<IPerson>();
            });

            //EntityContextFactory.FromConfiguration("dotnet2")
            //                      .WithDotNetRDF("dotnet2");


            store = StoresConfigurationSection.Default.CreateStore("dotnet2");

            //store = new TripleStore();
            contextFactory.WithDotNetRDF(store);
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
            store.SaveToFile("C:\\output.trig", new TriGWriter());
            store.SaveToFile("C:\\output.nq", new NQuadsWriter());
        }

        public static IPerson loadPerson(string personId)
        {
           return context.Load<IPerson>(new Uri("http://example.com/data#" + personId));
        }

    }
}
