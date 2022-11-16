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
        ObterTodosOsLivros: async function () {
            let livrosObtidos;
            await fetch(urlAPI)
                .then(response => {
                    if (!response.ok) {
                        const erro = response.status
                        console.log("teste reposta de erro http")
                        Promise.reject(erro)
                    }
                    return response.json()
                })
                .then(data => livrosObtidos = data)
                .catch(err => {
                    console.log("teste servidor desligado obter todos")
                    throw new Error(err)
                })
            return livrosObtidos;
        },
        //BuscarLivroPorId
        BuscarLivroPorId: async function (idLivroBuscado) {
            let livroBuscado;
            await fetch(`${urlAPI}${idLivroBuscado}`)
                .then((response) => {
                    if (!response.ok) {
                        const erro = response.status
                        throw new Error(erro)
                    }
                    return response.json()
                })
                .then(data => livroBuscado = data)
                .catch(err => {
                    console.log("pilantra")
                    throw new Error(err)
                })
            return livroBuscado;
        },
        //SalvarLivro
        SalvarLivro: async function (corpo) {
            const metodoSalvar = 'POST';
            let livroRetorno;
            let erroDeRequisicao = false;
            await fetch(urlAPI, {
                    headers: {
                        "Content-Type": tipoDeConteudo
                    },
                    method: metodoSalvar,
                    body: corpo
                })
                .then(response => {
                    if (!response.ok) {
                        erroDeRequisicao = true
                    }
                    return response.json()
                })
                .then(data => livroRetorno = data)
            if (!!erroDeRequisicao) {
                let erro = [livroRetorno.title, livroRetorno.detail, livroRetorno.erros.join("\r\n")]
                throw new Error((erro.join("\r\n")).toString())
            }
            return livroRetorno;
        },

        AtualizarLivro: async function (livroASerAtualizado, corpo) {
            const metodoEditar = 'PUT';
            let livroRetorno;
            let erroDeRequisicao = false;
            await fetch(`${urlAPI}${livroASerAtualizado.id}`, {
                    headers: {
                        "Content-Type": tipoDeConteudo
                    },
                    method: metodoEditar,
                    body: corpo
                })
                .then(response => {
                    if (!response.ok) {
                        erroDeRequisicao = true
                    }
                    return response.json()
                })
                .then(data => livroRetorno = data)
            if (!!erroDeRequisicao) {
                let erro = [livroRetorno.detail, livroRetorno.erros.join("\r\n")]
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