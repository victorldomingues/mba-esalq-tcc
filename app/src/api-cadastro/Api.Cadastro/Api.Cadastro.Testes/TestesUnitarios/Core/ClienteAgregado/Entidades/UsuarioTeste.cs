using System.Text.RegularExpressions;
using Api.Cadastro.Dominio.ClienteAgregado.Entidades;
using Api.Cadastro.Dominio.ClienteAgregado.ObjetosValor;
using Shouldly;

namespace Api.Cadastro.Testes.TestesUnitarios.Core.ClienteAgregado.Entidades;

public class UsuarioTeste
{
    [Fact(DisplayName = "Deve construir usuario")]
    public void DeveConstruirUsuario()
    {
        var nome = "nome";
        var email = "email@teste.com";
        var numeroCpf = "928.532.020-11";
        var numeroCpfLimpo = Regex.Replace(numeroCpf, @"[^\d]", "");
        var senha = new Senha("senha");
        var cpf = new Cpf(numeroCpf);
        var usuario = new Usuario(cpf, senha, nome, email );
        usuario.Senha.ShouldBe(senha);
        usuario.Nome.ShouldBe(nome);
        usuario.Email.ShouldBe(email);
        usuario.Cpf.Numero.ShouldBe(numeroCpfLimpo);
        usuario.CriadoEm.ShouldNotBe(default);
        usuario.Situacao.ShouldBe(Situacao.Ativo);
        usuario.AtualizadoEm.ShouldBeNull();
        usuario.DeletadoEm.ShouldBeNull();
    }
}