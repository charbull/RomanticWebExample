using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RomanticWeb.Mapping.Model;
using RomanticWeb.Mapping.Attributes;
using RomanticWeb.Entities;

namespace ConsoleApplication4
{
    [Class("foaf", "Person")]
    [Class("foaf", "Agent")]

    public interface IPerson : IEntity
    {
        [Property("http://xmlns.com/foaf/0.1/givenName")]
        string Name { get; set; }

        [Property("http://xmlns.com/foaf/0.1/familyName")]
        string LastName { get; set; }

        [Property("http://xmlns.com/foaf/0.1/knows")]
        ICollection<IPerson> Friends { get; set; }
    }
}
