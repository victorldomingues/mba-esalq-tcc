using System.Text.RegularExpressions;

namespace Api.Cadastro.Dominio.ClienteAgregado.ObjetosValor;

public class Cpf
{
    public Cpf(string numero)
    {
        
        Numero = LimparCpf(numero);
        Validar();
    }

    public string Numero { get; protected set; }
    public bool Valido { get; private set; }
    
    private void Validar()
    {
        Valido = ValidarCpf(Numero);
    }
    
    /// <summary>
    /// Método que valida CPF
    /// fonte Macoratti https://macoratti.net/11/09/c_val1.htm 
    /// </summary>
    /// <param name="cpf"></param>
    /// <returns></returns>
    private static bool ValidarCpf(string cpf)
    {
        if (string.IsNullOrEmpty(cpf)) return false;
        if (cpf == "11111111111") return false;
        int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        string tempCpf;
        string digito;
        int soma;
        int resto;
        cpf = cpf.Trim();
        cpf = cpf.Replace(".", "").Replace("-", "");
        if (cpf.Length != 11)
            return false;
        tempCpf = cpf.Substring(0, 9);
        soma = 0;

        for(int i=0; i<9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
        resto = soma % 11;
        if ( resto < 2 )
            resto = 0;
        else
            resto = 11 - resto;
        digito = resto.ToString();
        tempCpf = tempCpf + digito;
        soma = 0;
        for(int i=0; i<10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        digito = digito + resto.ToString();
        return cpf.EndsWith(digito);
    }

    public string LimparCpf(string numero) => Regex.Replace(numero, "[^0-9]", "");
}