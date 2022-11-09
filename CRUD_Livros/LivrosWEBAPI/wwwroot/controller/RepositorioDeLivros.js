sap.ui.define([
    "sap/ui/base/ManagedObject",
], function (
    ManagedObject,
) {
    "use strict";
    const urlAPI = "https://localhost:7012/livros/"
    const caminhoDoRepositorio = "sap.ui.demo.walkthrough.controller.RepositorioDeLivros"
    const tipoDeConteudo = "application/json; charset=utf-8"
    return ManagedObject.extend(caminhoDoRepositorio, {
        //ObterTodosOsLivros
        ObterTodosOsLivros: function () {

            let livrosObtidos = fetch(urlAPI)
                .then((response) => response.json())
                .then(data => livrosObtidos = data);
            return livrosObtidos;
        },
        //BuscarLivroPorId
        BuscarLivroPorId: function (idLivroBuscado) {
            let livroBuscado = fetch(`${urlAPI}${idLivroBuscado}`)
                .then((response) => response.json())
                .then(data => livroBuscado = data)
            return livroBuscado;
        },
        //SalvarLivro
        SalvarLivro: async function (livroASerSalvo) {
            const metodoSalvar = 'POST';
            let livroRetorno;
            let erroDeRequisicao = false;
            await fetch(urlAPI, {
                    headers: {
                        "Content-Type": tipoDeConteudo
                    },
                    method: metodoSalvar,
                    body: JSON.stringify({
                        autor: livroASerSalvo.autor,
                        titulo: livroASerSalvo.titulo,
                        editora: livroASerSalvo.editora,
                        lancamento: livroASerSalvo.lancamento,
                    })
                })
                .then(response => {
                    if (!response.ok) {
                        erroDeRequisicao = true
                    }
                    return response.json()
                })
                .then(data => livroRetorno = data)
                if(!!erroDeRequisicao){
                    let erro = [livroRetorno.detail, livroRetorno.title]
                    throw new Error((erro.join("\r\n")).toString())
                } 
            return livroRetorno;
        },

        AtualizarLivro: async function (livroASerAtualizado) {
            const metodoEditar = 'PUT';
            let livroRetorno;
            let erroDeRequisicao = false;
            await fetch(`${urlAPI}${livroASerAtualizado.id}`, {
                    headers: {
                        "Content-Type": tipoDeConteudo
                    },
                    method: metodoEditar,
                    body: JSON.stringify({
                        id: livroASerAtualizado.id,
                        autor: livroASerAtualizado.autor,
                        titulo: livroASerAtualizado.titulo,
                        editora: livroASerAtualizado.editora,
                        lancamento: livroASerAtualizado.lancamento,
                    })
                })
                .then(response => {
                    if (!response.ok) {
                        erroDeRequisicao = true
                    }
                    return response.json()
                })
                .then(data => livroRetorno = data)
                if(!!erroDeRequisicao){
                    let erro = [livroRetorno.detail, livroRetorno.title]
                    throw new Error((erro.join("\r\n")).toString())
                } 
            return livroRetorno;
        },

        ExcluirLivro: async function (livroASerExcluido) {
            const metodoExcluir = 'DELETE';
            let livroModelo = livroASerExcluido.getData();
            let idASerDeletado = livroModelo.id;
            await fetch(`${urlAPI}${idASerDeletado}`, {
                method: metodoExcluir
            })
        }
    });
});