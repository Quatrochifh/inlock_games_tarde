using senai.inlock.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Interfaces
{
    interface IJogosRepository
    {
        List<JogosDomain> ListarTodos();
        JogosDomain BuscarPorId(int idJogo);
        void Cadastrar(JogosDomain novoJogo);
        void AtualizarIdUrl(int idJogo, JogosDomain JogoAtualizado);
        void Deletar(int idJogo);

    }
}
