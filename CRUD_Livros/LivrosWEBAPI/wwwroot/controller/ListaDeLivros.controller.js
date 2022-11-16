sap.ui.define([
	"sap/ui/core/mvc/Controller",
	"sap/ui/model/json/JSONModel",
	"sap/ui/model/Filter",
	"sap/ui/model/FilterOperator",
	"sap/ui/demo/walkthrough/controller/RepositorioDeLivros",
	"sap/m/MessageBox"
], function (Controller,
	JSONModel,
	Filter,
	FilterOperator,
	RepositorioDeLivros,
	MessageBox) {
	"use strict";
	const caminhoDaLista = "sap.ui.demo.walkthrough.controller.ListaDeLivros"
	return Controller.extend(caminhoDaLista, {

		onInit: function () {
			const rotaDaLista = "listaDeLivros";
			let roteador = this
				.getOwnerComponent()
				.getRouter();
			roteador
				.getRoute(rotaDaLista)
				.attachPatternMatched(this._coincidirRotaDaLista, this);
		},
		_processarEvento: function (acao) {
			try {
				var promise = acao();
				if (promise && typeof (promise["catch"]) == "function") {
					promise.catch(error => MessageBox.error(error.message));
				}
			} catch (error) {
				MessageBox.error(error.message);
			}
		},
		_coincidirRotaDaLista: function () {
			this._processarEvento(() => {
				return this._carregarLivros();
			})
		},

		_carregarLivros: function () {
			const nomeModelo = "listaDeLivros";
			let _repositorioLivro = new RepositorioDeLivros;
			return _repositorioLivro.ObterTodosOsLivros()
				.then(lista => {
					let modelo = new JSONModel(lista);
					this.getView().setModel(modelo, nomeModelo)
				});
		},

		AoClicarEmLivro: function (evento) {
			const rotaDetalhes = "detalhes";
			const idDoLivroClicado = evento.getSource().getBindingContext("listaDeLivros").getProperty('id');
			this._processarEvento(() => {
				this._navegarParaRota(rotaDetalhes, idDoLivroClicado);
			});
		},

		AoClicarEmCadastrar: function () {
			const rotaDeCadastro = "cadastrarLivro";
			this._processarEvento(() => {
				this._navegarParaRota(rotaDeCadastro, null);
			})
		},

		aoClicarEmPesquisar: function (evento) {
			const lista = "ListaDeLivros";
			const tituloDoLivro = "titulo";
			let livrosBuscados = [];
			let parametroPesquisa = evento.getParameter("query");
			this._processarEvento(() => {
				if (parametroPesquisa) {
					livrosBuscados.push(new Filter(tituloDoLivro, FilterOperator.Contains, parametroPesquisa));
				}
				let listaDeLivros = this.byId(lista);
				let oBinding = listaDeLivros.getBinding("items");
				oBinding.filter(livrosBuscados);
			})
		},

		_navegarParaRota(nomeDaRota, id = null) {
			let rota = this.getOwnerComponent().getRouter();

			(id !== null) ?
			rota.navTo(nomeDaRota, {
				"id": id
			}): rota.navTo(nomeDaRota)
		}
	});
});