sap.ui.define([
	"sap/ui/core/mvc/Controller",
	"sap/ui/core/routing/History",
	"sap/m/MessageBox",
	"sap/ui/model/json/JSONModel",
	"sap/ui/core/Core",
	"sap/ui/demo/walkthrough/controller/Validacao",
	"sap/ui/demo/walkthrough/controller/RepositorioDeLivros"

], function (Controller,
	History,
	MessageBox,
	JSONModel,
	Core,
	Validacao,
	RepositorioDeLivros) {
	"use strict";
	return Controller.extend("sap.ui.demo.walkthrough.controller.CadastrarLivro", {

		onInit: function () {
			let rota = sap.ui.core.UIComponent.getRouterFor(this);
			rota.attachRoutePatternMatched(this._coincidirRota, this);
			let tela = this.getView(),
				oMM = Core.getMessageManager();
			oMM.registerObject(tela.byId("input-titulo"), true)
			oMM.registerObject(tela.byId("input-editora"), true)
			oMM.registerObject(tela.byId("input-autor"), true)
		},

		_coincidirRota: function (evento) {
			if (evento.getParameter("name") == "editarLivro") {
				let idAEditar = window.decodeURIComponent(evento.getParameter("arguments").id);
				this._carregarLivros(idAEditar)
			} else {
				this.getView().setModel(new sap.ui.model.json.JSONModel({}), "livro");
			}
		},

		_carregarLivros: function (idAEditar) {
			let _repositorioLivro = new RepositorioDeLivros;
			_repositorioLivro.BuscarLivroPorId(idAEditar).then((livroRetornado) => {
				var oModel = new JSONModel(livroRetornado);
				this.getView().setModel(oModel, "livro")
			});
		},

		AoClicarEmVoltar: function () {
			let confirmacaoParaVoltar = this.MensagemDeConfirmacao();
			// if (confirmacaoParaVoltar === 'OK') {
			// 	this.getOwnerComponent().getRouter().navTo("overview", {});
			// }

		},

		aoClicarEmSalvar: function () {
			let rota = this.getOwnerComponent().getRouter();
			let _validacao = new Validacao;

			var telaCadastro = this.getView(),
				inputs = [
					telaCadastro.byId("input-titulo"),
					telaCadastro.byId("input-editora"),
					telaCadastro.byId("input-autor"),
				],
				erroDeValidacaoDeCampos = false;
			inputs.forEach(input =>
				erroDeValidacaoDeCampos = _validacao.ValidarCampo(input) || erroDeValidacaoDeCampos, this);

			let erroDeValidacaoDeData = _validacao.ValidarData(this.getView().byId("DT"));
			let livroASerSalvo = this.getView().getModel("livro");

			if (!erroDeValidacaoDeCampos && !erroDeValidacaoDeData) {
				let _repositorioLivro = new RepositorioDeLivros;
				let livroModelo = livroASerSalvo.getData();
				if (!!livroModelo.id) {
					_repositorioLivro.AtualizarLivro(livroASerSalvo)
						.then(livro => {
							rota.navTo("detalhes", {
								id: livro.id
							});
						})
				} else {
					_repositorioLivro.SalvarLivro(livroASerSalvo)
						.then(livro => {
							rota.navTo("detalhes", {
								id: livro.id
							});
						})
				}
			} else {
				MessageBox.alert("Falha na validação dos campos");
			}
		},
		MensagemDeConfirmacao: function () {

			MessageBox.confirm("Ao voltar todos os dados serão perdidos. Deseja continuar?", {
				title: "Confirmação",
				emphasizedAction: sap.m.MessageBox.Action.OK,
				actions: [sap.m.MessageBox.Action.OK,
					sap.m.MessageBox.Action.CANCEL
				],
				onClose: function (oAction) {
					console.log(oAction);
					return oAction;
				}

			});

		}
	});
});