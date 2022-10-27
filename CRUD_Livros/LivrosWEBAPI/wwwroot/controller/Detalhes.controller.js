sap.ui.define([
	"sap/ui/core/mvc/Controller",
	"sap/ui/core/routing/History",
	"sap/ui/model/json/JSONModel",
	"sap/m/MessageBox",
	"sap/ui/demo/walkthrough/controller/RepositorioDeLivros"
], function (Controller,
	History,
	JSONModel,
	MessageBox,
	RepositorioDeLivros) {
	"use strict";
	return Controller.extend("sap.ui.demo.walkthrough.controller.Detalhes", {
		onInit: function () {
			this.getOwnerComponent();
			var oRouter = this.getOwnerComponent().getRouter();
			oRouter.getRoute("detalhes").attachPatternMatched(this._coincidirRota, this);
		},

		_coincidirRota: function (oEvent) {
			if (oEvent.getParameter("name") != "detalhes") {
				return;
			} else {
				var idTeste = window.decodeURIComponent(oEvent.getParameter("arguments").id);
				this._carregarLivros(idTeste);
			}
		},

		_carregarLivros: function (idLivroBuscado) {
			var repositorioBuscaLivro = new RepositorioDeLivros();
			var resultado = repositorioBuscaLivro.BuscarLivroPorId(idLivroBuscado);
			resultado.then(livroRetornado => {
				var oModel = new JSONModel(livroRetornado);
				this.getView().setModel(oModel, "livro")
			})
		},
		AoClicarEmVoltar: function () {
			this.getOwnerComponent().getRouter().navTo("overview", {});
		},

		AoClicarEmEditar: function () {
			var idLivro = this.getView().getModel("livro").getData().id
			this.getOwnerComponent().getRouter().navTo("editarLivro", {
				id: idLivro
			});
		},

		AoClicarEmDeletar: function () {
			let livroSelecionado = this.getView().getModel("livro").getData();
			let idASerDeletado = livroSelecionado.id;
			let oRouter = this.getOwnerComponent().getRouter();

			return MessageBox.confirm("Deseja excluir o livro?", {
				title: "Confirmação",
				emphasizedAction: sap.m.MessageBox.Action.OK,
				actions: [sap.m.MessageBox.Action.OK,
					sap.m.MessageBox.Action.CANCEL
				],
				onClose: async function (oAction) {
					if (oAction === 'OK') {
						await fetch(`https://localhost:7012/livros/${idASerDeletado}`, {
							method: 'DELETE'
						})
						oRouter.navTo("overview");
					}

				},
			});
		}
	});
});