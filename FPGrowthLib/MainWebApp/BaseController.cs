using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MainWebApp {
    public class BaseController : ControllerBase {
        public IOptions<AppSettings> Setting { get; set; }

        public BaseController (IOptions<AppSettings> appSettings) {
            Setting = appSettings;
        }
    }
}