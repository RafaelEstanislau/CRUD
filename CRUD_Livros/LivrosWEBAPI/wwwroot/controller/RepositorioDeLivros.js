sap.ui.define([
    "sap/ui/base/ManagedObject",
], function (
    ManagedObject,
) {
    "use strict";
    const urlAPI = "https://localhost:7012/livros/"
    const caminhoDoRepositorio = "sap.ui.demo.walkthrough.controller.RepositorioDeLivros"
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
            let livroModelo = livroASerSalvo;
            let livroRetorno;
            let erroDeRequisicao = false;
            await fetch(urlAPI, {
                    headers: {
                        "Content-Type": "application/json; charset=utf-8"
                    },
                    method: metodoSalvar,
                    body: JSON.stringify({
                        autor: livroModelo.autor,
                        titulo: livroModelo.titulo,
                        editora: livroModelo.editora,
                        lancamento: livroModelo.lancamento,
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
            let livroModelo = livroASerAtualizado;
            let livroRetorno;
            let erroDeRequisicao = false;
            await fetch(`${urlAPI}${livroModelo.id}`, {
                    headers: {
                        "Content-Type": "application/json; charset=utf-8"
                    },
                    method: metodoEditar,
                    body: JSON.stringify({
                        id: livroModelo.id,
                        autor: livroModelo.autor,
                        titulo: livroModelo.titulo,
                        editora: livroModelo.editora,
                        lancamento: livroModelo.lancamento,
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