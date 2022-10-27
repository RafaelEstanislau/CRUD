sap.ui.define([
    "sap/ui/base/ManagedObject",
], function (
    ManagedObject,
) {
    "use strict";
    return ManagedObject.extend("sap.ui.demo.walkthrough.controller.RepositorioDeAPI", {

        //ObterTodosOsLivros
        ObterTodosOsLivros: function () {
            let livrosObtidos = fetch("https://localhost:7012/livros")
                .then((response) => response.json())
                .then(data => livrosObtidos = data);
            return livrosObtidos;
        },

        //BuscarLivroPorId
        BuscarLivroPorId: function (idLivroBuscado) {
            var livroBuscado = fetch(`https://localhost:7012/livros/${idLivroBuscado}`)
                .then((response) => response.json())
                .then(data => livroBuscado = data)
            return livroBuscado;

        },

        //SalvarLivro
        SalvarLivro: async function (livroASerSalvo) {

        var livroModelo = livroASerSalvo.getData();
        var corpoDoLivro = JSON.stringify({
            id: livroModelo.id,
            autor: livroModelo.autor,
            titulo: livroModelo.titulo,
            editora: livroModelo.editora,
            lancamento: livroModelo.lancamento,
        })
        var metodoDaAPI;
        var URL;
            if (!!livroModelo.id) {
                metodoDaAPI = 'PUT';
                URL = `https://localhost:7012/livros/${livroModelo.id}`;

            } else {
                metodoDaAPI = 'POST';
                URL = 'https://localhost:7012/livros';
            };
            var livro;
            await fetch(URL, {
                    headers: {
                        "Content-Type": "application/json; charset=utf-8"
                    },
                    method: metodoDaAPI,
                    body: corpoDoLivro,
                })
                .then((response) => response.json())
                .then(data => livro = data)
            return livro;
        },

        AtualizarLivro: function(livro, idDoLivro){

        }
    });
});