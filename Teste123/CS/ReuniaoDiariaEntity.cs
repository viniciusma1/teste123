using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teste123
{
    public class ReuniaoDiariaEntity
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string DiaAnterior { get; set; }
        public string Impedimento { get; set; }
        public string DiaHoje { get; set; }
    }
}