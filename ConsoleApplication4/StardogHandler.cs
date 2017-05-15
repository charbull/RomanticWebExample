using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF;
using VDS.RDF.Query;
using VDS.RDF.Storage;
using VDS.RDF.Storage.Management;
using VDS.RDF.Storage.Management.Provisioning;
using VDS.RDF.Storage.Management.Provisioning.Stardog;

namespace ConsoleApplication4
{
    class StardogHandler
    {
        public static void deleteStore(string storeId)
        {

            StardogServer stardogServer = new StardogServer("http://localhost:5820", "admin", "admin");

            //Get the list of stores
            foreach (String store in stardogServer.ListStores())
            {
                if (store.CompareTo(storeId) == 0)
                {
                    Console.WriteLine("Store Found " + store);
                    var storeToCreate = stardogServer.GetStore(storeId);
                    if (storeToCreate != null)
                    {
                        Console.WriteLine("Store " + storeId + " found ");
                        var canBeDeleted = storeToCreate.DeleteSupported;
                        if (canBeDeleted)
                        {
                            Console.WriteLine("Deleting " + storeId);
                            stardogServer.DeleteStore(storeId);
                        }
                    }
                    Console.WriteLine(store);
                }


            }
        }


        public static void createStore(string storeId)
        {
            StardogServer stardogServer = new StardogServer("http://localhost:5820", "admin", "admin");
            IStoreTemplate iStoreTemplate = new StardogDiskTemplate(storeId);
            stardogServer.CreateStore(iStoreTemplate);
        }

        public static void stardog_Insert(string storeId)
        {
            StardogConnector connector = new StardogConnector("http://localhost:5820", storeId, "admin", "admin");
            Graph g = new Graph();
            g.LoadFromFile("C:\\Users\\SESA232537\\Downloads\\export.rdf");
            if (!connector.IsReadOnly)
            {
                connector.SaveGraph(g);
            }
            else
            {
                throw new Exception("Store is read-only");
            }
        }


        public static void queryStore(string storeId)
        {

            StardogConnector connector = new StardogConnector("http://localhost:5820", storeId, "admin", "admin");
            SparqlResultSet results = (SparqlResultSet)connector.Query("SELECT * WHERE { { GRAPH ?g { ?s ?p ?o } } UNION { ?s ?p ?o } }");


            if (results is SparqlResultSet)
            {
                //Print out the Results
                SparqlResultSet rset = (SparqlResultSet)results;
                foreach (SparqlResult result in rset)
                {
                    Console.WriteLine(result.ToString());
                }
            }


        }

    }
}
