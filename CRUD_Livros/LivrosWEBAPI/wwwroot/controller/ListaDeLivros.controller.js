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
			this.getOwnerComponent();
			var oRouter = this.getOwnerComponent().getRouter();
			oRouter.getRoute("overview").attachPatternMatched(this._coincidirRota, this);
		},

		_coincidirRota: function (oEvent) {
			if (oEvent.getParameter("name") != "overview") {
				return;
			} else {
				this._carregarLivros();
			}
		},

		_carregarLivros: function () {
			var repositorioBuscaLivros = new RepositorioDeLivros()
			var resultado = repositorioBuscaLivros.ObterTodosOsLivros();
			resultado.then(lista => {
				var oModel = new JSONModel(lista);
				this.getView().setModel(oModel, "listaDeLivros")
			})
		},

		AoClicarEmLivro: function (oEvent) {
			var oItem = oEvent.getSource();
			var oRouter = this.getOwnerComponent().getRouter();
			oRouter.navTo("detalhes", {
				id: window.encodeURIComponent(oItem.getBindingContext("listaDeLivros").getProperty('id'))
			});
		},

		AoClicarEmCadastrar: function () {
			this.getOwnerComponent().getRouter().navTo("cadastrarLivro");
		},

		AoProcurar: function (oEvent) {
			var livrosBuscados = [];
			var sQuery = oEvent.getParameter("query");
			if (sQuery) {
				livrosBuscados.push(new Filter("titulo", FilterOperator.Contains, sQuery));
			}
			var oList = this.byId("ListaDeLivros");
			var oBinding = oList.getBinding("items");
			oBinding.filter(livrosBuscados);
		}
	});
});