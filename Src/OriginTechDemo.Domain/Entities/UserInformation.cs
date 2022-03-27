using OriginTechDemo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginTechDemo.Domain.Entities
{
    public class UserInformation
    {
        public int Age { get; set; }

        public int Dependents { get; set; }

        public HouseInformation House { get; set; }

        public int Income { get; set; }

        public EMaritalStatus MaritalStatus { get; set; }

        public IList<int> RiskQuestions { get; set; }

        public VehicleInformation Vehicle { get; set; }
    }
}
