using System;
using System.Collections.Generic;
using System.Text;

namespace Selenium.Framework.Core.Models
{
    public class SiteEnvironment
    {

        public string _environment { get; set; }
        public string Host { get; set; }

        public SiteEnvironment()
        {

        }

        public SiteEnvironment(string host)
        {
            this.Host = host;
        }

    }
}
