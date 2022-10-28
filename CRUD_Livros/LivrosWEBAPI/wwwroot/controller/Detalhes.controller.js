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
			const rotaDetalhesLivro = "detalhes";

			if(evento.getParameter(parametroNome) == rotaDetalhesLivro){
				this._carregarLivros(window.decodeURIComponent(evento.getParameter("arguments").id));
			}
		},

		_carregarLivros: function (idLivroBuscado) {
			let _repositorioLivro = new RepositorioDeLivros();
			let resultado = _repositorioLivro.BuscarLivroPorId(idLivroBuscado);

			resultado.then(livroRetornado => {
				let modelo = new JSONModel(livroRetornado);
				this.getView().setModel(modelo, "livro");
			})
		},

		AoClicarEmVoltar: function () {
			const rotaDaLista = "listaDeLivros";
			this._navegarParaRota(rotaDaLista);
		
		},

		AoClicarEmEditar: function () {
			const rotaEditarLivro = "editarLivro";
			let idLivro = this.getView().getModel("livro").getData().id;
	
			this._navegarParaRota(rotaEditarLivro, idLivro);
		},

		AoClicarEmDeletar: function () {
			let livroASerExcluido = this.getView().getModel("livro");
			this._confirmarExclusaoDeLivro(livroASerExcluido);
		},

		_confirmarExclusaoDeLivro: function (livroASerExcluido) {
			MessageBox.confirm("Deseja excluir o livro?", {
				title: "Confirmação",
				emphasizedAction: sap.m.MessageBox.Action.OK,
				actions: [sap.m.MessageBox.Action.OK,
					sap.m.MessageBox.Action.CANCEL
				],
				onClose: function (confirmacao) {
					if (confirmacao === 'OK') {
						const rotaDaLista = "listaDeLivros";
						let parametroDaRota = null;
						let _repositorioLivro = new RepositorioDeLivros();
						_repositorioLivro.ExcluirLivro(livroASerExcluido);
						this._navegarParaRota(rotaDaLista, parametroDaRota)
					}
				}.bind(this)
			});
		},

		_navegarParaRota(nomeDaRota, parametroDaRota = null) {
			let rota = this.getOwnerComponent().getRouter();

			(parametroDaRota !== null) 
				? rota.navTo(nomeDaRota, {
					"id": parametroDaRota
				})
				: rota.navTo(nomeDaRota) 
		}
	});
});