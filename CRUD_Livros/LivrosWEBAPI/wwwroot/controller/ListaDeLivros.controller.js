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
			if (oEvent.getParameter(parametroNome) != rotaListaDeLivros) {
				return;
			} else {
				this._carregarLivros();
			}
		},

		_carregarLivros: function () {
			let repositorioBuscaLivros = new RepositorioDeLivros()
			let resultado = repositorioBuscaLivros.ObterTodosOsLivros();
			resultado.then(lista => {
				let oModel = new JSONModel(lista);
				this.getView().setModel(oModel, "listaDeLivros")
			})
		},

		AoClicarEmLivro: function (evento) {
			const detalhesDoLivro = "detalhes";
			const idDoLivroClicado = evento.getSource().getBindingContext("listaDeLivros").getProperty('id');
			this._navegarParaRota(detalhesDoLivro, idDoLivroClicado)
		},

		AoClicarEmCadastrar: function () {
			const rotaDeCadastro = "cadastrarLivro";
			let parametroDaRota = null;
			this._navegarParaRota(rotaDeCadastro, parametroDaRota);
		},

		AoProcurar: function (oEvent) {
			let livrosBuscados = [];
			let sQuery = oEvent.getParameter("query");
			if (sQuery) {
				livrosBuscados.push(new Filter("titulo", FilterOperator.Contains, sQuery));
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