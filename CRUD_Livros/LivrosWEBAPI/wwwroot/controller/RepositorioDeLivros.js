sap.ui.define([
    "sap/ui/base/ManagedObject",
], function (
    ManagedObject,
) {
    "use strict";
    return ManagedObject.extend("sap.ui.demo.walkthrough.controller.RepositorioDeLivros", {
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
            let livroASerCriado = livroASerSalvo.getData();
            var livro;
            await fetch('https://localhost:7012/livros', {
                    headers: {
                        "Content-Type": "application/json; charset=utf-8"
                    },
                    method: 'POST',
                    body: JSON.stringify({
                        autor: livroASerCriado.autor,
                        titulo: livroASerCriado.titulo,
                        editora: livroASerCriado.editora,
                        lancamento: livroASerCriado.lancamento,
                    })
                })
                .then((response) => response.json())
                .then(data => livro = data)
            return livro;
        },

        AtualizarLivro: async function (livroASerAtualizado) {
            let livroModelo = livroASerAtualizado.getData();
            var livro;
            await fetch(`https://localhost:7012/livros/${livroModelo.id}`, {
                    headers: {
                        "Content-Type": "application/json; charset=utf-8"
                    },
                    method: 'PUT',
                    body: JSON.stringify({
                        id: livroModelo.id,
                        autor: livroModelo.autor,
                        titulo: livroModelo.titulo,
                        editora: livroModelo.editora,
                        lancamento: livroModelo.lancamento,
                    })
                })
                .then((response) => response.json())
                .then(data => livro = data)
            return livro;
        }
    });
});