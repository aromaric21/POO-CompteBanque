using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp3
{
    internal interface ICompte
    {
        protected double Solde { set;}
        protected string Proprietaire { get; set;}
        protected List<double> ListOperations {  get; set;}

    }
}
