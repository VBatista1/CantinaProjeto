using System;
using System.Collections.Generic;
using System.Text;

namespace Users.Models
{
    public class UserPayload
    {
        public string id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }
    }
}
