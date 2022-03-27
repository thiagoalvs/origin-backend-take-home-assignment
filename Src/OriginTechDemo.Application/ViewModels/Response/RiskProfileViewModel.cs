using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginTechDemo.Application.ViewModels
{
    public class RiskProfileViewModel
    {
        public RiskProfileViewModel(string auto, string disability, string home, string life)
        {
            this.auto = auto;
            this.disability = disability;
            this.home = home;
            this.life = life;
        }
        public string auto { get; private set; }

        public string disability { get; private set; }

        public string home { get; private set; }

        public string life { get; private set; }
    }
}
