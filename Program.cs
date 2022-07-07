class CalculadorDeImpostos
{
    static double ICMS = 18;
    static double IPI = 4;
    static double PIS = 1.86;
    static double COFINS = 8.54;
    public static double ValorEnergetico = 4.50;

    public static double CalcularTotal(int quantidade_de_energeticos)
    {
        double valorBase = quantidade_de_energeticos * ValorEnergetico;
        double porcento = valorBase / 100;
        return valorBase + CalcularICMS(quantidade_de_energeticos) + CalcularCOFINS(quantidade_de_energeticos)
        + CalcularIPI(quantidade_de_energeticos) + CalcularPIS(quantidade_de_energeticos);
    }
    public static double CalcularTotalImpostos(int quantidade_de_energeticos)
    {
        return CalcularICMS(quantidade_de_energeticos) + CalcularCOFINS(quantidade_de_energeticos)
        + CalcularIPI(quantidade_de_energeticos) + CalcularPIS(quantidade_de_energeticos);
    }
    public static double CalcularICMS(int quantidade_de_energeticos)
    {
        double valorBase = quantidade_de_energeticos * ValorEnergetico;
        double porcento = valorBase / 100;
        return (porcento * ICMS);
    }
    public static double CalcularIPI(int quantidade_de_energeticos)
    {
        double valorBase = quantidade_de_energeticos * ValorEnergetico;
        double porcento = valorBase / 100;
        return (porcento * IPI);
    }
    public static double CalcularCOFINS(int quantidade_de_energeticos)
    {
        double valorBase = quantidade_de_energeticos * ValorEnergetico;
        double porcento = valorBase / 100;
        return (porcento * COFINS);
    }
    public static double CalcularPIS(int quantidade_de_energeticos)
    {
        double valorBase = quantidade_de_energeticos * ValorEnergetico;
        double porcento = valorBase / 100;
        return (porcento * PIS);
    }
}

public class Comprador
{
    public double ICMS;
    public double IPI;
    public double PIS;
    public double COFINS;
    public double Total;
    public double TotalImpostos;
    public double TotalLimpo;
    public double TotalDescontado;
    public double Desconto;
    public int QuantidadeComprada;
    public string Nome;
    public Comprador(string _nome, int _quantidade)
    {
        Nome = _nome;
        QuantidadeComprada = _quantidade;
        ICMS = CalculadorDeImpostos.CalcularICMS(QuantidadeComprada);
        IPI = CalculadorDeImpostos.CalcularIPI(QuantidadeComprada);
        PIS = CalculadorDeImpostos.CalcularPIS(QuantidadeComprada);
        COFINS = CalculadorDeImpostos.CalcularCOFINS(QuantidadeComprada);
        Total = CalculadorDeImpostos.CalcularTotal(QuantidadeComprada);
        TotalImpostos = CalculadorDeImpostos.CalcularTotalImpostos(QuantidadeComprada);
        TotalLimpo = CalculadorDeImpostos.ValorEnergetico * QuantidadeComprada;
        Desconto = 0;
        TotalDescontado = Total;
    }

    public void AplicarDesconto()
    {
        if (QuantidadeComprada > 400) Desconto = 30;
        else if (QuantidadeComprada > 200) Desconto = 20;
        else if (QuantidadeComprada > 100) Desconto = 10;
        TotalDescontado = Total - ((Total / 100) * Desconto);
    }

    public override string ToString()
    {
        return $"Cliente: {Nome}; \n ICMS: {ICMS.ToString("N2")}; IPI: {IPI.ToString("N2")}; PIS: {PIS.ToString("N2")}; COFINS: {COFINS.ToString("N2")}; Total: {Total.ToString("N2")}; Desconto: {Desconto};  Total Descontado: {TotalDescontado.ToString("N2")}";
    }
}


class Program
{
    public static void Main(string[] args)
    {
        bool loop = true;
        List<Comprador> compradores = new List<Comprador>();
        while (loop)
        {
            Console.WriteLine("Digite o nome do comprador e quantidade seguindo o padrão NOME_COMPRADOR; QUANTIDADE; \nPara ver os resultados digite C\n");
            var input = Console.ReadLine();
            if (input.ToLower() == "c" || input == null) break;
            var input_separado = input.Split(";");
            input_separado = input_separado.Select(input => input.Trim()).ToArray();
            int quantidade;
            var teste_double = Int32.TryParse(input_separado[1], out quantidade);
            if (!teste_double)
            {
                Console.WriteLine("Voce digitou a quantidade errado!");
                continue;
            }
            Comprador tempComprador = new Comprador(input_separado[0], quantidade);
            compradores.Add(tempComprador);
        }
        double totalImpostos = 0;
        double totalProdutos = 0;
        double totalDesconto = 0;
        foreach (var Comprador in compradores)
        {
            Comprador.AplicarDesconto();
            Console.WriteLine(Comprador.ToString());
            totalImpostos += Comprador.TotalImpostos;
            totalProdutos += Comprador.TotalLimpo;
            totalDesconto += Comprador.TotalDescontado;
        }
        double totalGeral = totalImpostos + totalProdutos;

        Console.WriteLine($"Total impostos: {totalImpostos}");
        Console.WriteLine($"Total mercadoris: {totalProdutos}");
        Console.WriteLine($"Total geral: {totalGeral}");
        Console.WriteLine($"Total de desconto: {(totalGeral - totalDesconto).ToString("N2")}");
    }
}