sap.ui.define([
	"sap/ui/core/mvc/Controller",
	"sap/ui/model/json/JSONModel",
	"sap/m/MessageBox",
	"sap/ui/demo/walkthrough/controller/RepositorioDeLivros"
], function (Controller,
	JSONModel,
	MessageBox,
	RepositorioDeLivros) {
	"use strict";
	return Controller.extend("sap.ui.demo.walkthrough.controller.Detalhes", {
		onInit: function () {
			let rota = sap.ui.core.UIComponent.getRouterFor(this);
			rota.attachRoutePatternMatched(this._coincidirRota, this);
		},

		_coincidirRota: function (evento) {
			const parametroNome = "name";
			const rotaDetalhes = "detalhes";

			if(evento.getParameter(parametroNome) == rotaDetalhes){
				this._carregarLivros(window.decodeURIComponent(evento.getParameter("arguments").id));
			}
		},

		_carregarLivros: function (idLivroBuscado) {
			const nomeModelo = "livro";
			let _repositorioLivro = new RepositorioDeLivros;
			let livroBuscado = _repositorioLivro.BuscarLivroPorId(idLivroBuscado);

			livroBuscado.then(livroRetornado => {
				let modelo = new JSONModel(livroRetornado);
				this.getView().setModel(modelo, nomeModelo);
			})
		},

		AoClicarEmVoltar: function () {
			const rotaDaLista = "listaDeLivros";
			this._navegarParaRota(rotaDaLista, null);
		},

		AoClicarEmEditar: function () {
			const rotaEditar = "editarLivro";
			let livro = this.getView().getModel("livro").getData();
	
			this._navegarParaRota(rotaEditar, livro.id);
		},

		AoClicarEmDeletar: function () {
			let livroASerExcluido = this.getView().getModel("livro");
			this._confirmarExclusaoDeLivro(livroASerExcluido);
		},

		_confirmarExclusaoDeLivro: function (livroASerExcluido) {
			const rotaDaLista = "listaDeLivros";
			let _repositorioLivro = new RepositorioDeLivros;

			MessageBox.confirm("Deseja excluir o livro?", {
				title: "Confirmação",
				emphasizedAction: sap.m.MessageBox.Action.OK,
				actions: [sap.m.MessageBox.Action.OK,
					sap.m.MessageBox.Action.CANCEL
				],
				onClose: function (confirmacao) {
					if (confirmacao === 'OK') {
						_repositorioLivro.ExcluirLivro(livroASerExcluido);
						this._navegarParaRota(rotaDaLista, null)
					}
				}.bind(this)
			});
		},

		_navegarParaRota(nomeDaRota, id = null) {
			let rota = this.getOwnerComponent().getRouter();

			(id !== null) 
				? rota.navTo(nomeDaRota, {
					"id": id
				})
				: rota.navTo(nomeDaRota) 
		}
	});
});