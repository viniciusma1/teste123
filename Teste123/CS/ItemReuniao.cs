using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teste123.CS
{
    public class ItemReuniao
    {
        public int IDNome { get; set; }
        public string DiaAnterior { get; set; }
        public string Impedimento { get; set; }
        public string DiaHoje { get; set; }
        public DateTime Data { get; set; }
        public string Nome { get; set; }
        public int IDResponsavel { get; set; }
        public string txtDiaAnterior { get; set; }
        public string txtImpedimento { get; set; }
        public string txtDiaHoje { get; set; }
        public bool EmailEnviado { get; set; }
    }
}