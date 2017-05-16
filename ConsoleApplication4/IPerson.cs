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
    [Class("http://se.com#Person")]
    public interface IPerson : IEntity
    {
        [Property("http://se.com/givenName")]
        string Name { get; set; }

        [Property("http://se.com/familyName")]
        string LastName { get; set; }

        [Property("http://se.com/knows")]
        IList<IPerson> Friends { get; set; }
    }
}
