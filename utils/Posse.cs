using CartolaFCRPG.Models;

namespace CartolaFCRPG.utils
{
    public class Posse
    {
        public Jogador? JogadorComBola { get; private set; }
        public Time? TimeComPosse => JogadorComBola?.Time;

        public bool NoAr => JogadorComBola == null;

        public void AtualizarPosse(Jogador novoDono)
        {
            JogadorComBola = novoDono;
            Console.WriteLine($"Nova posse: {novoDono.Nome} do {novoDono.Time.Nome}");
        }

        public void PerderPossePara(Jogador adversario)
        {
            AtualizarPosse(adversario);
            Console.WriteLine($"{adversario.Nome} recuperou a bola!");
        }
    }
}
