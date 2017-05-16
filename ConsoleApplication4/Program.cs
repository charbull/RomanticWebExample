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
using System.Collections.ObjectModel;

namespace ConsoleApplication4
{
    class Program
    {

        static IEntityContext context;
        static ITripleStore store;

        static void Main(string[] args)
        {

            initContextWithStardog();
            //testKnows();
            //initContextWithStore();
            testPersonInsert();


            //IPerson tim =  createPerson("Tim-Berners-Lee", "Tim", "Berners-Lee");
            //IPerson John = createPerson("Charbel", "Charbel", "CK");
            //store.SaveToFile("C:\\output.trig", new TriGWriter());
            //store.SaveToFile("C:\\output.nq", new NQuadsWriter(tim.Friends = new Collection<IPerson>();
            //tim.Friends.Add(John);

            //            context.Commit();
           
            // Console.Read();
        }

        public static void testPersonInsert()
        {
            IPerson tim =  createPerson("Tim-Charbel-Lee", "Charbel", "Berners-Lee");
           
            store.SaveToFile("C:\\output.trig", new TriGWriter());
            store.SaveToFile("C:\\output.nq", new NQuadsWriter());
        }


        public static void testKnows()
        {
            // create an entity
            IPerson person = context.Create<IPerson>(new Uri("http://se.com#" + "Charbel"));

            // set some properties
            person.Name = "Charbel" ;
            person.LastName = "lastName";

            IPerson person2 = context.Create<IPerson>(new Uri("http://se.com#" + "Charbel2"));

            // set some properties
            person2.Name = "Charbel2";
            person2.LastName = "lastName2";

            person.Friends.Add(person2);

            context.Commit();
            store.SaveToFile("C:\\output.trig", new TriGWriter());
            store.SaveToFile("C:\\output.nq", new NQuadsWriter());



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
            contextFactory.WithMetaGraphUri(new Uri("http://se.com"));
            context = contextFactory.CreateContext();


            //This saveToFile only works if we are not using a persistant store like Stardog or else
            store.SaveToFile("C:\\output.trig", new TriGWriter());
            store.SaveToFile("C:\\output.nq", new NQuadsWriter());
        }


        public static void initContextWithStardog()
        {
            var contextFactory = new EntityContextFactory();
            contextFactory.WithMappings((MappingBuilder builder) =>
            {
                builder.FromAssemblyOf<IPerson>();
            });
            
            store = StoresConfigurationSection.Default.CreateStore("dotnet2");
            contextFactory.WithDotNetRDF(store);
            contextFactory.WithMetaGraphUri(new Uri("http://se.com"));
            context = contextFactory.CreateContext();
        }


        public static IPerson createPerson(string personId, string name, string lastName)
        {
            // create an entity
            IPerson person = context.Create<IPerson>(new Uri("http://se.com#" + personId));

            // set some properties
            person.Name = name;
            person.LastName = lastName;

            // commit data to store
            context.Commit();
            //
            return person;
        }

        public static IPerson loadPerson(string personId)
        {
           return context.Load<IPerson>(new Uri("http://se.com#" + personId));
        }

    }
}
