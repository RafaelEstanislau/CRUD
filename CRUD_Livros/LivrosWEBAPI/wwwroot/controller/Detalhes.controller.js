sap.ui.define([
	"sap/ui/core/mvc/Controller",
	"sap/ui/model/json/JSONModel",
	"sap/m/MessageBox",
	"sap/ui/demo/walkthrough/controller/RepositorioDeLivros",
	"sap/ui/model/resource/ResourceModel"
], function (Controller,
	JSONModel,
	MessageBox,
	RepositorioDeLivros,
	ResourceModel) {
	"use strict";
	const nomeModelo = "livro";
	const caminhoDeDetalhes = "sap.ui.demo.walkthrough.controller.Detalhes"
	return Controller.extend(caminhoDeDetalhes, {
		onInit: function () {
			const rotaDeDetalhes = "detalhes";
			let roteador = this
				.getOwnerComponent()
				.getRouter();
			roteador
				.getRoute(rotaDeDetalhes)
				.attachPatternMatched(this._coincidirRotaDeDetalhes, this);
			var i18nModel = new ResourceModel({
				bundleName: "sap.ui.demo.walkthrough.i18n.i18n"
			});
			this.getView().setModel(i18nModel, "i18n");
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

		_coincidirRotaDeDetalhes: function (evento) {
			this._processarEvento(() => {
				return this._carregarLivros(window.decodeURIComponent(evento.getParameter("arguments").id));
			})
		},

		_carregarLivros: function (idLivroBuscado) {
			let _repositorioLivro = new RepositorioDeLivros;
			let livroBuscado = _repositorioLivro.BuscarLivroPorId(idLivroBuscado);

			return livroBuscado.then(livroRetornado => {
				let modelo = new JSONModel(livroRetornado);
				this.getView().setModel(modelo, nomeModelo);
			})
		},

		AoClicarEmVoltar: function () {
			const rotaDaLista = "listaDeLivros";
			this._processarEvento(() => {
				this._navegarParaRota(rotaDaLista, null);
			})
		},

		AoClicarEmEditar: function () {
			const rotaEditar = "editarLivro";
			let livro = this.getView().getModel(nomeModelo).getData();
			this._processarEvento(() => {
				this._navegarParaRota(rotaEditar, livro.id);
			})
		},

		AoClicarEmDeletar: function () {
			let livroASerExcluido = this.getView().getModel(nomeModelo);
			this._processarEvento(() => {
				this._confirmarExclusaoDeLivro(livroASerExcluido);
			})
		},

		_confirmarExclusaoDeLivro: function (livroASerExcluido) {
			const rotaDaLista = "listaDeLivros";
			const modeloi18n = "i18n";
			const confirmacaoDeExclusao = this.getView().getModel(modeloi18n).getResourceBundle().getText("mensagemConfirmacaoExclusao");
			let _repositorioLivro = new RepositorioDeLivros;

			MessageBox.confirm(confirmacaoDeExclusao, {
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

			(id !== null) ?
			rota.navTo(nomeDaRota, {
				"id": id
			}): rota.navTo(nomeDaRota)
		}
	});
});