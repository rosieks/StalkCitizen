using System;
using System.Collections.Generic;

namespace StalkCitizen.Services
{
    public class CitizenModel
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cpr { get; set; }
        public string MaritalStatus { get; set; }
        public IList<AddressModel> Addresses { get; set; }
        public IList<string> Citizenships { get; set; }
    }
    
    public class AddressModel
    {
        public string City { get; set; }
    }
}