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

			inputs.forEach(function (input) {
				erroDeValidacao = _validacao.validarCampo(input) || erroDeValidacao;
			}, this);

			if (!erroDeValidacao) {
				if (!!livroASerSalvo.id) {
					this._editarLivro()
				} else {
					this._criarLivro()
				};
			} else {
				MessageBox.alert("Todos os campos devem ser preenchidos");
			}
		},
		_criarLivro: function () {
			var livroASerCriado = this.getView().getModel("livro").getData();
			return MessageBox.confirm("Deseja concluir o cadastro?", {
				title: "Confirmação",
				emphasizedAction: sap.m.MessageBox.Action.OK,
				actions: [sap.m.MessageBox.Action.OK,
					sap.m.MessageBox.Action.CANCEL
				],
				onClose: async function (oAction) {
					if (oAction === 'OK') {
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

						let oRouter = this.getOwnerComponent().getRouter();
						oRouter.navTo("detalhes", {
							id: livroASerCriado.id
						});
					}
				},
			})
		},
		_editarLivro: function () {
			var livroASerEditado = this.getView().getModel("livro").getData();
			MessageBox.confirm("Deseja concluir a edição?", {
				title: "Confirmação",
				emphasizedAction: sap.m.MessageBox.Action.OK,
				actions: [sap.m.MessageBox.Action.OK,
					sap.m.MessageBox.Action.CANCEL
				],
				onClose: async function (oAction) {
					if (oAction === 'OK') {
						await fetch(`https://localhost:7012/livros/${livroASerEditado.id}`, {
							headers: {
								"Content-Type": "application/json; charset=utf-8"
							},
							method: 'PUT',
							body: JSON.stringify({
								id: livroASerEditado.id,
								autor: livroASerEditado.autor,
								titulo: livroASerEditado.titulo,
								editora: livroASerEditado.editora,
								lancamento: livroASerEditado.lancamento,
							})
						})
						let oRouter = this.getOwnerComponent().getRouter();
						oRouter.navTo("detalhes", {
							id: livroASerEditado.id
						});
					}
				},
			})
		}
	});
});