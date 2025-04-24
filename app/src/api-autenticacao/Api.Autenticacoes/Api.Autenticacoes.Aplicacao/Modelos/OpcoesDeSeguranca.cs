using System.Text;

namespace Api.Autenticacoes.Aplicacao.Modelos;

public class OpcoesDeSeguranca
{
    private readonly string _chave;
    
    public OpcoesDeSeguranca(string chave)
    {
        _chave = chave;
    }
    
    public byte[] Chave =>  Encoding.Default.GetBytes(Encoding.UTF8.GetString(Encoding.Default.GetBytes(_chave)));
    
}