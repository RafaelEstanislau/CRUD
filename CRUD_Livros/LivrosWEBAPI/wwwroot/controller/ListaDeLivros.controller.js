sap.ui.define([
	"sap/ui/core/mvc/Controller",
	"sap/ui/model/json/JSONModel",
	"sap/ui/model/Filter",
	"sap/ui/model/FilterOperator",
	"sap/ui/demo/walkthrough/controller/RepositorioDeLivros"
], function (Controller, JSONModel, Filter, FilterOperator, RepositorioDeLivros) {
	"use strict";

	return Controller.extend("sap.ui.demo.walkthrough.controller.ListaDeLivros", {

		onInit: function () {
			let rota = sap.ui.core.UIComponent.getRouterFor(this);
			rota.attachRoutePatternMatched(this._coincidirRota, this);
		},

		_coincidirRota: function (oEvent) {
			const parametroNome = "name";
			const rotaListaDeLivros = "listaDeLivros";

			oEvent.getParameter(parametroNome) == rotaListaDeLivros 
				? this._carregarLivros()
				: (() => null);
		},

		_carregarLivros: function () {
			const nomeModelo = "listaDeLivros";
			let _repositorioLivro = new RepositorioDeLivros;
			_repositorioLivro.ObterTodosOsLivros()
				.then(lista => {
				let oModel = new JSONModel(lista);
				this.getView().setModel(oModel, nomeModelo)
			});
		},

		AoClicarEmLivro: function (evento) {
			const rotaDetalhes = "detalhes";
			const idDoLivroClicado = evento.getSource().getBindingContext("listaDeLivros").getProperty('id');
			this._navegarParaRota(rotaDetalhes, idDoLivroClicado)
		},

		AoClicarEmCadastrar: function () {
			const rotaDeCadastro = "cadastrarLivro";
			this._navegarParaRota(rotaDeCadastro, null);
		},

		aoClicarEmPesquisar: function (oEvent) {
			let livrosBuscados = [];
			let parametroPesquisa = oEvent.getParameter("query");
			if (parametroPesquisa) {
				livrosBuscados.push(new Filter("titulo", FilterOperator.Contains, parametroPesquisa));
			}
			let listaDeLivros = this.byId("ListaDeLivros");
			let oBinding = listaDeLivros.getBinding("items");
			oBinding.filter(livrosBuscados);
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