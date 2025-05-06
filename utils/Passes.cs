using CartolaFCRPG.Models;
using CartolaFCRPG.utils;

namespace CartolaFCRPG.utils
{
    public class Passes
    {
        private readonly Posse _posse;
        private readonly Random _random = new Random();

        public Passes(Posse posse)
        {
            _posse = posse;
        }

        public bool RealizarPasse(Jogador passador, Jogador receptor, List<Jogador> adversariosNaZona)
        {
            if (passador == null || receptor == null || adversariosNaZona == null)
            {
                throw new ArgumentNullException("Os parâmetros passador, receptor e adversariosNaZona não podem ser nulos.");
            }
            
            if (passador.Time != receptor.Time)
            {
                Console.WriteLine("Erro: Jogadores não estão no mesmo time.");
                return false;
            }

            bool cruzamento = _random.Next(0, 100) < 10; // 10% de chance de ser um cruzamento
            if (cruzamento)
            {
                Console.WriteLine($"{passador.Nome} realizou um cruzamento para {receptor.Nome}!");
                _posse.AtualizarPosse(receptor);
                return true;
            }

            Jogador? interceptador = TentarInterceptar(adversariosNaZona);
            if (interceptador != null)
            {
                _posse.AtualizarPosse(interceptador);
                Console.WriteLine($"{interceptador.Nome} interceptou o passe de {passador.Nome}!");
                return false;
            }

            _posse.AtualizarPosse(receptor);
            Console.WriteLine($"{passador.Nome} passou a bola para {receptor.Nome}.");
            return true;
        }

        private Jogador? TentarInterceptar(List<Jogador> adversariosNaZona)
        {
            foreach (var adversario in adversariosNaZona)
            {
                if (_random.Next(0, 100) < 30) // Exemplo: 30% de chance de interceptação por adversário
                {
                    // fazer com que se o interceptador ser da zona ou do receptor ou do passador, utilizar probabilidades diferentes
                    return adversario; 
                }
            }
            return null;
        }
    }
}
