using AutoMapper;
using CT.Core.Domain;
using CT.Core.Shared.ModelsViews.Usuario;
using CT.Manager.Interfaces.Managers;
using CT.Manager.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.Manager.Implementation;

public class UsuarioManager : IUsuarioManager
{
    private readonly IUsuarioRepository _repository;
    private readonly IMapper _mapper;
    private readonly IJwtService _jwt;

    public UsuarioManager(IUsuarioRepository repository, IMapper mapper, IJwtService jwt)
    {
        _repository = repository;
        _mapper = mapper;
        _jwt = jwt;
    }

    public async Task<IEnumerable<UsuarioView>> GetAsync()
    {
        return _mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioView>>(await _repository.GetAsync());
    }

    public async Task<UsuarioView> GetAsync(string login)
    {
        return _mapper.Map<UsuarioView>(await _repository.GetAsync(login));
    }

    public async Task<UsuarioView> InsertAsync(NovoUsuario novoUsuario)
    {
        var usuario = _mapper.Map<Usuario>(novoUsuario);
        ConverteSenhaEmHash(usuario);
        return _mapper.Map<UsuarioView>(await _repository.InsertAsync(usuario));
    }

    private static void ConverteSenhaEmHash(Usuario usuario)
    {
        var passwordHasher = new PasswordHasher<Usuario>();
        usuario.Senha = passwordHasher.HashPassword(usuario, usuario.Senha);
    }

    public async Task<UsuarioView> UpdateMedicoAsync(Usuario usuario)
    {
        ConverteSenhaEmHash(usuario);
        return _mapper.Map<UsuarioView>(await _repository.UpdateAsync(usuario));
    }

    public async Task<UsuarioLogado> ValidaUsuarioEGeraTokenAsync(Usuario usuario)
    {
        var usuarioConsultado = await _repository.GetAsync(usuario.Login);
        if (usuarioConsultado == null)
        {
            return null;
        }
        if (await ValidaEAtualizaHashAsync(usuario, usuarioConsultado.Senha))
        {
            var usuarioLogado = _mapper.Map<UsuarioLogado>(usuarioConsultado);
            usuarioLogado.Token = _jwt.GerarToken(usuarioConsultado);
            return usuarioLogado;
        }
        return null;
    }

    private async Task<bool> ValidaEAtualizaHashAsync(Usuario usuario, string hash)
    {
        var passwordHasher = new PasswordHasher<Usuario>();
        var status = passwordHasher.VerifyHashedPassword(usuario, hash, usuario.Senha);
        switch (status)
        {
            case PasswordVerificationResult.Failed:
                return false;

            case PasswordVerificationResult.Success:
                return true;

            case PasswordVerificationResult.SuccessRehashNeeded:
                await UpdateMedicoAsync(usuario);
                return true;

            default:
                throw new InvalidOperationException();
        }
    }
}
