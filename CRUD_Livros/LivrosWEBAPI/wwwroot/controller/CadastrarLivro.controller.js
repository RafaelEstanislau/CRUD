sap.ui.define([
	"sap/ui/core/mvc/Controller",
	"sap/ui/core/routing/History",
	"sap/m/MessageBox",
	"sap/ui/model/json/JSONModel",
	"sap/ui/core/Core",
	"sap/ui/demo/walkthrough/controller/Validacao"

], function (Controller,
	History,
	MessageBox,
	JSONModel,
	Core,
	Validacao) {
	"use strict";
	return Controller.extend("sap.ui.demo.walkthrough.controller.CadastrarLivro", {

		onInit: function () {
			var router = sap.ui.core.UIComponent.getRouterFor(this);
			router.attachRoutePatternMatched(this._coincidirRota, this);
			var tela = this.getView(),
				oMM = Core.getMessageManager();
			oMM.registerObject(tela.byId("input-titulo"), true)
			oMM.registerObject(tela.byId("input-editora"), true)
			oMM.registerObject(tela.byId("input-autor"), true)
		},
		_coincidirRota: function (oEvent) {
			if (oEvent.getParameter("name") == "editarLivro") {
				var idAEditar = window.decodeURIComponent(oEvent.getParameter("arguments").id);
				this._carregarLivros(idAEditar)
			} else {
				this.getView().setModel(new sap.ui.model.json.JSONModel({}), "livro");
			}
		},
		_carregarLivros: function (idAEditar) {
			var resultado = this._buscarLivro(idAEditar)
			resultado.then(livroRetornado => {
				var oModel = new JSONModel(livroRetornado);
				this.getView().setModel(oModel, "livro")
			})
		},
		_buscarLivro: function (idAEditar) {
			var livroBuscado = fetch(`https://localhost:7012/livros/${idAEditar}`)
				.then((response) => response.json())
				.then(data => livroBuscado = data)
			return livroBuscado;

		},
		aoClicarEmVoltar: function () {
			var oHistory = History.getInstance();
			var sPreviousHash = oHistory.getPreviousHash();

			if (sPreviousHash !== undefined) {
				window.history.go(-1);
			} else {
				var oRouter = this.getOwnerComponent().getRouter();
				oRouter.navTo("overview", {});
			}
		},

		aoClicarEmSalvar: function () {
			var livroASerSalvo = this.getView().getModel("livro").getData();
			let _validacao = new Validacao()

			var telaCadastro = this.getView(),
				inputs = [
					telaCadastro.byId("input-titulo"),
					telaCadastro.byId("input-editora"),
					telaCadastro.byId("input-autor"),
				],
				erroDeValidacao = false;
			var dataInputada = this.getView().byId("DT").getValue();
			inputs.forEach(function (input) {
				erroDeValidacao = _validacao.validarCampo(input, dataInputada) || erroDeValidacao;
			}, this);



			var oRouter = this.getOwnerComponent().getRouter();
			var corpoDoLivro = JSON.stringify({
				id: livroASerSalvo.id,
				autor: livroASerSalvo.autor,
				titulo: livroASerSalvo.titulo,
				editora: livroASerSalvo.editora,
				lancamento: livroASerSalvo.lancamento,
			})
			var metodoDaAPI;
			var URL;

			if (!erroDeValidacao) {
				MessageBox.confirm("Deseja salvar o livro?", {
					title: "Confirmação",
					emphasizedAction: sap.m.MessageBox.Action.OK,
					actions: [sap.m.MessageBox.Action.OK,
						sap.m.MessageBox.Action.CANCEL
					],
					onClose: async function (oAction) {
						if (oAction === 'OK') {
							if (!!livroASerSalvo.id) {
								metodoDaAPI = 'PUT';
								URL = `https://localhost:7012/livros/${livroASerSalvo.id}`;

							} else {
								metodoDaAPI = 'POST';
								URL = 'https://localhost:7012/livros';
							};
							var idDoLivro = await fetch(URL, {
								headers: {
									"Content-Type": "application/json; charset=utf-8"
								},
								method: metodoDaAPI,
								body: corpoDoLivro,
							})
							.then((response) => idDoLivro = response.json())
							oRouter.navTo("detalhes", {
								id: idDoLivro.id
							});
						}
					}
				})
			} else {
				MessageBox.alert("Todos os campos devem ser preenchidos");
			}
		},
	});
});