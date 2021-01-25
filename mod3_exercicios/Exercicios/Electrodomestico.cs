namespace Exercicios
{
    public abstract class Electrodomestico
    {
        public Electrodomestico(string marca, string modelo, int largura, int altura, int profundidade)
        {
            Marca = marca;
            Modelo = modelo;
            Largura = largura;
            Altura = altura;
            Profundidade = profundidade;
            Ligado = false;
        }
        public Electrodomestico(string marca, string modelo)
        {
            Marca = marca;
            Modelo = modelo;
        }
        public string Marca { get; private set; }
        public string Modelo { get; private set; }
        public int Largura { get; }
        public int Altura { get; }
        public int Profundidade { get; }
        public bool Ligado { get; set; }
    }
}