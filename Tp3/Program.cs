namespace Tp3
{
    internal class Program
    {
        public abstract class Compte
        {
            public string Proprietaire { get; set; }
            protected List<Operation> listeOperations;
            public virtual decimal Solde 
            {
                get
                {
                    decimal total = 0;
                    foreach (Operation op in listeOperations)
                    {
                        if (op.TypeDeMouvement == Mouvement.Credit)
                            total += op.Montant;
                        else
                            total -= op.Montant;
                    }
                    return total;
                }
            }
            public Compte()
            {
                listeOperations = new List<Operation>();
            }
            public void Crediter(decimal montant)
            {
                Operation operation = new Operation { Montant = montant, TypeDeMouvement = Mouvement.Credit };
                listeOperations.Add(operation);
            }
            public void Crediter(decimal montant, Compte compte)
            {
                Crediter(montant);
                compte.Debiter(montant);
            }
            public void Debiter(decimal montant)
            {
                Operation operation = new Operation { Montant = montant, TypeDeMouvement = Mouvement.Credit };
                listeOperations.Add(operation);
            }
            public void Debiter(decimal montant, Compte compte)
            {
                Debiter(montant);
                compte.Crediter(montant);
            }
            public abstract void AfficherResume();
        }


        public class CompteCourant : Compte
        {
            private decimal decouvertAutorise;
            public CompteCourant(decimal decouvert)
            {
                this.decouvertAutorise = decouvert;
            }
            public override void AfficherResume()
            {
                Console.WriteLine("*******************************************");
                Console.WriteLine("Compte courant de " + Proprietaire);
                Console.WriteLine("\tSolde : " + Solde);
                Console.WriteLine("\tDécouvert autorisé : " +
                decouvertAutorise);
                Console.WriteLine("\n\nOpérations :");
                foreach (Operation operation in listeOperations)
                {
                    if (operation.TypeDeMouvement == Mouvement.Credit)
                        Console.Write("\t+");
                    else
                        Console.Write("\t-");
                    Console.WriteLine(operation.Montant);
                }
                Console.WriteLine("*******************************************");
            }
        }


        public class CompteEpargneEntreprise : Compte
        {
            private double tauxAbondement;
            public CompteEpargneEntreprise(double taux)
            {
                this.tauxAbondement = taux;
            }
            public override decimal Solde
            {
                get
                {
                    return base.Solde * (decimal)(1 + tauxAbondement);
                }
            }
            public override void AfficherResume()
            {
                Console.WriteLine("########################################################");
                Console.WriteLine("Compte épargne entreprise de " + Proprietaire);
                Console.WriteLine("\tSolde : " + Solde);
                Console.WriteLine("\tTaux : " + tauxAbondement);
                Console.WriteLine("\n\nOpérations :");
                foreach (Operation operation in listeOperations)
                {
                    if (operation.TypeDeMouvement == Mouvement.Credit)
                        Console.Write("\t+");
                    else
                        Console.Write("\t-");
                    Console.WriteLine(operation.Montant);
                }
                Console.WriteLine("########################################################");
            }
    }


        public enum Mouvement
        {
            Credit, 
            Debit
        }

        public class Operation
        {
            public Mouvement TypeDeMouvement {  get; set; }
            public decimal Montant {  get; set; }
        }


        static void Main(string[] args)
        {
            CompteCourant compteNicolas = new CompteCourant(2000) { Proprietaire = "Nicolas" };
            CompteEpargneEntreprise compteEpargneNicolas = new CompteEpargneEntreprise(0.02) 
               { Proprietaire = "Nicolas" };
            CompteCourant compteJeremie = new CompteCourant(500) { Proprietaire = "Jérémie" };

            compteNicolas.Crediter(100);
            compteNicolas.Debiter(50);

            compteEpargneNicolas.Crediter(20, compteNicolas);
            compteEpargneNicolas.Crediter(100);

            compteEpargneNicolas.Debiter(20, compteNicolas);

            compteJeremie.Debiter(500);
            compteJeremie.Debiter(200, compteNicolas);

            Console.WriteLine("Solde compte courant de " +
            compteNicolas.Proprietaire + " : " + compteNicolas.Solde);
            Console.WriteLine("Solde compte épargne de " +
            compteEpargneNicolas.Proprietaire + " : " +
            compteEpargneNicolas.Solde);
            Console.WriteLine("Solde compte courant de " +
            compteJeremie.Proprietaire + " : " + compteJeremie.Solde);
            Console.WriteLine("\n");

            Console.WriteLine("Résumé du compte de Nicolas");
            compteNicolas.AfficherResume();
            Console.WriteLine("\n");

            Console.WriteLine("Résumé du compte épargne de Nicolas");
            compteEpargneNicolas.AfficherResume();
            Console.WriteLine("\n");

        }
    }
}