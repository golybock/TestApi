using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [EnableCors("Police1")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
    }
}
