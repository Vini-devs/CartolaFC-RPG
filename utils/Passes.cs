using CartolaFCRPG.Models;

namespace CartolaFCRPG.utils
{
    public static class Passes
    {
        public static bool RealizarPasse(
            Jogador passador,
            Jogador receptor,
            List<Jogador> adversariosNaZona,
            Posse posse,
            Random random
        )
        {
            if (passador == null || receptor == null || adversariosNaZona == null)
            {
                throw new ArgumentNullException(
                    "Os parâmetros passador, receptor e adversariosNaZona não podem ser nulos."
                );
            }

            if (passador.Time != receptor.Time)
            {
                Console.WriteLine("Erro: Jogadores não estão no mesmo time.");
                return false;
            }

            bool cruzamento = random.Next(0, 100) < 10; // 10% de chance de ser um cruzamento
            if (cruzamento)
            {
                Console.WriteLine($"{passador.Nome} realizou um cruzamento para {receptor.Nome}!");
                posse.AtualizarPosse(receptor);
                return true;
            }

            Jogador? interceptador = TentarInterceptar(adversariosNaZona, random);
            if (interceptador != null)
            {
                posse.AtualizarPosse(interceptador);
                Console.WriteLine($"{interceptador.Nome} interceptou o passe de {passador.Nome}!");
                return false;
            }

            posse.AtualizarPosse(receptor);
            Console.WriteLine($"{passador.Nome} passou a bola para {receptor.Nome}.");
            return true;
        }

        private static Jogador? TentarInterceptar(List<Jogador> adversariosNaZona, Random random)
        {
            foreach (var adversario in adversariosNaZona)
            {
                if (random.Next(0, 100) < 30) // Exemplo: 30% de chance de interceptação por adversário
                {
                    // fazer com que se o interceptador ser da zona ou do receptor ou do passador, utilizar probabilidades diferentes
                    return adversario;
                }
            }
            return null;
        }
    }
}
